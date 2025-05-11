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
                new CurrencyData { Id = 0, CurrencyType = "USD", CurrencyName = "Dollar" },
                new CurrencyData { Id = 1, CurrencyType = "EUR", CurrencyName = "Euro" },
                new CurrencyData { Id = 2, CurrencyType = "JPY", CurrencyName = "Yen" }
            }
        };
    }
}
