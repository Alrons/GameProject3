using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class BalanceService
{
    private readonly BalanceServiceProperties _currencyServiceProperties = new BalanceServiceProperties();

    public async Task<BalanceResponse> GetBalance()
    {
        try
        {
            HttpResponseMessage response = null;

            for (int i = 0; i < 2; i++)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, _currencyServiceProperties.baseUrl);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _currencyServiceProperties.ApiKey);

                response = await _currencyServiceProperties.HttpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    break;
                }
            }

            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BalanceResponse>(result);
        }
        catch (HttpRequestException ex)
        {
            Debug.LogError($"Error getting balance: {ex.Message}");
            throw;
        }
    }
}
