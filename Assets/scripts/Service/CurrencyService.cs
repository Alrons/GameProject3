using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class CurrencyService: ICurrencyService
{
    private readonly CurrencyServiceProperties _currencyServiceProperties = new CurrencyServiceProperties();

    public async Task<CurrencyResponse> GetCurrency()
    {
        string url = _currencyServiceProperties.baseUrl;
        string apiKey = _currencyServiceProperties.ApiKey;

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Authorization", $"Bearer {apiKey}");
            request.SetRequestHeader("Accept", "*/*");

            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield(); // Ждем завершения запроса

            if (request.result == UnityWebRequest.Result.Success)
            {
                return JsonConvert.DeserializeObject<CurrencyResponse>(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError($"Error getting currency: {request.error}");
                throw new System.Exception($"Request failed: {request.error}");
            }
        }
    }
}