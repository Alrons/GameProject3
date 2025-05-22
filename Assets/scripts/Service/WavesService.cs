using Assets.scripts.Interface.Models;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;


public class WavesService : IWavesService
{
    private WavesServiceProperties _wavesServiceProperties = new WavesServiceProperties();

    public async Task<StartWavePosition> GetWaveStartPos()
    {
        string url = $"{_wavesServiceProperties.baseUrl}{_wavesServiceProperties.StartWavePos}";
        return await SendGetRequest<StartWavePosition>(url);
    }

    public async Task<Waves> GetWave()
    {
        string url = $"{_wavesServiceProperties.baseUrl}{_wavesServiceProperties.Wave}{UserConst.userId}";
        return await SendGetRequest<Waves>(url);
    }

    public async Task<bool> PostWaveStatus(WaveStatusModel model)
    {
        string url = $"{_wavesServiceProperties.baseUrl}{_wavesServiceProperties.Wave}";
        return await SendPostRequest(url, model);
    }

    private async Task<T> SendGetRequest<T>(string url)
    {
        string apiKey = _wavesServiceProperties.ApiKey;

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
                Debug.LogError($"Error getting data from {url}: {request.error}");
                throw new System.Exception($"Request failed: {request.error}");
            }
        }
    }

    private async Task<bool> SendPostRequest<T>(string url, T model)
    {
        string json = JsonConvert.SerializeObject(model, Formatting.Indented);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
        string apiKey = _wavesServiceProperties.ApiKey;

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
                Debug.LogError($"Error posting data to {url}: {request.error}");
                return false;
            }
        }
    }
}

