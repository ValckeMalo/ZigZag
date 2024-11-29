using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction = Vector3.right;
    [SerializeField] private float speed;
    private bool isFalling = false;

    public void Start()
    {
        direction = Vector3.right;
    }

    private void Update()
    {
        if (GameData.Instance.isPlay)
        {
            transform.position += direction * speed * Time.deltaTime;

            if (!isFalling)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ChangeDirection();
                    GameData.Instance.AddScore(1);
                }

                isFalling = !Physics.Raycast(transform.position, Vector3.down, 1f);
                if (isFalling)
                {
                    GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                    GameData.PlayerIsDead = true;
                }
            }
            else
            {
                if (transform.position.y <= 5f)
                {
                    GameData.Instance.ResetGame();
                }
            }
        }
    }

    private void ChangeDirection()
    {
        if (direction == Vector3.right)
        {
            direction = Vector3.forward;
        }
        else
        {
            direction = Vector3.right;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ICollectible collectible = other.GetComponent<ICollectible>();
        if (collectible != null)
        {
            (CollectibleType type, int scoreAmount) value = collectible.Collect();

            GameData.Instance.AddColectible(value.type);
            GameData.Instance.AddScore(value.scoreAmount);
        }
    }
}