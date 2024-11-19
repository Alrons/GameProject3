
using UnityEngine;

public class WebSocketMetods : MonoBehaviour 
{
    private Refrash refrash;
    public async void UpdateAllItems (GameObject mainCamera) 
    {
        refrash = mainCamera.GetComponent<Refrash>();
        if (await refrash.RefreshPlaseforDrop())
        {
            await refrash.RefreshItemsInShop();
        }
    }
    public void UpdateWaveStatus(WaveMovement waveMovement, float health, float progress)
    {
        waveMovement.UpdateWaveData(health, progress);
    }
}
