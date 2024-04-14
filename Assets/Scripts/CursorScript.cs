using UnityEngine;

public class CursorScript : MonoBehaviour
{

    private void Start()
    {
        // Cursor.visible = false;
    }
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }
}
