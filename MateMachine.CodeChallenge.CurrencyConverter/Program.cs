using MateMachine.CodeChallenge.CurrencyConverter;
using MateMachine.CodeChallenge.CurrencyConverterLib;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
	.AddSingleton<ICurrencyConverter, CurrencyConverter>()
	.BuildServiceProvider();

var converter = serviceProvider.GetRequiredService<ICurrencyConverter>();

var alive = true;

while (alive) {
	Helper.ShowMenu();
	var selectedAction = Console.ReadLine();

	switch (selectedAction) {
		case "1":
			Console.WriteLine(converter.ShowConversionRates());
			break;

		case "2":
			Helper.Update(converter);
			break;

		case "3":
			Helper.DoConvert(converter);
			break;

		case "4":
			converter.ClearConfiguration();
			break;

		case "5":
			converter.ResetToDefaults();
			break;

		case "9":
			alive = false;
			break;

		default:
			Console.WriteLine("> Enter a value between 1~9: ");
			break;
	}
}
