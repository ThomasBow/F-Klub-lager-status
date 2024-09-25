


using WebServer.Exceptions;

public class ReserveInfo
{
    public string Guid => guid;
    string guid = System.Guid.NewGuid().ToString();

    List<ItemReserveInfo> itemsReserved = new();

    public string Reason => reason;
    string reason;

    public readonly DateTime TimeReserved;
    public DateTime LastTimeEdited => lastTimeEdited;
    DateTime lastTimeEdited;

    public ReserveInfo(string guid, string reason)
    {
        this.guid = guid;

        this.reason = reason;

        DateTime now = DateTime.Now;
        TimeReserved = now;
        lastTimeEdited = now;
    }

    /// <summary>
    /// Add amount to reserved stock with given guid.
    /// </summary>
    /// <param name="amountToAdd"> The amount to add to the reserve. </param>
    /// <param name="itemIdentifier"> The identifier of the item reserve to edit. </param>
    public void AddAmountToReservedStock(int amountToAdd, string itemIdentifier)
    {
        FindItemReserveInfo(itemIdentifier)
            .AddAmountToReservedStock(amountToAdd);
        lastTimeEdited = DateTime.Now;
    }

    /// <summary>
    /// Subtract the amountToSubtract from reserved stock with given guid.
    /// </summary>
    /// <param name="amountToSubtract"> Amount to add to the reserved amount. </param>
    /// <param name="itemIdentifier"> The identifier of the item reserve to edit. </param>
    public void SubtractAmountFromReservedStock(int amountToSubtract, string itemIdentifier)
    {
        FindItemReserveInfo(itemIdentifier)
            .SubtractAmountFromReservedStock(amountToSubtract); 
        lastTimeEdited = DateTime.Now;
    }

    public void AddItemToReserve(WarehouseItem item, int amount)
    {
        if (ItemReserveInfoExists(item.Identifier)) throw new AlreadyExistsException();

        itemsReserved.Add(
            new(
                item,
                amount
            )
        );
    }

    public void RemoveItemFromReserve(string itemIdentifier)
    {
        itemsReserved.Remove(
            FindItemReserveInfo(itemIdentifier)
        );
    }

    /// <summary>
    /// Find the itemReserveInfo with the item with the given identifier.
    /// </summary>
    /// <param name="itemIdentifier"> The identifier of the item in the reserve. </param>
    /// <returns> An itemRerserveInfo with an item with the given identifier. </returns>
    public ItemReserveInfo FindItemReserveInfo(string itemIdentifier) =>
        itemsReserved.First(x => x.Item.Identifier == itemIdentifier);

    public bool ItemReserveInfoExists(string itemIdentifier) =>
        itemsReserved.Exists(x => x.Item.Identifier == itemIdentifier);

}