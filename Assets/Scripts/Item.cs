using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public ItemData item;
    public UnityEvent onItemObtained;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TakeItem();
        }
    }

    void TakeItem()
    {
        NotificationSystem.instance.ObtainItem(item.name, item.description, item.sprite);
        Inventory.instance.AddItem(item);
        onItemObtained.Invoke();
        Destroy(gameObject);
    }
}
