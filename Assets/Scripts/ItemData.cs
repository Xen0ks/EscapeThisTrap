
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Item/CreateItem")]
public class ItemData : ScriptableObject
{
    public string name;
    [TextArea(10,5)]
    public string description;
    public Sprite sprite;
}
