
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICurrencyService
{
    Task<CurrencyResponse> GetCurrency();
}
