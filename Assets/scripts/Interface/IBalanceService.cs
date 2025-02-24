
using System.Threading.Tasks;

public interface IBalanceService
{
    Task<BalanceResponse> GetBalance();
}

