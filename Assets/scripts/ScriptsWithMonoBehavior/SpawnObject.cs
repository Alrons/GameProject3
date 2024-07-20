using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class SpawnObject : MonoBehaviour
{
    public List<AddedItemModel> addedItemsList = new List<AddedItemModel>();
    public List<ItemModel> AllItems = new List<ItemModel>();
    public List<OurTablesModel> ourTables = new List<OurTablesModel>();


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

    ItemService ItemService = new ItemService();
    public GameObject MainCamera;


    async void Start()
    {
        string AddedItemJson = await ItemService.GetAddedItem();

        addedItemsList = JsonConvert.DeserializeObject<List<AddedItemModel>>(AddedItemJson);



        string ItemJson = await ItemService.GetItem();

        AllItems = JsonConvert.DeserializeObject<List<ItemModel>>(ItemJson);



        string OurTablseJson = await ItemService.GetOurTables();

        ourTables = JsonConvert.DeserializeObject<List<OurTablesModel>>(OurTablseJson);

        Initializing();

    }
    private void Initializing()
    {
        InitializingTable();
    }

    private void InitializingTable()
    {
        TableCreator tableCreator = MainCamera.GetComponent<TableCreator>();
        foreach (OurTablesModel Tables in ourTables)
        {
            tableCreator.CreateTable(Tables.Width, Tables.Height, new Vector3((float)Tables.PosX, (float)Tables.PosY), Tables.Rotate);
        }

    }
}
