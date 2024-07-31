using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ItemService : IItemService
{
    private ItemServiceProperties itemServiseProperties = new ItemServiceProperties();
    public async Task<bool> DeleteAddedItem(int id)
    {
        try
        {
            HttpResponseMessage response = default;
            // Use the BaseUrl constant
            for (int i = 0; i < 2; i++)
            {
                response = await itemServiseProperties._httpClient.DeleteAsync($"{itemServiseProperties.BaseUrl}{itemServiseProperties._AddedItemsUrl}{id}");
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
            throw; // re-throw the exception
        }
    }

    public async Task<bool> DeleteItem(int id)
    {
        try
        {
            HttpResponseMessage response = default;
            // Use the BaseUrl constant
            for (int i = 0; i < 2; i++)
            {
                response = await itemServiseProperties._httpClient.DeleteAsync($"{itemServiseProperties.BaseUrl}{itemServiseProperties._Items}{id}");
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
            throw; // re-throw the exception
        }
    }

    public async Task<string> GetAddedItem()
    {
        try
        {
            HttpResponseMessage response = default;
            for (int i = 0; i < 2; i++)
            {
                response = await itemServiseProperties._httpClient.GetAsync(itemServiseProperties._AddedItemsUrl);
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
            throw; // re-throw the exception
        }
    }

    public async Task<string> GetItem()
    {
        try
        {
            HttpResponseMessage response = default;
            for (int i = 0; i < 2; i++)
            {
                response = await itemServiseProperties._httpClient.GetAsync(itemServiseProperties._Items);
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
            throw; // re-throw the exception
        }
    }

    public async Task<string> GetOurTables()
    {
        try
        {
            HttpResponseMessage response = default;
            for (int i = 0; i < 2; i++)
            {
                response = await itemServiseProperties._httpClient.GetAsync(itemServiseProperties._OurTables);
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
            throw; // re-throw the exception
        }
    }

    public async Task<bool> PostAddedItem(AddedItemModel model)
    {
        try
        {
            var json = JsonUtility.ToJson(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await itemServiseProperties._httpClient.PostAsync(itemServiseProperties._AddedItemsUrl, content);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            Debug.Log("Response: " + responseBody);

            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
            throw; // re-throw the exception
        }
    }

    public async Task<bool> PostItem(ItemModel model)
    {
        try
        {
            var json = JsonUtility.ToJson(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await itemServiseProperties._httpClient.PostAsync(itemServiseProperties._Items, content);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            Debug.Log("Response: " + responseBody);

            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
            throw; // re-throw the exception
        }
    }

}
