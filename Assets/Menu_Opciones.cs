using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class Menu_Opciones : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixer SFXMixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private float currentRefreshRate;
    private int currentResolutionIndex = 0;
    public Slider slider;
    public float sliderValue;
    public Image panelBrillo;

    public Slider musicSlider;

    public Slider sfxSlider;

    void Start()
    {
        // =========================
        // LUZ
        // =========================

        slider.value = PlayerPrefs.GetFloat("Brillo",0.45f);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, sliderValue);

        // =========================
        // AUDIO
        // =========================
        float musicVolume =
            PlayerPrefs.GetFloat(
                "MusicVolume",
                0.3f
            );

        float sfxVolume =
            PlayerPrefs.GetFloat(
                "SFXVolume",
                1f
            );

        audioMixer.SetFloat(
            "MusicVolume",
            Mathf.Log10(musicVolume) * 20
        );

        SFXMixer.SetFloat(
            "SFXVolume",
            Mathf.Log10(sfxVolume) * 20
        );

        musicSlider.value =
            musicVolume;

        sfxSlider.value =
            sfxVolume;

        // =========================
        // RESOLUCIONES
        // =========================

        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();
        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();

        for (int i = 0; i < filteredResolutions.Count; i++)
        {
           // string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRateRatio + "Hz";
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height ;
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    } 

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }



    public void PantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }


    public void CambiarVolumenMusica(float volumen)
    {
        audioMixer.SetFloat(
            "MusicVolume",
            Mathf.Log10(volumen) * 20
        );

        PlayerPrefs.SetFloat(
            "MusicVolume",
            volumen
        );
    }

    public void CambiarVolumenSFX(float volumen)
    {
        SFXMixer.SetFloat(
            "SFXVolume",
            Mathf.Log10(volumen) * 20
        );

        PlayerPrefs.SetFloat(
            "SFXVolume",
            volumen
        );
    }

    public void CambiarCalidad(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void brillo(float value)
    {
       slider.value = value;
       PlayerPrefs.SetFloat("Brillo", slider.value);
       panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, slider.value);
    }

}