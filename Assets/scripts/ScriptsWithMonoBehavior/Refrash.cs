using Newtonsoft.Json;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class Refrash : MonoBehaviour
{
    public Transform refrashCunvas;
    public GameObject prefabObject;
    public GameObject mainCamera;
    public GameObject contentShop;

    // for future
    //public GameObject Coins;

    public Text title;
    public Text price;
    public Text description;
    public Text health;
    public Text power;
    public Text xPower;

    private SpawnObject spawnObject;


    public void Start()
    {
        spawnObject = mainCamera.GetComponent<SpawnObject>();
        foreach (Transform child in mainCamera.transform)
        {
            if (child.name == "Canvas Place")
            {
                refrashCunvas = child;
            }
        }

    }

    // for future

    //public void RefreshLinePower()
    //{
    //    TableCreator tableCreator = GetComponent<TableCreator>();
    //    foreach (Text txt in tableCreator.textsLinePower)
    //    {
    //        PowerForLine powerForLine = txt.GetComponent<PowerForLine>();
    //        powerForLine.CulculateLine();
    //    }
    //}

    private int FindTableNumber(int NumberCell)
    {
        int totalCells = 0;
        foreach (var table in spawnObject.tableDataModel)
        {
            int tableCells = table.Height * table.Width;
            if (totalCells <= NumberCell && NumberCell <= totalCells + tableCells)
            {
                return table.Id;
            }
            totalCells += tableCells;
        }
        return -1;
    }

    public async Task<bool> RefrachLists()
    {
        ItemService ItemService = new ItemService();

        string AddedItemJson = await ItemService.GetAddedItem();

        spawnObject.addedItemsList = JsonConvert.DeserializeObject<List<AddedItemModel>>(AddedItemJson);

        string ItemJson = await ItemService.GetItem();

        spawnObject.allItems = JsonConvert.DeserializeObject<List<ItemModel>>(ItemJson);

        return true;
    }


    public async Task<bool> RefreshPlaseforDrop()
    {

        if (await RefrachLists())
        {

            TableCreator tableCreator = mainCamera.GetComponent<TableCreator>();
            int Count = 1;

            // Cost refresh (for future)
            //Coins.GetComponent<currency>().ChangeValues();

            foreach (GameObject gameobj in tableCreator.ourCell)
            {

                foreach (Transform children in gameobj.transform)
                {
                    Destroy(children.gameObject);
                }

                for (int i = 0; i < spawnObject.addedItemsList.Count; i++)
                {
                    if (spawnObject.addedItemsList[i].place == Count)
                    {
                        title.text = spawnObject.addedItemsList[i].title;
                        price.text = $"{spawnObject.addedItemsList[i].price}";
                        description.text = spawnObject.addedItemsList[i].description;
                        health.text = $"{spawnObject.addedItemsList[i].health}";
                        power.text = $"{spawnObject.addedItemsList[i].power}";
                        xPower.text = $"{spawnObject.addedItemsList[i].xPower}";
                        GameObject _object = spawnObject.CopyPref(prefabObject, gameobj.transform.position, gameobj.transform);
                        DragDrop script = _object.GetComponent<DragDrop>();
                        script.ThisAddedItem = true;
                        script.Id = spawnObject.addedItemsList[i].id;
                        script.Title = spawnObject.addedItemsList[i].title;
                        script.Description = spawnObject.addedItemsList[i].description;
                        script.Price = spawnObject.addedItemsList[i].price;
                        script.Ñurrency = spawnObject.addedItemsList[i].currency;
                        script.Image = spawnObject.addedItemsList[i].image;
                        script.Place = spawnObject.addedItemsList[i].place;
                        script.Health = spawnObject.addedItemsList[i].health;
                        script.Power = spawnObject.addedItemsList[i].power;
                        script.XPower = spawnObject.addedItemsList[i].xPower;


                        _object.transform.Rotate(0, 0, (float)spawnObject.tableDataModel[FindTableNumber(Count) - 1].Rotate);


                        break;
                    }

                }
                Count++;

            }
            // for future
            //RefreshLinePower();
        }

        return true;
    }



    public async Task<bool> RefreshItemsInShop()
    {
        TableCreator tableCreator = mainCamera.GetComponent<TableCreator>();
        if (await RefrachLists())
        {

            foreach (Transform child in contentShop.transform)
            {
                Destroy(child.gameObject);

            }
            int count = 0;
            foreach (ItemModel Item in spawnObject.allItems)
            {
                if (spawnObject.allItems[count].place <= tableCreator.ourCell.Count)
                {
                    title.text = Item.title;
                    price.text = $"{Item.price}";
                    description.text = Item.description;
                    health.text = $"{Item.health}";
                    power.text = $"{Item.power}";
                    xPower.text = $"{Item.xPover}";

                    GameObject gmItem = spawnObject.CopyPref(prefabObject, new Vector3(0, 0, 0), contentShop.transform);
                    DragDrop dragDrop = gmItem.GetComponent<DragDrop>();
                    dragDrop.Id = spawnObject.allItems[count].id;
                    dragDrop.Title = spawnObject.allItems[count].title;
                    dragDrop.Description = spawnObject.allItems[count].description;
                    dragDrop.Price = spawnObject.allItems[count].price;
                    dragDrop.Ñurrency = spawnObject.allItems[count].currency;
                    dragDrop.Image = spawnObject.allItems[count].image;
                    dragDrop.Place = spawnObject.allItems[count].place;
                    dragDrop.Health = spawnObject.allItems[count].health;
                    dragDrop.Power = spawnObject.allItems[count].power;
                    dragDrop.XPower = spawnObject.allItems[count].xPover;
                }
                count++;
            }
            // for future
            //RefreshLinePower();
        }
        return true;
    }
}
