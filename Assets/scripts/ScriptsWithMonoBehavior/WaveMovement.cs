using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WaveMovement : MonoBehaviour
{
    public Button startButton;
    public Vector3 targetPosition;
    public Vector3 startPos;
    public GameObject mainCamera;

    public int levelDevenc = 5;
    public float progress; // Current progress value

    private float targetProgress; // The target value of progress
    private WaveHealth waveHealth;
    private TowerPlase towerPlase;

    public bool isMoving = false;
    public bool isLoading = false;
    private WebSocketClient webSocketClient;
    private WebSocketManager webSocketManager;

    void Start()
    {
        SetupWavePosition();
        
        waveHealth = mainCamera.GetComponent<WaveHealth>();
        towerPlase = mainCamera.GetComponent<TowerPlase>();

        // Initialize WebSocket client
        webSocketClient = new WebSocketClient(WebSocketConst.path);
        webSocketManager = new WebSocketManager();
        webSocketClient.OnMessageReceived += OnServerMessage;
        webSocketClient.ConnectAsync();

        startButton.onClick.AddListener(StartWave);
        isLoading = true;

        targetProgress = 0f;
    }

    private void OnServerMessage(string message)
    {
        try
        {
            if (message == "Player win!")
            {
                WaveWin();
                towerPlase.RestorePrefabs();
            }
            else if (message == "Player lose!")
            {
                WaveLoos();
                towerPlase.RestorePrefabs();
            }
            webSocketManager.HandleMessage(message, this);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error processing message: " + ex.Message);
        }
    }

    public void UpdateWaveData(float health, float waveProgress)
    {
        waveHealth.ConditionalDamage(health);
        isMoving = true;
        targetProgress = waveProgress;
    }

    private void StartWave()
    {
        if (isMoving) return;

        // Notify server that the wave has started
        webSocketClient.Send("start_wave");
        isMoving = true;
    }
    private async void SetupWavePosition()
    {
        WavesService wavesService = new WavesService();
        StartWavePosition wavePos = await wavesService.GetWaveStartPos();
        startPos = new Vector3(wavePos.X, wavePos.Y, 0);
        transform.position = startPos;
        targetPosition = new Vector3(50, -150, 0);
    }

    private void Update()
    {
        if (isMoving)
        {
            // Smoothly updating progress
            progress = Mathf.MoveTowards(progress, targetProgress, Time.deltaTime);
            UpdateWavePosition();

            if (progress <= 0.33)
            {
                levelDevenc = 4;
            }
            else if (progress <= 0.66)
            {
                levelDevenc = 3;
            }
            else
            {
                levelDevenc = 2;
            }
        }
    }

    private void WaveWin()
    {
        isMoving = false;
        progress = 0;
        levelDevenc = 5;
        SetupWavePosition();
        waveHealth.ConditionalDamage(100);
        ChangeItemState();
    }
    private void WaveLoos()
    {
        isMoving = false;
        progress = 0;
        levelDevenc = 5;
        SetupWavePosition();
        waveHealth.ConditionalDamage(100);
        ChangeItemState();
    }

    private void UpdateWavePosition()
    {
        // Update wave position based on progress
        transform.position = Vector3.Lerp(startPos, targetPosition, progress);
    }

    private void ChangeItemState()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items)
        {
            if (item.GetComponent<ItemState>().itemState == 3)
            {
                item.GetComponent<ItemState>().itemState = 1;
            }
        }
    }

    void OnDestroy()
    {
        webSocketClient?.Disconnect();
    }
}