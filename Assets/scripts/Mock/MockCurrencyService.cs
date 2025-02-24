using System.Collections.Generic;
using System.Threading.Tasks;

public class MockCurrencyService: ICurrencyService
{
    public async Task<CurrencyResponse> GetCurrency()
    {
        return new CurrencyResponse
        {
            Data = new List<CurrencyData>
            {
                new CurrencyData { Id = 1, CurrencyType = "USD", CurrencyName = "Dollar", BalanceCurrency = 1000 },
                new CurrencyData { Id = 2, CurrencyType = "EUR", CurrencyName = "Euro", BalanceCurrency = 500 },
                new CurrencyData { Id = 3, CurrencyType = "JPY", CurrencyName = "Yen", BalanceCurrency = 2000 }
            }
        };
    }
}
