using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MockBalanceService: IBalanceService
{
    public async Task<BalanceResponse> GetBalance()
    {
        return new BalanceResponse
        {
            Data = new BalanceData
            {
                BalanceList = new Dictionary<string, double>
                {
                    { "0", 1000 },
                    { "1", 500 },
                    { "2", 2000 }
                }
            }
        };
    }
}