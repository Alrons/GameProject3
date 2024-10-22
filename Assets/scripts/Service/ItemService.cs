using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ItemService : IItemService
{
    private readonly ItemServiceProperties _itemServiceProperties = new ItemServiceProperties();

    public async Task<bool> DeleteAddedItem(int id)
    {
        try
        {
            HttpResponseMessage response = null;
            for (int i = 0; i < 2; i++)
            {
                response = await _itemServiceProperties.HttpClient.DeleteAsync($"{_itemServiceProperties.baseUrl}{_itemServiceProperties.AddedItems}{id}");
                if (response.IsSuccessStatusCode)
                {
                    break;
                }
            }
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Debug.LogError($"Error deleting item: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> DeleteItem(int id)
    {
        try
        {
            HttpResponseMessage response = null;
            for (int i = 0; i < 2; i++)
            {
                response = await _itemServiceProperties.HttpClient.DeleteAsync($"{_itemServiceProperties.baseUrl}{_itemServiceProperties.Items}{id}");
                if (response.IsSuccessStatusCode)
                {
                    break;
                }
            }
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException ex)
        {
            Debug.LogError($"Error deleting item: {ex.Message}");
            throw;
        }
    }

    public async Task<string> GetAddedItem()
    {
        try
        {
            HttpResponseMessage response = null;
            for (int i = 0; i < 2; i++)
            {
                response = await _itemServiceProperties.HttpClient.GetAsync($"{_itemServiceProperties.AddedItems}{_itemServiceProperties.UserId}");
                if (response.IsSuccessStatusCode)
                {
                    break;
                }
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            Debug.LogError($"Error getting added items: {ex.Message}");
            throw;
        }
    }

    public async Task<string> GetItem()
    {
        try
        {
            HttpResponseMessage response = null;
            for (int i = 0; i < 2; i++)
            {
                response = await _itemServiceProperties.HttpClient.GetAsync($"{_itemServiceProperties.Items}{_itemServiceProperties.UserId}");
                if (response.IsSuccessStatusCode)
                {
                    break;
                }
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            Debug.LogError($"Error getting items: {ex.Message}");
            throw;
        }
    }

    public async Task<string> GetOurTables()
    {
        try
        {
            HttpResponseMessage response = null;
            for (int i = 0; i < 2; i++)
            {
                response = await _itemServiceProperties.HttpClient.GetAsync($"{_itemServiceProperties.Tables}");
                if (response.IsSuccessStatusCode)
                {
                    break;
                }
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            Debug.LogError($"Error getting tables: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> PostAddedItem(AddedItemsRequest model)
    {
        try
        {
            string json = JsonConvert.SerializeObject(model, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _itemServiceProperties.HttpClient.PostAsync($"{_itemServiceProperties.AddedItems}save", content);

            response.EnsureSuccessStatusCode(); // Ensure the response was successful

            return true;
        }
        catch (HttpRequestException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            throw;
        }
    }

    public async Task<bool> PostItem(ItemRequest model)
    {
        try
        {
            string json = JsonConvert.SerializeObject(model, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _itemServiceProperties.HttpClient.PostAsync($"{_itemServiceProperties.Items}save", content);

            response.EnsureSuccessStatusCode(); // Ensure the response was successful

            return true;
        }
        catch (HttpRequestException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            throw;
        }
    }
}
