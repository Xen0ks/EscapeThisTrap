using UnityEngine;
using UnityEngine.EventSystems;

public class GetBiggerOnPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Vector3 scaleSelected = new Vector3(1.2f,1.2f,1.2f);

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = scaleSelected;
        Invoke("Smaller", 2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Smaller();
    }

    void Smaller()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}