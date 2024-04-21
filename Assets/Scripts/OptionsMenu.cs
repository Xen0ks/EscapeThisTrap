using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer mixer;

    Resolution[] resolutions;

    [SerializeField]
    private GameObject optionsPanel;

    [SerializeField]
    private Dropdown resolutionDropDown;

    [SerializeField]
    private Slider volumeSlider;

    [SerializeField]
    private Toggle fullScreenToggle;

    private void Start()
    {
        SetupOptions();
    }

    public void SetVolume(float volume)
    {
        mixer.SetFloat("Volume", volume);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            SetFullScreen(!Screen.fullScreen);
        }
    }

    public void ToggleOptionsMenu()
    {
        optionsPanel.SetActive(!optionsPanel.activeSelf);
        SetupOptions();
    }


    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    void SetupOptions()
    {

        // Resolution Setup
        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();
        int currentResolutionIndex = 5;
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }


        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();

        // Volume Setup
        float volume;
        mixer.GetFloat("Volume", out volume);
        volumeSlider.value = volume;

        // FullScreen Setup
        fullScreenToggle.isOn = Screen.fullScreen;
    }
}
