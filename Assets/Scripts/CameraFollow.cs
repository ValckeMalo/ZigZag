using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void LateUpdate()
    {
        if (target == null || GameData.PlayerIsDead)
            return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = desiredPosition;

        transform.LookAt(target);
    }
}