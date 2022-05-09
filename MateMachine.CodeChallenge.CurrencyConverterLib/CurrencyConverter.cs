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

	public double Convert(string fromCurrency, string toCurrency, double amount) {
		var result = -1;

		if (fromCurrency == toCurrency) {
			return amount;
		}

		return result;
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

	private double FormatOutput(double input) {
		return Math.Round(input, 5);
	}
}
