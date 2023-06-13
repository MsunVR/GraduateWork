using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSettings : MonoBehaviour
{
    [SerializeField]
    public TMP_Dropdown resolutDropdown;
    public TMP_Dropdown QualityDropdown;

    Resolution[] resolutions;

    void Start()
    {
        resolutDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        for (int i =0; i< resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        resolutDropdown.AddOptions(options);
        resolutDropdown.RefreshShownValue();
        LoadSettings(currentResolutionIndex);

    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void  SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference", QualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference", resolutDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference", System.Convert.ToInt32(Screen.fullScreen));
    }

    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("QualitySettingPreference"))
            QualityDropdown.value = PlayerPrefs.GetInt("QualitySettingPreference");
        else
            QualityDropdown.value = 4; //Цифра обозначает сколько настроек

        if (PlayerPrefs.HasKey("ResolutionPreference"))
            resolutDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
        else
            resolutDropdown.value = currentResolutionIndex;

        if (PlayerPrefs.HasKey("FullscreenPreference"))
            Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        else
            Screen.fullScreen = true;
    }

}
