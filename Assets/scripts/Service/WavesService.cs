using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


internal class WavesService : IWavesService
{
    private WavesServiceProperties _wavesServiceProperties = new WavesServiceProperties();
    public async Task<string> GetWaves()
    {
        try
        {
            HttpResponseMessage response = default;
            for (int i = 0; i < 2; i++)
            {
                response = await _wavesServiceProperties.HttpClient.GetAsync(_wavesServiceProperties.Waves);
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

    public async Task<string> GetWavesById(int id)
    {
        try
        {
            HttpResponseMessage response = default;
            for (int i = 0; i < 2; i++)
            {
                response = await _wavesServiceProperties.HttpClient.GetAsync($"{_wavesServiceProperties.Waves}{id}");
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

    public async Task<bool> PutWaves(int id, Waves model)
    {
        try
        {
            var json = JsonUtility.ToJson(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _wavesServiceProperties.HttpClient.PutAsync($"{_wavesServiceProperties.Waves}{id}", content);
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

