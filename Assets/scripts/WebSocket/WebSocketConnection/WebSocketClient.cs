using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class WebSocketClient
{
    private ClientWebSocket clientWebSocket;
    private Uri serverUri;
    public event Action<string> OnMessageReceived;

    public WebSocketClient(string serverAddress)
    {
        clientWebSocket = new ClientWebSocket();
        serverUri = new Uri(serverAddress);
    }

    public async void ConnectAsync()
    {
        try
        {
            await clientWebSocket.ConnectAsync(serverUri, CancellationToken.None);
            Debug.Log("WebSocket connected to " + serverUri);

            Send("userId:" + UserConst.userId);
            
            await ReceiveMessagesAsync();
        }
        catch (Exception ex)
        {
            Debug.LogError("WebSocket connection failed: " + ex.Message);
        }
    }

    private async Task ReceiveMessagesAsync()
    {
        var buffer = new byte[1024];
        while (clientWebSocket.State == WebSocketState.Open)
        {
            try
            {
                var result = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    OnMessageReceived?.Invoke(message);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error receiving WebSocket message: " + ex.Message);
            }
        }
    }

    public async void Send(string message)
    {
        if (clientWebSocket.State == WebSocketState.Open)
        {
            try
            {
                var sendBuffer = Encoding.UTF8.GetBytes(message);
                await clientWebSocket.SendAsync(new ArraySegment<byte>(sendBuffer), WebSocketMessageType.Text, true, CancellationToken.None);
                Debug.Log("Sent message: " + message);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error sending WebSocket message: " + ex.Message);
            }
        }
        else
        {
            Debug.LogWarning("WebSocket is not connected. Message not sent: " + message);
        }
    }

    public async void Disconnect()
    {
        if (clientWebSocket.State == WebSocketState.Open)
        {
            try
            {
                await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                Debug.Log("WebSocket disconnected.");
            }
            catch (Exception ex)
            {
                Debug.LogError("Error disconnecting WebSocket: " + ex.Message);
            }
        }
    }
}
