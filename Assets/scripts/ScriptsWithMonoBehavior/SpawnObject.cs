using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class SpawnObject : MonoBehaviour
{
    public List<addedItemModel> AddedItemsList = new List<addedItemModel>();
    public List<itemModel> AllItems = new List<itemModel>();
    public List<tableDataModel> OurTables = new List<tableDataModel>();


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

        AddedItemsList = JsonConvert.DeserializeObject<List<addedItemModel>>(AddedItemJson);



        string ItemJson = await ItemService.GetItem();

        AllItems = JsonConvert.DeserializeObject<List<itemModel>>(ItemJson);



        string OurTablseJson = await ItemService.GetOurTables();

        OurTables = JsonConvert.DeserializeObject<List<tableDataModel>>(OurTablseJson);

        Initializing();

    }
    private void Initializing()
    {
        InitializingTable();
    }

    private void InitializingTable()
    {
        TableCreator tableCreator = MainCamera.GetComponent<TableCreator>();
        foreach (tableDataModel Tables in OurTables)
        {
            tableCreator.CreateTable(Tables.Width, Tables.Height, new Vector3((float)Tables.PosX, (float)Tables.PosY), Tables.Rotate);
        }

    }
}
