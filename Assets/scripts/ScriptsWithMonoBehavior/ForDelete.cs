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

            // Добавьте await перед вызовом асинхронных методов
            await ItemService.DeleteAddedItem(dragDropScript.Id);

            bool Cheak = await ItemService.PostItem(new ItemRequest(
                1, dragDropScript.Title, dragDropScript.Description, dragDropScript.Price,
                dragDropScript.Сurrency, dragDropScript.Image, dragDropScript.Place,
                dragDropScript.Health, dragDropScript.Power, dragDropScript.XPower
            ));

            mainCamera.GetComponent<Currency>().Sale(dragDropScript.Сurrency, dragDropScript.Price);

            if (Cheak)
            {
                await refrash.RefreshItemsInShop();
                refrash.RefreshLinePower();
            }

            Destroy(gameObjects);
        }
    }
}