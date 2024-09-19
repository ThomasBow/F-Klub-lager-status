

public class WarehouseItem
{
    public string Identifier => identifier;
    string identifier;

    public string Name => name;
    string name;

    public int Amount => amount;
    int amount;

    public List<ReserveInfo> Reserves => new(reserves);

    List<ReserveInfo> reserves;


    public WarehouseItem(string identifier, string? name = null, int amount = 0)
    {
        this.identifier = identifier;

        this.name = name ?? identifier;

        this.amount = amount;

        reserves = new();
    }

    public int GetEffectiveAmount()
    {
        int amount = this.amount;
        foreach (ReserveInfo reserve in reserves)
        {
            amount -= reserve.AmountReserved;
        }
        return amount;
    }

    /// <summary>
    /// Adds the amountToAdd to internal amount.
    /// </summary>
    /// <param name="amountToAdd"> Amount to add to internal amount. </param>
    /// <returns> The new effective amount. </returns>
    public int AddStock(int amountToAdd)
    {
        amount += amountToAdd;
        return GetEffectiveAmount();
    }

    /// <summary>
    /// Subtracts the amountToSubtract from internal amount.
    /// </summary>
    /// <param name="amountToSubtract"> Amount to subtract from internal amount. </param>
    /// <returns> The new effective amount. </returns>
    public int SubtractStock(int amountToSubtract)
    {
        amount -= amountToSubtract;
        return GetEffectiveAmount();
    }

    /// <summary>
    /// Add a new entry of reserved stock.
    /// </summary>
    /// <param name="amountToReserve"> Amount to add to the reserved amount. </param>
    /// <param name="reason"> The reason for the reserved stock. </param>
    /// <returns> The new effective amount. </returns>
    public int AddNewReservedStock(int amountToReserve, string reason)
    {
        DateTime now = DateTime.Now;
        ReserveInfo reserve = new()
        {
             = amountToReserve,
            Reason = reason,

            timeReserved = now,
            LastTimeEdited = now,
        };
        reserves.Add(reserve);

        return GetEffectiveAmount();
    }

    /// <summary>
    /// Add amount to reserved stock with given guid.
    /// </summary>
    /// <param name="amountToAdd"> The amount to add to the reserve. </param>
    /// <param name="guid"> The guid of the reserve to edit. </param>
    /// <returns> The new effective amount. </returns>
    public int AddAmountToReservedStock(int amountToAdd, string guid)
    {
        ReserveInfo reserve = GetReserve(guid);
        reserve.AddAmountToReservedStock(amountToAdd);

        return GetEffectiveAmount();
    }

    /// <summary>
    /// Subtract the amountToSubtract from reserved stock with given guid.
    /// </summary>
    /// <param name="amountToSubtract"> Amount to add to the reserved amount. </param>
    /// <param name="guid"> The guid of the reserve to edit. </param>
    /// <returns> The new effective amount. </returns>
    public int SubtractAmountFromReservedStock(int amountToSubtract, string guid)
    {
        ReserveInfo reserve = GetReserve(guid);
        reserve.SubtractAmountFromReservedStock(amountToSubtract);
        return GetEffectiveAmount();
    }

    /// <summary>
    /// Find the reserve info with the given guid.
    /// </summary>
    /// <param name="guid"> The guid of the reserve info to get. </param>
    /// <returns> A reserve info with the given guid. (Throws exception if non-existant) </returns>
    private ReserveInfo GetReserve(string guid) =>
        reserves.First(reserve => reserve.Guid == guid);
}