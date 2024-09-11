using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IItemService
{
    Task<string> GetItem();

    Task<string> GetAddedItem();

    Task<string> GetOurTables();

    Task<bool> PostItem(ItemModel model);

    Task<bool> PostAddedItem(AddedItemModel model);

    Task<bool> DeleteItem(int id);

    Task<bool> DeleteAddedItem(int id);
}
