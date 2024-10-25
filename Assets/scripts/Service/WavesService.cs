using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class WavesService : IWavesService
{
    private WavesServiceProperties _wavesServiceProperties = new WavesServiceProperties();

    public async Task<string> PatchWaveStrength(WavesRequest waves)
    {
        try
        {
            string json = JsonConvert.SerializeObject(waves, Formatting.Indented);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _wavesServiceProperties.HttpClient.PutAsync("https://localhost:7075/api/Wave/startWave", content);

            response.EnsureSuccessStatusCode(); // Ensure that the response is successful

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
            throw; // re-throw the exception
        }
    }

    public async Task<string> GetWaves(int userId)
    {
        try
        {
            HttpResponseMessage response = null;

            // Retry the request up to 2 times if necessary
            for (int i = 0; i < 2; i++)
            {
                response = await _wavesServiceProperties.HttpClient.GetAsync($"{_wavesServiceProperties.Wave}{userId}");

                if (response.IsSuccessStatusCode)
                {
                    break; // Break if request is successful
                }

            }

            // Ensure the response was successful before proceeding
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            Debug.LogError($"Error getting: {ex.Message}");
            throw; // re-throw the exception
        }
    }

    public async Task<bool> PutWaveAsync(ChangeWaveRequest wave)
    {
        try
        {
            string json = JsonConvert.SerializeObject(wave, Formatting.Indented);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _wavesServiceProperties.HttpClient.PutAsync("https://localhost:7075/api/Wave/changeWave", content);

            // Ensure the response was successful
            response.EnsureSuccessStatusCode();

            return true; // Return true if the request was successful
        }
        catch (HttpRequestException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            throw; // Re-throw the exception to be handled at a higher level
        }
    }
}

