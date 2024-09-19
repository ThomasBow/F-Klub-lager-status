


public class ReserveInfo
{
    public string Guid => guid;
    string guid = System.Guid.NewGuid().ToString();

    public int AmountReserved => amountReserved;
    int amountReserved;

    public string Reason => reason;
    string reason;

    public readonly DateTime TimeReserved;
    public DateTime LastTimeEdited => lastTimeEdited;
    DateTime lastTimeEdited;

    public ReserveInfo(string guid, int amountToReserve, string reason)
    {
        this.guid = guid;

        amountReserved = amountToReserve;
        this.reason = reason;

        DateTime now = DateTime.Now;
        TimeReserved = now;
        lastTimeEdited = now;
    }

    /// <summary>
    /// Add amount to reserved stock with given guid.
    /// </summary>
    /// <param name="amountToAdd"> The amount to add to the reserve. </param>
    /// <param name="guid"> The guid of the reserve to edit. </param>
    public void AddAmountToReservedStock(int amountToAdd)
    {
        amountReserved += amountToAdd;
        lastTimeEdited = DateTime.Now;
    }

    /// <summary>
    /// Subtract the amountToSubtract from reserved stock with given guid.
    /// </summary>
    /// <param name="amountToSubtract"> Amount to add to the reserved amount. </param>
    /// <param name="guid"> The guid of the reserve to edit. </param>
    public void SubtractAmountFromReservedStock(int amountToSubtract)
    {
        amountReserved -= amountToSubtract;
        lastTimeEdited = DateTime.Now;
    }
}