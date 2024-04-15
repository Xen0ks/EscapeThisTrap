using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<ItemData> inventory = new List<ItemData>();

    // References
    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private Transform slotsParent;
    [SerializeField]
    private Slot slotPrefab;

    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text descriptionText;

    [SerializeField]
    private AudioClip itemObtainSfx;
    AudioSource audio;

    private PlayerController controller;

    // Pause Menu
    [SerializeField]
    private GameObject pauseMenu;

    private void Awake()
    {
        instance = this;
        RefreshContent();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }



    void ToggleInventory()
    {
        if (pauseMenu.activeSelf || !inventoryPanel.activeSelf && Time.timeScale == 0) return;

        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if(inventoryPanel.activeSelf)
        {
            controller.enabled = false;
        }
        else
        {
            controller.enabled = true;
        }
    }

    public void AddItem(ItemData item)
    {
        inventory.Add(item);
        RefreshContent();
        audio.PlayOneShot(itemObtainSfx);
    }

    public void RemoveItem(ItemData item)
    {
        if (inventory.Contains(item))
        {
            inventory.Remove(item);
        }
        else
        {
            // Debug.Log("Doesn't have this item but you want to remove it, you are kinda dumb");
        }

        RefreshContent();
    }

    void RefreshContent()
    {
        foreach (Transform slot in slotsParent)
        {
            Destroy(slot.gameObject);
        }

        foreach (ItemData item in inventory)
        {
            Slot slot = Instantiate(slotPrefab, slotsParent);
            slot.Initialize(item);
        }
    }

    public void ShowInfos(string name, string description)
    {
        nameText.text = name;
        descriptionText.text = description;
    }


    // Pause Menu
    public void TogglePauseMenu()
    {
        if (inventoryPanel.activeSelf || !pauseMenu.activeSelf && Time.timeScale == 0) return;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (pauseMenu.activeSelf)
        {
            controller.enabled = false;
        }
        else
        {
            controller.enabled = true;
        }
    }

    public void Options()
    {

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
