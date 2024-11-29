using System.Collections;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Transform parentLevel;
    private Vector3 positionBlock;
    public GameObject platform;
    public GameObject startPlatform;

    public GameObject crystal;

    public Transform player;
    public int currentPlatform = 0;

    public float speedFall = 0f;
    private int blockToSpawn = 12;
    private int currentBlock = 0;
    private bool startPlatformFall = false;

    public void Start()
    {
        Instantiate(startPlatform, transform);

        positionBlock = new Vector3(5f, 0f, 5f);

        for (int i = 0; i < blockToSpawn; i++)
        {
            SpawnBlock();
        }
    }

    public void Update()
    {
        if (!GameData.PlayerIsDead)
        {
            currentPlatform = Mathf.CeilToInt(player.position.x) + Mathf.CeilToInt(player.position.z);

            if (parentLevel.childCount > 0)
            {
                if (parentLevel.GetChild(currentBlock).position.x + 1 < player.position.x || parentLevel.GetChild(currentBlock).position.z + 1 < player.position.z)
                {
                    StartCoroutine(GroundFall(parentLevel.GetChild(currentBlock).gameObject));
                    currentBlock++;
                    SpawnBlock();
                }
                if (!startPlatformFall && (transform.GetChild(1).GetChild(transform.GetChild(1).childCount - 2).position.x + 1f < player.position.x || transform.GetChild(1).GetChild(transform.GetChild(1).childCount - 2).position.z + 1f < player.position.z))
                {
                    StartCoroutine(GroundFall(transform.GetChild(1).gameObject));
                    startPlatformFall = true;
                }
            }
        }
    }

    public void SpawnBlock()
    {
        if (platform == null)
        {
            Debug.LogError("No prefabs platform");
            return;
        }

        if (Random.Range(0, 2) == 0)
        {
            positionBlock += Vector3.right;
        }
        else
        {
            positionBlock += Vector3.forward;
        }

        GameObject platformSpawned = Instantiate(platform, positionBlock, Quaternion.identity, parentLevel);

        if (Random.Range(0, 100) < 10)
        {
            SpawnCollectible(CollectibleType.Crystal, platformSpawned.transform.position);
        }
    }

    private void SpawnCollectible(CollectibleType type, Vector3 platformPos)
    {
        switch (type)
        {
            case CollectibleType.Crystal:
                Instantiate(crystal, platformPos + new Vector3(0f, 1.7f, 0f), crystal.transform.rotation, parentLevel);
                break;
            default:
                break;
        }
    }

    private IEnumerator GroundFall(GameObject platform)
    {
        float time = 0f;
        while (time <= 1f)
        {
            time += Time.deltaTime;
            platform.transform.position -= new Vector3(0f, Time.deltaTime * speedFall, 0f);
            yield return null;
        }

        DestroyImmediate(platform);
        currentBlock--;
    }
}
