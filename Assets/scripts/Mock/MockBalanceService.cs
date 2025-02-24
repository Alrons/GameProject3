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
                    { "1", 1000 },
                    { "2", 500 },
                    { "3", 2000 }
                }
            }
        };
    }
}