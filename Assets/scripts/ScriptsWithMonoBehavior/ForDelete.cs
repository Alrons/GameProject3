using UnityEngine.EventSystems;
using UnityEngine;


public class ForDelete : MonoBehaviour, IPointerClickHandler
{
    public GameObject gameObjects;
    public GameObject mainCamera;
    public GameObject Coins;
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
            Debug.Log(dragDropScript.Сurrency);
            Coins.GetComponent<Currency>().currencyValues[dragDropScript.Сurrency] = Coins.GetComponent<Currency>().currencyValues[dragDropScript.Сurrency] + (dragDropScript.Price / 2);
            Coins.GetComponent<Currency>().ChangeValues();
            ItemService ItemService = new ItemService();
            ItemService.DeleteAddedItem(dragDropScript.Id);
            bool Cheak = await ItemService.PostItem(new ItemModel(dragDropScript.Id, dragDropScript.Title, dragDropScript.Description, dragDropScript.Price, dragDropScript.Сurrency, dragDropScript.Image, dragDropScript.Place, dragDropScript.Health, dragDropScript.Power, dragDropScript.XPower));

            if (Cheak)
            {
                refrash.RefreshItemsInShop();
                refrash.RefreshLinePower();
            }

            Destroy(gameObjects);
        }

    }
}