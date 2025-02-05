
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICurrencyService
{
    Task<List<BalanceList>> GetBalances();
}
