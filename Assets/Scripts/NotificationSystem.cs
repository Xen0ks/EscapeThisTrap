using UnityEngine;
using UnityEngine.UI;

public class NotificationSystem : MonoBehaviour
{
    public static NotificationSystem instance;

    [SerializeField]
    private Animator itemPanel;

    PlayerController controller;

    [SerializeField]
    Animator smallNotifPanel;
    [SerializeField]
    Image smallNotifImage;
    [SerializeField]
    Text smallNotifText;

    private void Awake()
    {
        instance = this;
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void ObtainItem(string name, string description, Sprite image)
    {
        Time.timeScale = 0;
        controller.enabled = false;
        itemPanel.transform.GetChild(0).GetComponent<Text>().text = name;
        itemPanel.transform.GetChild(1).GetComponent<Text>().text = description;
        itemPanel.transform.GetChild(2).GetComponent<Image>().sprite = image;
        itemPanel.SetBool("Show", true);
    }

    public void CloseObtainItem()
    {
        Time.timeScale = 1;
        controller.enabled = true;
        itemPanel.SetBool("Show", false);
    }

    public void SmallNotif(Sprite sprite, string message)
    {
        smallNotifImage.sprite = sprite;
        smallNotifText.text = message;
        smallNotifPanel.SetTrigger("Show");
    }

}
