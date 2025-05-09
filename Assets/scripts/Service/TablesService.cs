using Assets.scripts.Interface.Models;
using System.Collections.Generic;
using Assets.scripts.ServiceProperties;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine;

namespace Assets.scripts.Service
{
    public class TablesService
    {
        public readonly TablesServiceProperties _tablesServiceProperties = new TablesServiceProperties();

        public async Task<List<TableDto>> GetTablesAsync()
        {
            string url = $"{_tablesServiceProperties.baseUrl}{_tablesServiceProperties.Tables}";
            return await SendGetRequest<List<TableDto>>(url);
        }

        private async Task<T> SendGetRequest<T>(string url)
        {
            string apiKey = _tablesServiceProperties.ApiKey;

            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                request.SetRequestHeader("Authorization", $"Bearer {apiKey}");
                request.SetRequestHeader("Accept", "*/*");

                var operation = request.SendWebRequest();
                while (!operation.isDone)
                    await Task.Yield();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    return JsonConvert.DeserializeObject<BaseResponse<T>>(request.downloadHandler.text).Result;
                }
                else
                {
                    Debug.LogError($"Error getting data: {request.error}");
                    throw new System.Exception($"Request failed: {request.error}");
                }
            }
        }
    }
}
