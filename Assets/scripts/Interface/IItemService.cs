using System.Collections.Generic;
using System.Threading.Tasks;
public interface IItemService
{
    Task<List<ItemModel>> GetItem();

    Task<List<AddedItemModel>> GetAddedItem();

    Task<bool> PostItem(ItemRequest model);

    Task<bool> PostAddedItem(AddedItemsRequest model);

    Task<bool> DeleteItem(int id);

    Task<bool> DeleteAddedItem(int id);
}
