using System.Text;

namespace MateMachine.CodeChallenge.CurrencyConverterLib;

public sealed class CurrencyConverter : ICurrencyConverter {
	private IList<Tuple<string, string, double>> _rates;

	public CurrencyConverter() {
		_rates = new List<Tuple<string, string, double>>();
		SetupDefaults();
	}

	public void ClearConfiguration() {
		_rates.Clear();
	}

	// Author note:
	// Previously, I tried to implement dijkstra algorithm so that 
	// calculate the scale factor directly! :(
	public double? Convert(string fromCurrency, string toCurrency, double amount) {
		var scaleFactor = 1.0;

		var vertices = ConverterHelper.CreateSource(_rates);
		var graph = new Graph(vertices, _rates);

		var dijkstraAlgorithm = Dijkstra.ShortestPathFunction(graph, fromCurrency);
		var path = dijkstraAlgorithm(toCurrency);

		if (path is null) {
			return null;
		}

		var shortestPath = path as string[] ?? path.ToArray();

		var convertors = new List<Tuple<string, string>>();
		for (var i = 0; i < shortestPath.Length - 1; i++) {
			convertors.Add(Tuple.Create(shortestPath[i], shortestPath[i + 1]));
		}
		foreach (var (from, to) in convertors) {
			var converter = _rates.FirstOrDefault(_ => _.Item1 == from && _.Item2 == to);
			if (converter is not null) {
				scaleFactor *= converter.Item3;
			}
			else {
				converter = _rates.First(_ => _.Item1 == to && _.Item2 == from);
				scaleFactor /= converter.Item3;
			}
		}

		return FormatOutput(scaleFactor * amount);
	}

	public void ResetToDefaults() {
		_rates = new List<Tuple<string, string, double>> {
			Tuple.Create("USD", "CAD", 1.34),
			Tuple.Create("CAD", "GBP", 0.58),
			Tuple.Create("USD", "EUR", 0.86)
		};
	}

	public string ShowConversionRates() {
		if (!_rates.Any()) {
			return $"There is no conversion rate!{Environment.NewLine}";
		}

		var stringBuilder = new StringBuilder();
		foreach (var (from, to, rate) in _rates) {
			stringBuilder.Append($"({from} => {to}) {rate} {Environment.NewLine}");
		}

		stringBuilder.Append($"{Environment.NewLine}");

		return stringBuilder.ToString();
	}

	public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates) {
		var tuples = conversionRates.ToList();

		if (!tuples.Any()) {
			return;
		}

		foreach (var rate in tuples) {
			var origin = _rates.FirstOrDefault(_ => _.Item1 == rate.Item1 && _.Item2 == rate.Item2);

			if (origin is null) {
				_rates.Add(new Tuple<string, string, double>(rate.Item1, rate.Item2, rate.Item3));
			}
			else {
				var index = _rates.IndexOf(origin);
				_rates[index] = rate;
			}
		}
	}

	private void SetupDefaults() {
		_rates = new List<Tuple<string, string, double>> {
			Tuple.Create("USD", "CAD", 1.34),
			Tuple.Create("CAD", "GBP", 0.58),
			Tuple.Create("USD", "EUR", 0.86)
		};
	}

	private static double FormatOutput(double input) {
		return Math.Round(input, 5);
	}
}
