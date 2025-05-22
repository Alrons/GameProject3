using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    // Since the script is attached to 1 item, the parameters of each item are stored here
    public bool IsAddedItem { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int Сurrency { get; set; }
    public string Image { get; set; }
    public int Place { get; set; }
    public string Group { get; set; }
    public int Health { get; set; }
    public int Power { get; set; }
    public int XPower { get; set; }


    // get unity objects
    public GameObject dragObject; // item
    public ScrollRect scrollRect;
    public GameObject mainCamera;

    public GameObject targetCell;

    DragDropProperties dragDropProperties = new DragDropProperties();

    private Refrash refrash;

    //for future
    //private currency currency;

    private void Start()
    {
        
        //mainCamera = SciptSpawnObject.GetComponent<SpawnObject>();
        refrash = mainCamera.GetComponent<Refrash>();
        

        //for future
        //currency = coins.GetComponent<currency>();

    }

    private bool FormIsFull(GameObject form)
    {
        if (targetCell.transform.childCount > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    private void FindForm()
    {
        if (!dragDropProperties.DidTheFormSearchWork)
        {
            TableCreator tableCreator = mainCamera.GetComponent<TableCreator>();
            foreach (CellNumberModel cellClass in tableCreator.hashSetCellNumber)
            {
                if (!string.IsNullOrEmpty(cellClass?.group) && cellClass.group == Group)
                {
                    dragDropProperties.Form.Add(cellClass);
                }
            }
            dragDropProperties.DidTheFormSearchWork = true;
        }


    }

    //ShopItemModel shopItemModel = spawnObject.shopItemModels;


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (dragDropProperties.Form != null)
        {
            foreach (var cell in dragDropProperties.Form)
            {
                if (collision.gameObject == cell.cell)
                {
                    if (dragDropProperties.IsOnEndDrag == true) 
                    {
                        Place = cell.cellNumber;
                    }
                    targetCell = collision.gameObject;
                    dragDropProperties.PosNow = true;
                    break;
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {

        if (dragDropProperties.Form != null)
        {
            foreach (var cell in dragDropProperties.Form)
            {
                if (collision.gameObject == cell.cell)
                {
                    dragDropProperties.PosNow = false;
                    break;
                }
            }
        }
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsAddedItem) { return; }
        FindForm();
        dragDropProperties.IsOnEndDrag = true;
        scrollRect.vertical = false;
        //image.raycastTarget = false;
        dragDropProperties.StartPos = dragObject.transform.position; // We take the coordinates of the initial position and remember
        foreach (var cell in dragDropProperties.Form)
        {
            cell.cell.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.6f);
        }
        
    }

    public void OnDrag(PointerEventData eventData) 
    {
        if (IsAddedItem) { return; }
        dragDropProperties.RecetTransform = GetComponent<RectTransform>();
        dragDropProperties.RecetTransform.anchoredPosition += eventData.delta;
    }
     
    private IEnumerator CantUseForm(GameObject gameObject)
    {

        gameObject.GetComponent<Image>().color = new Color(255f, 0f, 0f, 0.2f);
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.1f);
    }

    public async void OnEndDrag(PointerEventData eventData)
    {
        if (IsAddedItem) { return; }
        scrollRect.vertical = true;
        bool Check = false;
        if (dragDropProperties.PosNow)
        {
            if (FormIsFull(targetCell))
            {
                if (mainCamera.GetComponent<Currency>().Purchase(this.Сurrency,Price))
                {
                    ItemService itemService = new ItemService();
                    
                    bool check = await itemService.PostAddedItem(new AddedItemsRequest(1, Title, Description, Price, Сurrency, Image, Place, Group, Health, Power, XPower));
                    Refreshing(check);
                    Destroy(dragObject);
                }
                    
            }
        }

        foreach (var cell in dragDropProperties.Form)
        {
            cell.cell.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.2f);
        }

        if (Check)
        {
            StartCoroutine(CantUseForm(targetCell));
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
                    await refrash.RefreshItemsInShop();
                }

            }
        }


    }


}
