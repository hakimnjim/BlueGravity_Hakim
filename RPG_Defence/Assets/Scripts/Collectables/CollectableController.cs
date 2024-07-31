using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    [SerializeField] private Item item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (item != null)
            {
                GlobalEventManager.TriggerPickupItem(item);
                Destroy(gameObject);
            }
        }
    }
}
