

public class WarehouseItem
{
    public string Identifier => identifier;
    string identifier;

    public string Name => name;
    string name;

    public int Amount => amount;
    int amount;

    public int AmountAddedOverTime => amountAddedOverTime;
    int amountAddedOverTime;
    
    public int AmountRemovedOverTime => amountRemovedOverTime;
    int amountRemovedOverTime;

    public WarehouseItem(string identifier, string? name, int amount = 0)
    {
        this.identifier = identifier;

        this.name = name ?? identifier;

        this.amount = amount;
    }

    /// <summary>
    /// Adds the amountToAdd to internal amount.
    /// </summary>
    /// <param name="amountToAdd"> Amount to add to internal amount. </param>
    /// <returns> The new effective amount. </returns>
    public void AddStock(int amountToAdd)
    {
        amount += amountToAdd;
    }

    /// <summary>
    /// Subtracts the amountToSubtract from internal amount.
    /// </summary>
    /// <param name="amountToSubtract"> Amount to subtract from internal amount. </param>
    /// <returns> The new effective amount. </returns>
    public void SubtractStock(int amountToSubtract)
    {
        amount -= amountToSubtract;
    }
}