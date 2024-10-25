using Newtonsoft.Json;
using System;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class WaveMovement : MonoBehaviour
{
    public Button startButton;

    public Vector3 targetPosition;
    public Vector3 StartPos;
    public float duration;
    public float startPercentage;

    public WavesRequest towerPower;
    public Waves Wave;
    public GameObject mainCamera;

    private float timer;
    private bool isMoving = false; // flag to control movement
    private float waveEndPercentage; // percentage at which the wave should stop
    
    void Start()
    {
        towerPower = new WavesRequest()
        {
            UserID = 1,
            WavesNumber = 0,
            FirstLevelOfProtectionPower = 0,
            SecondLevelOfProtectionPower = 0,
            ThirdLevelOfProtectionPower = 0
        };
        GetTowerPower();
        StaticWavePos();
        InitializingWaveData();

        startButton.onClick.AddListener(StartMovement);

        
    }

    void Update()
    {
        if (isMoving)
        {
            timer += Time.deltaTime;
            float t = (timer / duration) + (startPercentage / 100f); // adjust for start percentage

            // Check if we reached the wave end percentage (WaveEnd)
            if (t >= (waveEndPercentage / 100f))
            {
                t = waveEndPercentage / 100f; // limit to not exceed the final percentage
                isMoving = false; // stop movement
                OnWaveComplete(); // trigger action on completion
                return;
            }

            transform.position = Vector3.Lerp(StartPos, targetPosition, t);
        }
    }
    // Initialize wave data from the server
    public async void InitializingWaveData()
    {
        WavesService wavesService = new WavesService();
        string WaveJson = await wavesService.GetWaves(1);
        BaseResponse<Waves> baseResponseItems = JsonConvert.DeserializeObject<BaseResponse<Waves>>(WaveJson);
        Wave = baseResponseItems.Result;
        SetData(Wave);
        if (Wave.Status == 2)
        {
            await InitializingStartedWave(); 
        }
        if (Wave.Status == 4) { WaveIsLost(); }

    }
    private async Task InitializingStartedWave()
    {
        WavesService wavesService = new WavesService();
        string WaveJson = await wavesService.PatchWaveStrength(towerPower);
        BaseResponse<Waves> baseResponseItems = JsonConvert.DeserializeObject<BaseResponse<Waves>>(WaveJson);
        Wave = baseResponseItems.Result;
        SetData(Wave);
        isMoving = true;
    }
    private void SetData(Waves Wave)
    {
        duration = (float)Wave.DurationInSeconds;
        startPercentage = (float)Wave.Passing; // set the start percentage for the wave
        waveEndPercentage = (float)Wave.WaveEnd; // set the final percentage for the wave

        float t = startPercentage / 100f;
        transform.position = Vector3.Lerp(StartPos, targetPosition, t);
    }

    // Method to trigger the movement
    public async void StartMovement()
    {
        if (!isMoving)
        {
            GetTowerPower();
            await InitializingStartedWave();
            isMoving = true;
        }
        
    }

    // Method triggered when the wave completes its movement
    private void OnWaveComplete()
    {
        Debug.Log(Wave.WavesNumber);
        Debug.Log(towerPower.FirstLevelOfProtectionPower);
        Debug.Log(towerPower.SecondLevelOfProtectionPower);
        Debug.Log(towerPower.ThirdLevelOfProtectionPower);
        if (waveEndPercentage != 100f)
        {
            Debug.Log("Win!");
            WaveIsWin();
        }
        else
        {
            Debug.Log("Lose!");
            WaveIsLost();
        }
    }
    private void GetTowerPower()
    {
        towerPower.FirstLevelOfProtectionPower = 0;
        towerPower.SecondLevelOfProtectionPower = 0;
        towerPower.ThirdLevelOfProtectionPower = 0;
        SpawnObject spawnObject = mainCamera.GetComponent<SpawnObject>();
        foreach (AddedItemModel addedItem in spawnObject.addedItemsList)
        {
            int tableNumber = FindTableNumber(addedItem.place);
            switch (tableNumber) 
            {
                case 1:
                    towerPower.FirstLevelOfProtectionPower += addedItem.power;
                    break;
                case 2:
                    towerPower.SecondLevelOfProtectionPower += addedItem.power;
                    break;
                case 3:
                    towerPower.ThirdLevelOfProtectionPower += addedItem.power;
                    break;
            }

        }
    }
    private int FindTableNumber(int NumberCell)
    {
        TableCreator tableCreator = mainCamera.GetComponent<TableCreator>();
        foreach (CellNumberModel cellClass in tableCreator.hashSetCellNumber)
        {
            if (cellClass.cellNumber == NumberCell)
            {
                return cellClass.tableNumber;
            }
        }
        return -1;
    }

    private void StaticWavePos()
    {
        StartPos = new Vector3(-200, 100, 0);
        transform.position = StartPos;
        targetPosition = new Vector3(350, 100, 0);
    }
    
    private async void WaveIsLost()
    {
        isMoving = false;
        StaticWavePos();
        timer = 0;
        ChangeWaveRequest request = new ChangeWaveRequest
        {
            Id = Wave.Id,
            UserID = Wave.UserID,
            WavesNumber = Wave.WavesNumber,
            DurationInSeconds = Wave.DurationInSeconds,
            WavesPower = Wave.WavesPower,
            StartWave = DateTime.Now,
            Passing = 0,
            WaveEnd = 0,
            WaveHealth = Wave.WaveHealth,
            Status = 1
        };
        WavesService wavesService = new WavesService();
        await wavesService.PutWaveAsync(request);

    }
    private async void WaveIsWin()
    {
        await InitializingStartedWave();
        isMoving = false;
        timer = 0;
        StaticWavePos();
        InitializingWaveData();
    }
}
