using UnityEngine;

public class WaveState : MonoBehaviour
{
    public int waveState;
    //0 = stay
    //1 = moving

    public void DestroyObject()
    {
        // code before destroyed object

        Destroy(transform.gameObject);
    }
}
