using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class SpawnObject : MonoBehaviour
{
    public List<AddedItemModel> addedItemsList = new List<AddedItemModel>();
    public List<ItemModel> allItems = new List<ItemModel>();
    public List<TableDataModel> ourTables = new List<TableDataModel>();


    // for future
    //private int count;

    //public GameObject Box;
    //public Transform CanvasObject;

    //public UnityEngine.UI.Text Title;
    //public UnityEngine.UI.Text Price;
    //public UnityEngine.UI.Text Description;
    //public UnityEngine.UI.Text Health;
    //public UnityEngine.UI.Text Power;
    //public UnityEngine.UI.Text XPower;
    //public GameObject MainCamera;

    
    public GameObject mainCamera;

    async void Start()
    {
        ItemService ItemService = new ItemService();
        string AddedItemJson = await ItemService.GetAddedItem();

        addedItemsList = JsonConvert.DeserializeObject<List<AddedItemModel>>(AddedItemJson);



        string ItemJson = await ItemService.GetItem();

        allItems = JsonConvert.DeserializeObject<List<ItemModel>>(ItemJson);



        string OurTablseJson = await ItemService.GetOurTables();

        ourTables = JsonConvert.DeserializeObject<List<TableDataModel>>(OurTablseJson);

        Initializing();

    }
    private void Initializing()
    {
        InitializingTable();
    }

    private void InitializingTable()
    {
        TableCreator tableCreator = mainCamera.GetComponent<TableCreator>();
        foreach (TableDataModel Tables in ourTables)
        {
            tableCreator.CreateTable(Tables.Width, Tables.Height, new Vector3((float)Tables.PosX, (float)Tables.PosY), Tables.Rotate);
        }

    }
}
