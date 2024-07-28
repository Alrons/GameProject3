using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class SpawnObject : MonoBehaviour
{
    public List<addedItemModel> addedItemsList = new List<addedItemModel>();
    public List<itemModel> allItems = new List<itemModel>();
    public List<tableDataModel> ourTables = new List<tableDataModel>();


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

        addedItemsList = JsonConvert.DeserializeObject<List<addedItemModel>>(AddedItemJson);



        string ItemJson = await ItemService.GetItem();

        allItems = JsonConvert.DeserializeObject<List<itemModel>>(ItemJson);



        string OurTablseJson = await ItemService.GetOurTables();

        ourTables = JsonConvert.DeserializeObject<List<tableDataModel>>(OurTablseJson);

        Initializing();

    }
    private void Initializing()
    {
        InitializingTable();
    }

    private void InitializingTable()
    {
        TableCreator tableCreator = mainCamera.GetComponent<TableCreator>();
        foreach (tableDataModel Tables in ourTables)
        {
            tableCreator.CreateTable(Tables.width, Tables.height, new Vector3((float)Tables.posX, (float)Tables.posY), Tables.rotate);
        }

    }
}
