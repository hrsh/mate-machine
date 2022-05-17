namespace MateMachine.CodeChallenge.CurrencyConverterLib; 

internal static class ConverterHelper {
	public static IEnumerable<string> CreateSource(IList<Tuple<string, string, double>> input) {
		var fromCurrencies = input.Select(_ => _.Item1);
		var toCurrencies = input.Select(_ => _.Item2);

		return fromCurrencies.Concat(toCurrencies).Distinct();
	}
}