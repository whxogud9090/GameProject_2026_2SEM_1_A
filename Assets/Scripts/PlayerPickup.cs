using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private float pickupRange = 2f;

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            TryPickup();
        }
    }

    private void TryPickup()
    {
        PickupItem closestItem = FindClosestItem();

        if (closestItem != null)
        {
            closestItem.Collect();
        }
    }

    private PickupItem FindClosestItem()
    {
        PickupItem[] items = FindObjectsByType<PickupItem>(FindObjectsSortMode.None);
        PickupItem closestItem = null;
        float closestDistance = pickupRange;

        foreach (PickupItem item in items)
        {
            float distance = Vector3.Distance(transform.position, item.transform.position);

            if (distance <= closestDistance)
            {
                closestDistance = distance;
                closestItem = item;
            }
        }

        return closestItem;
    }
}
