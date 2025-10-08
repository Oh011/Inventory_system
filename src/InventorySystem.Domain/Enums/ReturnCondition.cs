namespace InventorySystem.Domain.Enums
{
    public enum ReturnCondition
    {
        Good = 0,     // Can be restocked
        Damaged = 1,  // Scrapped or moved to damaged stock
        Expired = 2   // Scrapped
    }
}
