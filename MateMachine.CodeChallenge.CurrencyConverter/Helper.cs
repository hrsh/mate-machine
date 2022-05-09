using MateMachine.CodeChallenge.CurrencyConverterLib;

namespace MateMachine.CodeChallenge.CurrencyConverter;

public class Helper {
	public static void ShowMenu() {
		Print($"1: List of converters");
		Print($"2: Update converter");
		Print($"3: Convert");
		Print($"4: Clear conversion rates");
		Print($"5: Set conversion rates to default");
		Print($"9: Exit\n");
		Print($"> ", false);
	}

	public static void Update(ICurrencyConverter converter) {
		var getInput = true;
		var input = new List<Tuple<string, string, double>>();
		do {
			Print("Enter origin currency, destination currency, convert rate: ");
			string? fromCurrency, toCurrency;
			double convertRate;
			Print("Origin currency: ", false);
			while ((fromCurrency = Console.ReadLine()) is null || string.IsNullOrWhiteSpace(fromCurrency)) {
				Print("Enter a valid value for origin currency: ", false);
			}

			Print("Destination currency: ", false);
			while ((toCurrency = Console.ReadLine()) is null || string.IsNullOrWhiteSpace(toCurrency)) {
				Print("Enter a valid value for destination currency: ", false);
			}

			Print("Rate: ", false);
			while (!double.TryParse(Console.ReadLine(), out convertRate) || convertRate == 0) {
				Print("Enter a valid value for convert rate: ", false);
			}
			input.Add(Tuple.Create(fromCurrency.ToUpper()!, toCurrency.ToUpper()!, convertRate));
			Print("Continue (y/n)? ", false);
			getInput = Console.ReadLine()?.ToLower() != "n";
		} while (getInput);
		converter.UpdateConfiguration(input);
	}

	public static void DoConvert(ICurrencyConverter converter) {
		var getInput = true;
		do {
			Print("Enter origin currency, destination currency, amount: ");
			string? fromCurrency, toCurrency;
			double convertRate;
			Print("Origin currency: ", false);
			while ((fromCurrency = Console.ReadLine()) is null || string.IsNullOrWhiteSpace(fromCurrency)) {
				Print("Enter a valid value for origin currency: ", false);
			}

			Print("Destination currency: ", false);
			while ((toCurrency = Console.ReadLine()) is null || string.IsNullOrWhiteSpace(toCurrency)) {
				Print("Enter a valid value for destination currency: ", false);
			}

			Print("Amount: ", false);
			while (!double.TryParse(Console.ReadLine(), out convertRate)) {
				Print("Enter a valid value for amount: ", false);
			}
			var result = converter.Convert(fromCurrency.ToUpper(), toCurrency.ToUpper(), convertRate);
			Print($"({convertRate}){fromCurrency} is ({result}){toCurrency}.");

			Print("Convert another value (y/n)? ", false);
			getInput = Console.ReadLine()?.ToLower() != "n";
		} while (getInput);
	}

	private static void Print(string source, bool line = true) {
		if (line) {
			Console.WriteLine(source);
		}
		else {
			Console.Write(source);
		}
	}
}
