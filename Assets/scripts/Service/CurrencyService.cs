using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class CurrencyService
{
    private readonly CurrencyServiceProperties _currencyServiceProperties = new CurrencyServiceProperties();

    public async Task<CurrencyResponse> GetCurrency()
    {
        try
        {
            HttpResponseMessage response = null;

            for (int i = 0; i < 2; i++)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{_currencyServiceProperties.baseUrl}");
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
            return JsonConvert.DeserializeObject<CurrencyResponse>(result);
        }
        catch (HttpRequestException ex)
        {
            Debug.LogError($"Error getting currency: {ex.Message}");
            throw;
        }
    }
}