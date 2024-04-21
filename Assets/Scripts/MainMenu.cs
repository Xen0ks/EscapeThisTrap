using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject creditsPanel;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ToggleCredits()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }

    public void Xen0ks()
    {
        Application.OpenURL("https://www.youtube.com/@Xen0ks");
    }

    public void BeyondBaka()
    {
        Application.OpenURL("https://www.youtube.com/@BeyondBAKA-wd6fd");
    }
}
