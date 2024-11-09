using Newtonsoft.Json;
using System;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using System.Collections;


public class WaveMovement : MonoBehaviour
{
    public Button startButton;
    public Vector3 targetPosition;
    public Vector3 startPos;
    public float duration;
    public float startPercentage;
    public float progress;

    public int levelDevenc;

    public WavesRequest towerPower;
    public Waves Wave;
    public GameObject mainCamera;
    private WaveHealth waveHealth;
    SpawnObject spawnObject;

    private float timer;
    private bool isMoving = false; // flag to control movement
    private float waveEndPercentage; // percentage at which the wave should stop

    public bool isLoading = false;

    void Start()
    {
        towerPower = new WavesRequest
        {
            UserID = 1,
            WavesNumber = 0
        };
        levelDevenc = 4;
        spawnObject = mainCamera.GetComponent<SpawnObject>();
        StartCoroutine(InitializeSpawnObject());
    }

    void Update()
    {
        if (!isMoving) return;

        timer += Time.deltaTime;
        progress = (timer / duration) + (startPercentage / 100f);

        float healthPercent = CalculateHealthPercentage(progress);
        waveHealth.ConditionalDamage((healthPercent / Wave.WaveHealth) * 100);

        if (progress >= (waveEndPercentage / 100f))
        {
            progress = waveEndPercentage / 100f;
            isMoving = false;
            OnWaveComplete();
            return;
        }

        transform.position = Vector3.Lerp(startPos, targetPosition, progress);
    }

    private IEnumerator InitializeSpawnObject()
    {
        // Check every 0.1 seconds for SpawnObject loading completion
        while (spawnObject == null || !spawnObject.isLoading)
        {
            spawnObject = mainCamera.GetComponent<SpawnObject>();
            yield return new WaitForSeconds(0.1f);
        }

        GetTowerPower();
        SetupWavePosition();
        InitializeWaveData();
        waveHealth = mainCamera.GetComponent<WaveHealth>();
        startButton.onClick.AddListener(StartMovement);
    }

    private float CalculateHealthPercentage(float t)
    {
        if (t < WaveConstants.firstlevel)
            return Mathf.Lerp(Wave.WaveHealth, Wave.HealthLevel1, t / WaveConstants.firstlevel);

        if (t < WaveConstants.secondLevel)
        {
            levelDevenc = 2;
            return Mathf.Lerp(Wave.HealthLevel1, Wave.HealthLevel2, (t - WaveConstants.firstlevel) / WaveConstants.firstlevel);
        }

        levelDevenc = 1;
        return Mathf.Lerp(Wave.HealthLevel2, Wave.HealthLevel3, (t - WaveConstants.secondLevel) / WaveConstants.secondLevel);
    }

    public async void InitializeWaveData()
    {
        WavesService wavesService = new WavesService();
        string waveJson = await wavesService.GetWaves(1);
        var response = JsonConvert.DeserializeObject<BaseResponse<Waves>>(waveJson);
        Wave = response.Result;
        ConfigureWaveData(Wave);

        if (Wave.Status == 2) await StartInitializedWave();
        if (Wave.Status == 4) WaveIsLost();
    }

    private async Task StartInitializedWave()
    {
        WavesService wavesService = new WavesService();
        string waveJson = await wavesService.PatchWaveStrength(towerPower);
        var response = JsonConvert.DeserializeObject<BaseResponse<Waves>>(waveJson);
        Wave = response.Result;
        ConfigureWaveData(Wave);
        isMoving = true;
    }

    private void ConfigureWaveData(Waves wave)
    {
        duration = wave.DurationInSeconds;
        startPercentage = (float)wave.Passing;
        waveEndPercentage = (float)wave.WaveEnd;

        float t = startPercentage / 100f;
        transform.position = Vector3.Lerp(startPos, targetPosition, t);
    }

    public async void StartMovement()
    {
        if (!isMoving)
        {
            GetTowerPower();
            await StartInitializedWave();
            isMoving = true;
        }
    }

    private void OnWaveComplete()
    {
        Debug.Log(waveEndPercentage != 100f ? "Win!" : "Lose!");
        if (waveEndPercentage != 100f) WaveIsWin();
        else WaveIsLost();

        levelDevenc = 10;
    }

    private void GetTowerPower()
    {
        towerPower.FirstLevelOfProtectionPower = 0;
        towerPower.SecondLevelOfProtectionPower = 0;
        towerPower.ThirdLevelOfProtectionPower = 0;

        foreach (var addedItem in spawnObject.addedItemsList)
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

    private int FindTableNumber(int cellNumber)
    {
        var tableCreator = mainCamera.GetComponent<TableCreator>();
        foreach (var cellClass in tableCreator.hashSetCellNumber)
            if (cellClass.cellNumber == cellNumber)
                return cellClass.tableNumber;

        return -1;
    }

    private void SetupWavePosition()
    {
        startPos = new Vector3(-200, 100, 0);
        transform.position = startPos;
        targetPosition = new Vector3(350, 100, 0);
        isLoading = true;
    }

    private async void WaveIsLost()
    {
        ResetWave();
        await new WavesService().PutWaveAsync(CreateWaveChangeRequest());
        TowerPlase towerPlase = mainCamera.GetComponent<TowerPlase>();
        StartCoroutine(towerPlase.InitializeWaveMovementAndSpawn());
    }

    private async void WaveIsWin()
    {
        await StartInitializedWave();
        ResetWave();
        InitializeWaveData();
        TowerPlase towerPlase = mainCamera.GetComponent<TowerPlase>();
        StartCoroutine(towerPlase.InitializeWaveMovementAndSpawn());
    }

    private void ResetWave()
    {
        isMoving = false;
        timer = 0;
        SetupWavePosition();
        waveHealth.ConditionalDamage(100f);
    }

    private ChangeWaveRequest CreateWaveChangeRequest()
    {
        return new ChangeWaveRequest
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
    }
}
