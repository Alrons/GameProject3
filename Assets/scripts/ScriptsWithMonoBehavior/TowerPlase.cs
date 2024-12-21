using System.Collections;
using UnityEngine;

public class TowerPlase : MonoBehaviour
{
    public Canvas canvas;
    public GameObject wave;
    public GameObject prefab;
    private WaveMovement waveMovement;
    private GameObject firstInstanse;
    private GameObject secondInstanse;

    void Start()
    {
        StartCoroutine(InitializeWaveMovementAndSpawn());
    }

    public IEnumerator InitializeWaveMovementAndSpawn()
    {
        while (waveMovement == null || !waveMovement.isLoading)
        {
            waveMovement = wave.GetComponent<WaveMovement>();
            yield return new WaitForSeconds(0.1f);
        }

        StartSpawnPrefabs();
    }

    private static Vector2 CalculatePosition(Vector2 start, Vector2 end, float percentage)
    {
        percentage = Mathf.Clamp01(percentage);
        return Vector2.Lerp(start, end, percentage);
    }

    private GameObject SpawnPrefabOnCanvas(Vector2 position)
    {
        GameObject instance = Instantiate(prefab, canvas.transform);
        _ = instance.AddComponent<TowerState>();
        RectTransform rectTransform = instance.GetComponent<RectTransform>();

        if (rectTransform != null)
        {
            rectTransform.position = position;
        }
        else
        {
            Debug.LogWarning("Prefab does not have a RectTransform.");
        }
        return instance;
    }

    private IEnumerator SpawnPrefabs()
    {
        while (waveMovement.startPos == null)
        {
            yield return new WaitForSeconds(0.5f);  // wait for 0.5 seconds. to wait for this to load startPos
        }

        Vector2 firstPosition = CalculatePosition(waveMovement.startPos, waveMovement.targetPosition, WaveConstants.firstlevel);
        Vector2 secondPosition = CalculatePosition(waveMovement.startPos, waveMovement.targetPosition, WaveConstants.secondLevel);

        if (firstInstanse == null)
        {
            firstInstanse = SpawnPrefabOnCanvas(firstPosition);
        }

        if (secondInstanse == null)
        {
            secondInstanse = SpawnPrefabOnCanvas(secondPosition);
        }
    }

    private void StartSpawnPrefabs()
    {
        StartCoroutine(SpawnPrefabs());
    }

    // Method to restore prefabs if they are destroyed
    public void RestorePrefabs()
    {
        if (firstInstanse == null || secondInstanse == null)
        {
            StartSpawnPrefabs();
        }
    }
}
