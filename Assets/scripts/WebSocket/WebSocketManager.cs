using Newtonsoft.Json;
using System;
using UnityEngine;

public class WebSocketManager
{
    private WebSocketMetods webSocketMetods = new WebSocketMetods();

    public void HandleMessage(string message, Wave wave)
    {
        if (message == "item has been changed")
        {
            webSocketMetods.UpdateAllItems();
        }
        else if (message.StartsWith("{")) // check that the message is in JSON format
        {
            try
            {
                WebSocketResponseWaveParams data = JsonConvert.DeserializeObject<WebSocketResponseWaveParams>(message);
                //if (data != null)
                //{
                //    webSocketMetods.UpdateWaveStatus(wave, data.health, data.progress / 100f);
                //}
            }
            catch (Exception ex)
            {
                Debug.LogError("Error parsing JSON message: " + ex.Message);
            }
        }
        
    }
}