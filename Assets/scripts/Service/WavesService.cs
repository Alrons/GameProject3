using Assets.scripts.Interface.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class WavesService : IWavesService
{
    private WavesServiceProperties _wavesServiceProperties = new WavesServiceProperties();

    public async Task<StartWavePosition> GetWaveStartPos()
    {
        try
        {
            HttpResponseMessage response = null;

            // Retry the request up to 2 times if necessary
            for (int i = 0; i < 2; i++)
            {
                response = await _wavesServiceProperties.HttpClient.GetAsync($"{_wavesServiceProperties.StartWavePos}");

                if (response.IsSuccessStatusCode)
                {
                    break; // Break if request is successful
                }

            }

            // Ensure the response was successful before proceeding
            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BaseResponse<StartWavePosition>>(result).Result; 
        }
        catch (HttpRequestException ex)
        {
            Debug.LogError($"Error getting: {ex.Message}");
            throw; // re-throw the exception
        }
    }
    public async Task<Waves> GetWave()
    {
        try
        {
            HttpResponseMessage response = null;

            // Retry the request up to 2 times if necessary
            for (int i = 0; i < 2; i++)
            {
                response = await _wavesServiceProperties.HttpClient.GetAsync($"{_wavesServiceProperties.Wave}{UserConst.userId}");

                if (response.IsSuccessStatusCode)
                {
                    break; // Break if request is successful
                }

            }

            // Ensure the response was successful before proceeding
            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BaseResponse<Waves>>(result).Result;
        }
        catch (HttpRequestException ex)
        {
            Debug.LogError($"Error getting: {ex.Message}");
            throw; // re-throw the exception
        }
    }
    public async Task<bool> PostWaveStatus(WaveStatusModel model)
    {
        try
        {
            string json = JsonConvert.SerializeObject(model, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _wavesServiceProperties.HttpClient.PostAsync($"{_wavesServiceProperties.Wave}", content);

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

