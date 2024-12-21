using UnityEngine;

public class PrefabCollisionHandler : MonoBehaviour
{
    public GameObject wave; 
    public float detectionRadius = 0.5f; 

    private void Update()
    {
        CheckForCollision();
    }

    private void CheckForCollision()
    {
        // Determining the position of the current object (prefab) and the wave object
        Vector2 prefabPosition = transform.position;
        Vector2 wavePosition = wave.transform.position;

        // Checking if the prefab object is within the radius of the wave object
        Collider2D[] colliders = Physics2D.OverlapCircleAll(prefabPosition, detectionRadius);
        foreach (var collider in colliders)
        {
            if (collider.gameObject == wave)
            {
                gameObject.GetComponent<TowerState>().DestroyObject(); // Destroy the prefab
                break;               // Break the loop as the prefab is already destroyed
            }
        }
    }

    // Visualizing the collision radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
