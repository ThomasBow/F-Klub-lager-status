

public class WarehouseItem
{
    public readonly string Identifier;

    public string Name => name;
    string name;

    public int Amount => amount;
    int amount;


    public WarehouseItem(string identifier, string? name, int amount = 0)
    {
        Identifier = identifier;

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