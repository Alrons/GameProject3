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
        // Waiting for the WaveMovement component to finish loading
        while (waveMovement == null || !waveMovement.isLoading)
        {
            waveMovement = wave.GetComponent<WaveMovement>();
            yield return new WaitForSeconds(0.1f);
        }

        // Creating prefabs after the waveMovement is fully initialized
        Vector2 firstPosition = CalculatePosition(waveMovement.startPos, waveMovement.targetPosition, WaveConstants.firstlevel);
        Vector2 secondPosition = CalculatePosition(waveMovement.startPos, waveMovement.targetPosition, WaveConstants.secondLevel);

        firstInstanse = SpawnPrefabOnCanvas(firstPosition);
        secondInstanse = SpawnPrefabOnCanvas(secondPosition);
    }

    private void Update()
    {
        if (wave.GetComponent<WaveMovement>().progress >= WaveConstants.firstlevel)
        {
            Destroy(firstInstanse); // Destroying the first instance when progress reaches 0.333
        }
        if (wave.GetComponent<WaveMovement>().progress >= WaveConstants.secondLevel)
        {
            Destroy(secondInstanse); // Destroying the second instance when progress reaches 0.666
        }
    }

    public static Vector2 CalculatePosition(Vector2 start, Vector2 end, float percentage)
    {
        // Clamping the percentage between 0 and 1
        percentage = Mathf.Clamp01(percentage);
        return Vector2.Lerp(start, end, percentage);
    }

    private GameObject SpawnPrefabOnCanvas(Vector2 position)
    {
        // Creating a copy of the prefab as a child of the Canvas
        GameObject instance = Instantiate(prefab, canvas.transform);
        RectTransform rectTransform = instance.GetComponent<RectTransform>();

        if (rectTransform != null)
        {
            // Setting the position of RectTransform to the specified coordinates
            rectTransform.position = position;
        }
        else
        {
            Debug.LogWarning("Prefab does not have a RectTransform.");
        }
        return instance;
    }
}
