using Assets.scripts.Interface.Models;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Wave : MonoBehaviour
{
    public double WavePassing { get; private set; }

    public int WaveDurationInSeconds { get; set; }

    public double WaveEnd { get; set; }

    public float WaveHealth { get; set; }

    public float MaxWaveHealth { get; set; }

    public bool isMove { get; set; }

    public int levelDevenc = 5;

    public GameObject mainCamera;
    public Button startButton;

    private Vector3 startPos;
    private Vector3 endPos;

    void Start()
    {
        ReloadWaveState();
        startButton.onClick.AddListener(StartMovement);
        isMove = false;
    }

    public void GetDamage(float damage)
    {
        if (isMove)
        {
            WaveHealth -= damage;
            ShowWaveHealth();
        }
        
    }

    public void StartMovement()
    {
        MoveWave();
    }

    private void ShowWaveHealth()
    {
        WaveHealth waveHealth = mainCamera.GetComponent<WaveHealth>();
        waveHealth.ConditionalDamage((float)((WaveHealth / MaxWaveHealth)*100));
    }
    private async void MoveWave()
    {
        startPos = await GetStartPos();
        endPos = GetEndPos();
        isMove = true;

        StartCoroutine(MoveObject());
    }
    private async void InitializedTower()
    {
        mainCamera.GetComponent<TowerPlase>().SpawnPrefabs(await GetStartPos(), GetEndPos());
    }

    private async void ReloadWaveState()
    {
        WavesService wavesService = new WavesService();
        Waves wave = await wavesService.GetWave();
        WavePassing = wave.Passing;
        WaveDurationInSeconds = wave.DurationInSeconds;
        WaveEnd = wave.WaveEnd;
        WaveHealth = wave.WaveHealth;
        MaxWaveHealth = wave.WaveHealth;
        levelDevenc = 6;

        isMove = false;
        ShowWaveHealth();
        InitializedTower();
        transform.position = await GetStartPos();
    }
    private async void WaveWin()
    {
        WavesService wavesService = new WavesService();
        WaveStatusModel waveStatusModel = new WaveStatusModel();
        waveStatusModel.WaveStatus = 1;
        waveStatusModel.UserId = UserConst.userId;
        await wavesService.PostWaveStatus(waveStatusModel);
        ReloadWaveState();
    }
    private async void WaveLose()
    {
        WavesService wavesService = new WavesService();
        WaveStatusModel waveStatusModel = new WaveStatusModel();
        waveStatusModel.WaveStatus = 0;
        waveStatusModel.UserId = UserConst.userId;
        await wavesService.PostWaveStatus(waveStatusModel);
        ReloadWaveState();
    }
    private async Task<Vector3> GetStartPos()
    {
        WavesService wavesService = new WavesService();
        StartWavePosition wavePos = await wavesService.GetWaveStartPos();

        return new Vector3(wavePos.X, wavePos.Y, 0);
    }

    private Vector3 GetEndPos()
    {
        return new Vector3(50, -150, 0);
    }

    private IEnumerator MoveObject()
    {
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        if (journeyLength == 0 || WaveDurationInSeconds <= 0)
        {
            Debug.LogError("!(journeyLength == 0 || WaveDurationInSeconds <= 0)");
            yield break;
        }

        while (true)
        {
            float fractionOfJourney = Mathf.Clamp01((Time.time - startTime) / WaveDurationInSeconds);
            transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);

            if (fractionOfJourney <= 0.25f)
            {
                levelDevenc = 5;
            }
            else if (fractionOfJourney <= 0.5f)
            {
                levelDevenc = 4;
            }
            else if (fractionOfJourney <= 0.75f)
            {
                levelDevenc = 3;
            }
            else
            {
                levelDevenc = 2;
            }

            if (fractionOfJourney >= 1)
            {
                WaveLose();
                yield return StartCoroutine(ChangeColor(Color.red));
                break;
            }
            if (WaveHealth <= 0 && fractionOfJourney < 1)
            {
                WaveWin();
                yield return StartCoroutine(ChangeColor(Color.green));
                break;
            }

            yield return null;
        }

    }

    private IEnumerator ChangeColor(Color targetColor)
    {
        Renderer renderer = transform.GetComponent<Renderer>();
        if (renderer != null)
        {
            Color startColor = targetColor;
            renderer.material.color = startColor;

            yield return new WaitForSeconds(1);

            float fadeDuration = 1f;
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                renderer.material.color = Color.Lerp(startColor, Color.white, elapsedTime / fadeDuration);
                yield return null;
            }

            renderer.material.color = Color.white;
        }
    }

}