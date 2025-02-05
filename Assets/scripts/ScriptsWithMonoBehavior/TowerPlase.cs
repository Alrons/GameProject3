using System.Collections;
using UnityEngine;

public class TowerPlase : MonoBehaviour
{
    public Canvas canvas;
    public GameObject prefab;
    private GameObject firstInstanse;
    private GameObject secondInstanse;

    private static Vector2 CalculatePosition(Vector2 start, Vector2 end, float percentage)
    {
        percentage = Mathf.Clamp01(percentage);
        return Vector2.Lerp(start, end, percentage);
    }

    private GameObject SpawnPrefabOnCanvas(Vector2 position)
    {
        GameObject instance = Instantiate(prefab, canvas.transform);
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

    public void SpawnPrefabs(Vector3 startPosition, Vector3 targetPosition)
    {
        Vector2 firstPosition = CalculatePosition(startPosition, targetPosition, WaveConstants.firstlevel);
        Vector2 secondPosition = CalculatePosition(startPosition, targetPosition, WaveConstants.secondLevel);

        if (firstInstanse == null)
        {
            firstInstanse = SpawnPrefabOnCanvas(firstPosition);
        }

        if (secondInstanse == null)
        {
            secondInstanse = SpawnPrefabOnCanvas(secondPosition);
        }
    }
}
