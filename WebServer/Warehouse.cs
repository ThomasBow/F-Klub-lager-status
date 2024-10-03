


using WebServer.Exceptions;

public class Warehouse
{
    readonly object _locker = new();

    List<ReserveInfo> reserveInfos = new();

    public List<WarehouseItem> Items => new(items);
    List<WarehouseItem> items = new();

    public void OnChangeMade() => lastChange = DateTime.Now;
    public DateTime lastChange = DateTime.Now;

    public void AddNewItem(string identifier, string name)
    {
        lock (_locker)
        {
            if (ItemExists(identifier)) throw new AlreadyExistsException();

            items.Add( new(identifier, name) );

            OnChangeMade();
        }
    }

    public void AddAmountToItem(string identifier, int amount)
    {
        lock (_locker)
        {
            FindItem(identifier)
                .AddStock(amount);

            OnChangeMade();
        }
    }

    public void SubtractAmountFromItem(string identifier, int amount) 
    {
        lock (_locker)
        {
            FindItem(identifier)
                .SubtractStock(amount);

            OnChangeMade();
        }
    }

    public void AddNewItemToReserve(string guid, string itemIdentifier, int amountToReserve)
    {
        lock (_locker)
        {
            WarehouseItem item = FindItem(itemIdentifier);

            FindReserveInfo(guid)
                .AddItemToReserve(item, amountToReserve);

            OnChangeMade();
        }
    }

    public void AddReservedAmountToItem(string guid, string itemIdentifier, int amount)
    {
        lock (_locker)
        {
            FindReserveInfo(guid)
                .AddAmountToReservedStock(amount, itemIdentifier);

            OnChangeMade();
        }
    }

    public void SubtractReservedAmountFromItem(string guid, string itemIdentifier, int amount)
    {
        lock (_locker)
        {
            FindReserveInfo(guid)
                .SubtractAmountFromReservedStock(amount, itemIdentifier);

            OnChangeMade();
        }
    }

    public void DeleteItemFromReserve(string guid, string itemIdentifier)
    {
        lock (_locker)
        {
            FindReserveInfo(guid)
                .RemoveItemFromReserve(itemIdentifier);

            OnChangeMade();
        }
            
    }

    public void DeleteItem(string identifier)
    {
        lock (_locker)
        {
            WarehouseItem item = FindItem(identifier);
            items.Remove(item);

            OnChangeMade();
        }
    }

    private bool ItemExists(string identifier) =>
        items.Exists(item => item.Identifier == identifier);

    public WarehouseItem FindItem(string identifier) => 
        items.FirstOrDefault(item => item.Identifier == identifier) ?? throw new NotFoundException();

    private ReserveInfo FindReserveInfo(string guid) =>
        reserveInfos.FirstOrDefault(reserveInfo => reserveInfo.Guid == guid) ?? throw new NotFoundException();
}