using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ItemService : IItemService
{
    private readonly ItemServiceProperties _itemServiceProperties = new ItemServiceProperties();

    public async Task<bool> DeleteAddedItem(int id)
    {
        string url = $"{_itemServiceProperties.baseUrl}{_itemServiceProperties.AddedItems}{id}";
        return await SendDeleteRequest(url);
    }

    public async Task<bool> DeleteItem(int id)
    {
        string url = $"{_itemServiceProperties.baseUrl}{_itemServiceProperties.Items}{id}";
        return await SendDeleteRequest(url);
    }

    public async Task<List<AddedItemModel>> GetAddedItem()
    {
        string url = $"{_itemServiceProperties.baseUrl}{_itemServiceProperties.AddedItems}{_itemServiceProperties.UserId}";
        return await SendGetRequest<List<AddedItemModel>>(url);
    }

    public async Task<List<ItemModel>> GetItem()
    {
        string url = $"{_itemServiceProperties.baseUrl}{_itemServiceProperties.Items}{_itemServiceProperties.UserId}";
        return await SendGetRequest<List<ItemModel>>(url);
    }

    public async Task<List<TableDataModel>> GetOurTables()
    {
        string url = $"{_itemServiceProperties.baseUrl}{_itemServiceProperties.Tables}";
        return await SendGetRequest<List<TableDataModel>>(url);
    }

    public async Task<bool> PostAddedItem(AddedItemsRequest model)
    {
        string url = $"{_itemServiceProperties.baseUrl}{_itemServiceProperties.AddedItems}save";
        return await SendPostRequest(url, model);
    }

    public async Task<bool> PostItem(ItemRequest model)
    {
        string url = $"{_itemServiceProperties.baseUrl}{_itemServiceProperties.Items}save";
        return await SendPostRequest(url, model);
    }

    private async Task<bool> SendDeleteRequest(string url)
    {
        string apiKey = _itemServiceProperties.ApiKey;

        using (UnityWebRequest request = UnityWebRequest.Delete(url))
        {
            request.SetRequestHeader("Authorization", $"Bearer {apiKey}");
            request.SetRequestHeader("Accept", "*/*");

            var operation = request.SendWebRequest();
            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                return true;
            }
            else
            {
                Debug.LogError($"Error deleting item: {request.error}");
                return false;
            }
        }
    }

    private async Task<T> SendGetRequest<T>(string url)
    {
        string apiKey = _itemServiceProperties.ApiKey;

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

    private async Task<bool> SendPostRequest<T>(string url, T model)
    {
        string json = JsonConvert.SerializeObject(model, Formatting.Indented);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
        string apiKey = _itemServiceProperties.ApiKey;

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.SetRequestHeader("Authorization", $"Bearer {apiKey}");
            request.SetRequestHeader("Accept", "*/*");

            request.uploadHandler = new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            var operation = request.SendWebRequest();
            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                return true;
            }
            else
            {
                Debug.LogError($"Error posting data: {request.error}");
                return false;
            }
        }
    }
}
