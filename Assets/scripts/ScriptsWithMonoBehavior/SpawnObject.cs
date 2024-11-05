using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using UnityEngine.UI;


public class SpawnObject : MonoBehaviour
{
    public List<AddedItemModel> addedItemsList = new List<AddedItemModel>();
    public List<ItemModel> allItems = new List<ItemModel>();
    public List<TableDataModel> tableDataModel = new List<TableDataModel>();

    private int count;

    public Text title;
    public Text price;
    public Text description;
    public Text health;
    public Text power;
    public Text xPower;

    public GameObject box;
    public Transform canvasObject;
    public GameObject mainCamera;

    async void Start()
    {
        ItemService ItemService = new ItemService();

        string AddedItemJson = await ItemService.GetAddedItem();
        BaseResponse<List<AddedItemModel>> baseResponseAddedItem = JsonConvert.DeserializeObject<BaseResponse<List<AddedItemModel>>>(AddedItemJson);
        addedItemsList = baseResponseAddedItem.Result;

        string ItemJson = await ItemService.GetItem();
        BaseResponse<List<ItemModel>> baseResponseItems = JsonConvert.DeserializeObject<BaseResponse<List<ItemModel>>>(ItemJson);
        allItems = baseResponseItems.Result;

        string TablseJson = await ItemService.GetOurTables();
        BaseResponse <List<TableDataModel>> baseResponseTable = JsonConvert.DeserializeObject<BaseResponse<List<TableDataModel>>>(TablseJson);
        tableDataModel = baseResponseTable.Result;

        Initializing();
    }
    private void Initializing()
    {

        InitializingTable();
        InitializingAddedItems();
        InitializingItems();

        Refrash refrash = mainCamera.GetComponent<Refrash>();
        refrash.RefreshLinePower();
    }
    public GameObject CopyPref(GameObject box, Vector3 position, Transform setparent)
    {
        var spawn = Instantiate(box, position, Quaternion.identity);
        spawn.transform.SetParent(setparent.transform);
        spawn.transform.Rotate(0, 0, 0);
        spawn.transform.localScale = new Vector3(1, 1, 1);
        return spawn;

    }
    private void ChangePref(string title, int price, string description, int health, double power, double xpower)
    {
        this.title.text = title;
        this.price.text = string.Format("{0}", price);
        this.description.text = description;
        this.health.text = string.Format("{0}", health);
        this.power.text = string.Format("{0}", power);
        xPower.text = String.Format("{0}", xpower);
    }

    private void InitializingItems()
    {
        for (int i = 0; i < allItems.Count; i++)
        {
            if (count != allItems.Count)
            {
                TableCreator tableCreator = mainCamera.GetComponent<TableCreator>();
                if (allItems[i].place <= tableCreator.hashSetCellNumber.Count)
                {
                    ChangePref(allItems[i].title, allItems[i].price, allItems[i].description, allItems[i].health, allItems[i].power, allItems[i].xPover);
                    GameObject gmItem = CopyPref(box, box.transform.position, canvasObject);
                    DragDrop dragDrop = gmItem.GetComponent<DragDrop>();
                    dragDrop.Id = allItems[i].id;
                    dragDrop.Title = allItems[i].title;
                    dragDrop.Description = allItems[i].description;
                    dragDrop.Price = allItems[i].price;
                    dragDrop.Ñurrency = allItems[i].currency;
                    dragDrop.Image = allItems[i].image;
                    dragDrop.Place = allItems[i].place;
                    dragDrop.Health = allItems[i].health;
                    dragDrop.Power = allItems[i].power;
                    dragDrop.XPower = allItems[i].xPover;
                }

            }

        }
    }
    private void InitializingTable()
    {
        TableCreator tableCreator = mainCamera.GetComponent<TableCreator>();
        foreach (TableDataModel Tables in tableDataModel)
        {

            tableCreator.CreateTable(Tables.Width, Tables.Height, new Vector3((float)Tables.PosX, (float)Tables.PosY), Tables.Rotate);
        }

    }
    private int FindTableNumber(int NumberCell)
    {
        TableCreator tableCreator = mainCamera.GetComponent<TableCreator>();
        foreach (CellNumberModel cellClass in tableCreator.hashSetCellNumber)
        {
            if (cellClass.cellNumber == NumberCell)
            {
                return cellClass.tableNumber;
            }
        }
        return -1;
    }

    private void InitializingAddedItems()
    {

        TableCreator tableCreator = mainCamera.GetComponent<TableCreator>();
        int Count = 1;
        foreach (CellNumberModel Cell in tableCreator.hashSetCellNumber)
        {
            GameObject gameobj = Cell.cell;
            foreach (Transform children in gameobj.transform)
            {
                Destroy(children.gameObject);
            }

            for (int i = 0; i < addedItemsList.Count; i++)
            {
                if (addedItemsList[i].place == Count)
                {
                    title.text = addedItemsList[i].title;
                    price.text = $"{addedItemsList[i].price}";
                    description.text = addedItemsList[i].description;
                    health.text = $"{addedItemsList[i].health}";
                    power.text = $"{addedItemsList[i].power}";
                    xPower.text = $"{addedItemsList[i].xPower}";
                    GameObject newpref = CopyPref(box, gameobj.transform.position, gameobj.transform);
                    DragDrop script = newpref.GetComponent<DragDrop>();
                    script.ThisAddedItem = true;
                    script.Id = addedItemsList[i].id;
                    script.Title = addedItemsList[i].title;
                    script.Description = addedItemsList[i].description;
                    script.Price = addedItemsList[i].price;
                    script.Ñurrency = addedItemsList[i].currency;
                    script.Image = addedItemsList[i].image;
                    script.Place = addedItemsList[i].place;
                    script.Health = addedItemsList[i].health;
                    script.Power = addedItemsList[i].power;
                    script.XPower = addedItemsList[i].xPower;
                    newpref.transform.Rotate(0, 0, (float)tableDataModel[FindTableNumber(Count) - 1].Rotate);

                    break;
                }

            }
            Count++;

        }
    }
}
