using System.Threading.Tasks;
public interface IItemService
{
    Task<string> GetItem();

    Task<string> GetAddedItem();

    Task<string> GetOurTables();

    Task<bool> PostItem(ItemRequest model);

    Task<bool> PostAddedItem(AddedItemsRequest model);

    Task<bool> DeleteItem(int id);

    Task<bool> DeleteAddedItem(int id);
}
