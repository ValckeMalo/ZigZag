public interface ICollectible
{
    /// <summary>
    /// return the score amount and the type of the collectible
    /// </summary>
    /// <returns></returns>
    public (CollectibleType, int) Collect();
}
public enum CollectibleType
{
    Crystal,
    Count,
}