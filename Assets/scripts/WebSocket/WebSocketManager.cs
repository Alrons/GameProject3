using Newtonsoft.Json;
using System;
using UnityEngine;

public class WebSocketManager
{
    private WebSocketMetods webSocketMetods = new WebSocketMetods();

    public void HandleMessage(string message, WaveMovement waveMovement)
    {
        if (message == "item has been changed")
        {
            webSocketMetods.UpdateAllItems(waveMovement.gameObject);
        }
        else if (message.StartsWith("{")) // check that the message is in JSON format
        {
            try
            {
                WebSocketResponseWaveParams data = JsonConvert.DeserializeObject<WebSocketResponseWaveParams>(message);
                if (data != null)
                {
                    webSocketMetods.UpdateWaveStatus(waveMovement, data.health, data.progress / 100f);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error parsing JSON message: " + ex.Message);
            }
        }
        
    }
}