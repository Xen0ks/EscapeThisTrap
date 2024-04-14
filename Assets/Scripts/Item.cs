using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public ItemData item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TakeItem();
        }
    }

    void TakeItem()
    {
        item.onItemObtained.Invoke();
        Destroy(gameObject);
    }
}
[System.Serializable]
public class ItemData
{
    public string name;
    public string description;
    public UnityEvent onItemObtained;
}
