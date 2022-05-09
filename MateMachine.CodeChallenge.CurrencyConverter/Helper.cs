using MateMachine.CodeChallenge.CurrencyConverterLib;

namespace MateMachine.CodeChallenge.CurrencyConverter;

public class Helper {
	public static void ShowMenu() {
		Console.WriteLine($"1: List of converters");
		Console.WriteLine($"2: Update converter");
		Console.WriteLine($"3: Convert");
		Console.WriteLine($"4: Clear conversion rates");
		Console.WriteLine($"5: Set conversion rates to default");
		Console.WriteLine($"9: Exit\n");
		Console.Write($"> ");
	}

	public static void Update(ICurrencyConverter converter) {
		var getInput = true;
		var input = new List<Tuple<string, string, double>>();
		do {
			Console.WriteLine("Enter origin currency, destination currency, convert rate: ");
			string? fromCurrency, toCurrency;
			double convertRate;
			Console.Write("Origin currency: ");
			while ((fromCurrency = Console.ReadLine()) is null || string.IsNullOrWhiteSpace(fromCurrency)) {
				Console.Write("Enter a valid value for origin currency: ");
			}

			Console.Write("Destination currency: ");
			while ((toCurrency = Console.ReadLine()) is null || string.IsNullOrWhiteSpace(toCurrency)) {
				Console.Write("Enter a valid value for destination currency: ");
			}

			Console.Write("Rate: ");
			while (!double.TryParse(Console.ReadLine(), out convertRate) || convertRate == 0) {
				Console.Write("Enter a valid value for convert rate: ");
			}
			input.Add(Tuple.Create(fromCurrency.ToUpper()!, toCurrency.ToUpper()!, convertRate));
			Console.Write("Continue (y/n)? ");
			getInput = Console.ReadLine() != "n";
		} while (getInput);
		converter.UpdateConfiguration(input);
	}

	public static void DoConvert(ICurrencyConverter converter) {
		var getInput = true;
		do {
			Console.WriteLine("Enter origin currency, destination currency, amount: ");
			string? fromCurrency, toCurrency;
			double convertRate;
			Console.Write("Origin currency: ");
			while ((fromCurrency = Console.ReadLine()) is null || string.IsNullOrWhiteSpace(fromCurrency)) {
				Console.Write("Enter a valid value for origin currency: ");
			}

			Console.Write("Destination currency: ");
			while ((toCurrency = Console.ReadLine()) is null || string.IsNullOrWhiteSpace(toCurrency)) {
				Console.Write("Enter a valid value for destination currency: ");
			}

			Console.Write("Amount: ");
			while (!double.TryParse(Console.ReadLine(), out convertRate)) {
				Console.Write("Enter a valid value for amount: ");
			}
			var result = converter.Convert(fromCurrency.ToUpper(), toCurrency.ToUpper(), convertRate);
			Console.WriteLine($"({convertRate}){fromCurrency} is ({result}){toCurrency}.");

			Console.Write("Convert another value (y/n)? ");
			getInput = Console.ReadLine() != "n";
		} while (getInput);
	}
}
