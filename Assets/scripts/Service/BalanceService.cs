using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class BalanceService : IBalanceService
{
    private readonly BalanceServiceProperties _balanceServiceProperties = new BalanceServiceProperties();

    public async Task<BalanceResponse> GetBalance()
    {
        string url = _balanceServiceProperties.baseUrl;
        string apiKey = _balanceServiceProperties.ApiKey;

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Authorization", $"Bearer {apiKey}");
            request.SetRequestHeader("Accept", "*/*");

            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield(); // Ждем завершения запроса

            if (request.result == UnityWebRequest.Result.Success)
            {
                return JsonConvert.DeserializeObject<BalanceResponse>(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError($"Error getting balance: {request.error}");
                throw new System.Exception($"Request failed: {request.error}");
            }
        }
    }
}
