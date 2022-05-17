namespace MateMachine.CodeChallenge.CurrencyConverterLib;

public interface ICurrencyConverter {
	/// <summary>
	/// Clears any prior configuration.
	/// </summary>
	void ClearConfiguration();

	/// <summary>
	/// Converts the specified amount to the desired currency.
	/// </summary>
	double? Convert(string fromCurrency, string toCurrency, double amount);

	/// <summary>
	/// Resets configuration list to its default value.
	/// </summary>
	void ResetToDefaults();

	/// <summary>
	/// Returns a list of all configurations.
	/// </summary>
	/// <returns></returns>
	string ShowConversionRates();

	/// <summary>
	/// Updates the configuration. Rates are inserted or replaced internally.
	/// </summary>
	void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates);
}
