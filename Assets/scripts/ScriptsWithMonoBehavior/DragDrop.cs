using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    // Since the script is attached to 1 item, the parameters of each item are stored here
    public bool ThisAddedItem { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int Сurrency { get; set; }
    public string Image { get; set; }
    public int Place { get; set; }
    public int Health { get; set; }
    public int Power { get; set; }
    public int XPower { get; set; }

    // get unity objects
    public GameObject dragObject; // item
    public ScrollRect scrollRect;
    public GameObject sciptSpawnObject;
    public GameObject coins;

    DragDropProperties dragDropProperties = new DragDropProperties();

    private Refrash refrash;
    private Currency currency;

    private void Start()
    {
        refrash = sciptSpawnObject.GetComponent<Refrash>();
        currency = coins.GetComponent<Currency>();

    }

    private void FormIsFull()
    {
        if (dragDropProperties.Form.transform.childCount > 0)
        {
            dragDropProperties.FormIsFull = false;
        }
        else
        {
            dragDropProperties.FormIsFull = true;
        }
    }


    private void FindForm()
    {
        if (!dragDropProperties.DidTheFormSearchWork)
        {
            TableCreator tableCreator = sciptSpawnObject.GetComponent<TableCreator>();
            foreach (CellNumberModel cellClass in tableCreator.hashSetCellNumber)
            {
                if (cellClass.cellNumber == Place)
                {
                    dragDropProperties.Form = cellClass.cell;
                }
            }
            dragDropProperties.DidTheFormSearchWork = true;
        }


    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == dragDropProperties.Form)
        {
            Debug.Log("collision: works");
            dragDropProperties.PosNow = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject == dragDropProperties.Form)
        {
            dragDropProperties.PosNow = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        FindForm();
        scrollRect.vertical = false;
        //image.raycastTarget = false;
        dragDropProperties.StartPos = dragObject.transform.position; // We take the coordinates of the initial position and remember
        dragDropProperties.Form.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.6f);




    }

    public void OnDrag(PointerEventData eventData) 
    {
        dragDropProperties.RecetTransform = GetComponent<RectTransform>();
        dragDropProperties.RecetTransform.anchoredPosition += eventData.delta;
    }


     
    IEnumerator CantUseForm()
    {

        dragDropProperties.Form.GetComponent<Image>().color = new Color(255f, 0f, 0f, 0.2f);
        yield return new WaitForSeconds(1);
        dragDropProperties.Form.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.1f);
    }

    public async void OnEndDrag(PointerEventData eventData)
    {
        FormIsFull();
        scrollRect.vertical = true;
        bool Check = false;
        if (dragDropProperties.PosNow)
        {
            if (dragDropProperties.FormIsFull)
            {
                if (currency.currencyValues[Сurrency] >= Price)
                {
                    ItemService itemService = new ItemService();
                    currency.currencyValues[Сurrency] = currency.currencyValues[Сurrency] - Price;
                    bool check = await itemService.PostAddedItem(new AddedItemModel(Id, 1, Title, Description, Price, Сurrency, Image, Place, Health, Power, XPower));
                    Refreshing(check);

                    Destroy(dragObject);
                }
                else
                {
                    Check = true;
                }
            }
        }
        dragDropProperties.Form.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.1f);
        if (Check)
        {
            StartCoroutine(CantUseForm());
        }
        this.transform.position = dragDropProperties.StartPos; 
    }
    private async void Refreshing(bool check)
    {
        ItemService itemService = new ItemService();
        if (check)
        {
            if (await itemService.DeleteItem(Id))
            {
                if (await refrash.RefreshPlaseforDrop())
                {
                    refrash.RefreshItemsInShop();
                }

            }
        }


    }


}
