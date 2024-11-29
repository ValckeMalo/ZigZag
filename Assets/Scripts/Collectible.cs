using UnityEngine;

public class Collectible : MonoBehaviour, ICollectible
{
    [Header("Data")]
    [SerializeField] private CollectibleType type;
    [SerializeField] private int scoreAmount = 2;

    public (CollectibleType, int) Collect()
    {
        Destroy(gameObject);
        return (type, scoreAmount);
    }
}