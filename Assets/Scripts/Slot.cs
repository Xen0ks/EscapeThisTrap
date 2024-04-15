using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public string name;
    public string description;
    public void Initialize(ItemData item)
    {
        name = item.name;
        description = item.description;
        GetComponent<Image>().sprite = item.sprite;
        GetComponent<Button>().onClick.AddListener(ShowInfos);
    }

    public void ShowInfos()
    {
        Inventory.instance.ShowInfos(name, description);
    }
}
