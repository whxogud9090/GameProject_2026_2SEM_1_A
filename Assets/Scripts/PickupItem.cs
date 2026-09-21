using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 120f;

    private void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
    }

    public void Collect()
    {
        Destroy(gameObject);
    }
}
