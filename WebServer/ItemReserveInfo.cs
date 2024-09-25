


public class ItemReserveInfo
{
    public WarehouseItem Item => item;
    WarehouseItem item;

    public int AmountReserved => amountReserved;
    int amountReserved;

    public DateTime LastTimeEdited => lastTimeEdited;
    DateTime lastTimeEdited;

    public ItemReserveInfo(WarehouseItem item, int amountToReserve)
    {
        this.item = item;
        amountReserved = amountToReserve;
    }

    /// <summary>
    /// Add amount to reserved stock with given guid.
    /// </summary>
    /// <param name="amountToAdd"> The amount to add to the reserve. </param>
    public void AddAmountToReservedStock(int amountToAdd)
    {
        amountReserved += amountToAdd;
        lastTimeEdited = DateTime.Now;
    }

    /// <summary>
    /// Subtract the amountToSubtract from reserved stock with given guid.
    /// </summary>
    /// <param name="amountToSubtract"> Amount to subtract from the reserved amount. </param>
    public void SubtractAmountFromReservedStock(int amountToSubtract)
    {
        amountReserved -= amountToSubtract;
        lastTimeEdited = DateTime.Now;
    }
}