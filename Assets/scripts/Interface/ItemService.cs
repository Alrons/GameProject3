using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IItemService
{
    Task<string> GetItem();

    Task<string> GetAddedItem();

    Task<string> GetOurTables();

    Task<bool> PostItem(itemModel model);

    Task<bool> PostAddedItem(addedItemModel model);

    Task<bool> DeleteItem(int id);

    Task<bool> DeleteAddedItem(int id);
}
