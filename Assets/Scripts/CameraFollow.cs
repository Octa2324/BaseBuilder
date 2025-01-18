using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform sword;
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (sword.position.y > transform.position.y)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, sword.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
    }
}
