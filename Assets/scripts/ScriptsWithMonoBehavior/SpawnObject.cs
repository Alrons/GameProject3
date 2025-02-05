using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SpawnObject : MonoBehaviour
{
    public List<AddedItemModel> addedItemsList = new();
    public List<ItemModel> allItems = new();
    public List<TableDataModel> tableDataModel = new();

    public Text title, price, description, health, power, xPower;
    public GameObject box;
    public Transform canvasObject;
    public GameObject mainCamera;

    public bool isLoading = false;

    private async void Start()
    {
        var itemService = new ItemService();

        addedItemsList = await itemService.GetAddedItem();
        allItems = await itemService.GetItem();
        tableDataModel = await itemService.GetOurTables();

        if (addedItemsList == null || allItems == null || tableDataModel == null)
        {
            Debug.LogError("Îøèáêà çàãðóçêè äàííûõ! Ïðîâåðü API.");
            return;
        }

        Initialize();
    }


    private void Initialize()
    {
        InitializeTables();
        InitializeAddedItems();
        InitializeItems();

        mainCamera.GetComponent<Refrash>().RefreshLinePower();
        isLoading = true;
    }

    public GameObject CopyPref(GameObject prefab, Vector3 position, Transform parent)
    {
        if (prefab == null)
        {
            Debug.LogError("CopyPref: prefab is NULL!");
            return null;
        }

        if (parent == null)
        {
            Debug.LogError("CopyPref: parent is NULL!");
            return null;
        }

        GameObject instance = Instantiate(prefab, position, Quaternion.identity, parent);
        instance.transform.localScale = Vector3.one;
        return instance;
    }

    private void UpdateTextFields(string title, int price, string description, int health, double power, double xpower)
    {
        this.title.text = title;
        this.price.text = price.ToString();
        this.description.text = description;
        this.health.text = health.ToString();
        this.power.text = power.ToString();
        this.xPower.text = xpower.ToString();
    }

    private void InitializeItems()
    {
        var tableCreator = mainCamera.GetComponent<TableCreator>();

        foreach (var item in allItems)
        {
            if (item.place > tableCreator.hashSetCellNumber.Count) continue;

            UpdateTextFields(item.title, item.price, item.description, item.health, item.power, item.xPover);
            GameObject gmItem = CopyPref(box, box.transform.position, canvasObject);

            if (gmItem.TryGetComponent(out DragDrop dragDrop))
            {
                dragDrop.Id = item.id;
                dragDrop.Title = item.title;
                dragDrop.Description = item.description;
                dragDrop.Price = item.price;
                dragDrop.Ñurrency = item.currency;
                dragDrop.Image = item.image;
                dragDrop.Place = item.place;
                dragDrop.Health = item.health;
                dragDrop.Power = item.power;
                dragDrop.XPower = item.xPover;
            }
        }
    }

    private void InitializeTables()
    {
        var tableCreator = mainCamera.GetComponent<TableCreator>();

        foreach (var table in tableDataModel)
        {
            tableCreator.CreateTable(table.Width, table.Height, new Vector3((float)table.PosX, (float)table.PosY), table.Rotate);
        }
    }

    private int FindTableNumber(int cellNumber)
    {
        var tableCreator = mainCamera.GetComponent<TableCreator>();

        foreach (var cell in tableCreator.hashSetCellNumber)
        {
            if (cell.cellNumber == cellNumber)
                return cell.tableNumber;
        }

        return -1;
    }

    private void InitializeAddedItems()
    {
        var tableCreator = mainCamera.GetComponent<TableCreator>();

        foreach (var item in addedItemsList)
        {
            int place = item.place;

            var cell = tableCreator.hashSetCellNumber.FirstOrDefault(c => c.cellNumber == place);

            if (cell == null)
            {
                continue;
            }

            foreach (Transform child in cell.cell.transform)
            {
                Destroy(child.gameObject);
            }

            UpdateTextFields(item.title, item.price, item.description, item.health, item.power, item.xPower); 
            GameObject newPrefab = CopyPref(box, cell.cell.transform.position, cell.cell.transform);

            if (newPrefab == null)
            {
                Debug.LogError($"InitializeAddedItems: Failed to instantiate prefab for place {place}");
                return;
            }

            if (newPrefab.TryGetComponent(out DragDrop script))
            {
                script.ThisAddedItem = true;
                script.Id = item.id;
                script.Title = item.title;
                script.Description = item.description;
                script.Price = item.price;
                script.Ñurrency = item.currency;
                script.Image = item.image;
                script.Place = item.place;
                script.Health = item.health;
                script.Power = item.power;
                script.XPower = item.xPower; 
            }

            int tableIndex = FindTableNumber(place) - 1;
            if (tableIndex >= 0 && tableIndex < tableDataModel.Count)
            {
                newPrefab.transform.Rotate(0, 0, (float)tableDataModel[tableIndex].Rotate);
            }
        }
    }


}
