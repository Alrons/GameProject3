using UnityEngine.EventSystems;
using UnityEngine;


public class ForDelete : MonoBehaviour, IPointerClickHandler
{
    public GameObject gameObjects;
    public GameObject mainCamera;
    private DragDrop dragDropScript;
    private Refrash refrash;


    private void Start()
    {
        dragDropScript = gameObjects.GetComponent<DragDrop>();
        refrash = mainCamera.GetComponent<Refrash>();
    }
    public async void OnPointerClick(PointerEventData eventData)
    {
        if (dragDropScript.ThisAddedItem)
        {
            ItemService ItemService = new ItemService();
            ItemService.DeleteAddedItem(dragDropScript.Id);
            bool Cheak = await ItemService.PostItem(new ItemModel(dragDropScript.Id, dragDropScript.Title, dragDropScript.Description, dragDropScript.Price, dragDropScript.Сurrency, dragDropScript.Image, dragDropScript.Place, dragDropScript.Health, dragDropScript.Power, dragDropScript.XPower));

            if (Cheak)
            {
                refrash.RefreshItemsInShop();
            }

            Destroy(gameObjects);
        }

    }
}