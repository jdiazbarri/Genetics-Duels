using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    // =========================
    // Audio
    // =========================

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixer SFXMixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    public Slider musicSlider;
    public Slider sfxSlider;

    // =========================
    // Resolución
    // =========================

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    // =========================
    // Brillo
    // =========================

    public Slider slider;
    public float sliderValue;
    public Image brightnessPanel;

    // Inicializa todas las opciones guardadas previamente por el jugador
    void Start()
    {
        InitializeBrightness();
        InitializeAudio();
        InitializeResolutions();
    } 

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    // Activa o desactiva la pantalla completa
    public void SetFullscreen(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    // Cambia el volumen de la música.
    public void SetMusicVolume(float volumen)
    {
        audioMixer.SetFloat("MusicVolume",Mathf.Log10(volumen) * 20
        );

        PlayerPrefs.SetFloat("MusicVolume",volumen
        );
    }

    // Cambia el volumen de los SFX
    public void SetSFXVolume(float volumen)
    {
        SFXMixer.SetFloat( "SFXVolume", Mathf.Log10(volumen) * 20);
        PlayerPrefs.SetFloat("SFXVolume",volumen);
    }

    // Cambia la calidad gráfica
    public void SetQualityLevel(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    // Ajusta el brillo de la pantalla
    public void SetBrightness(float value)
    {
       slider.value = value;
       PlayerPrefs.SetFloat("Brillo", slider.value);
       brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, slider.value);
    }

    // Carga el brillo guardado y actualiza la apariencia visual del panel
    void InitializeBrightness()
    {
        slider.value = PlayerPrefs.GetFloat("Brillo", 0.45f);
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, sliderValue);
    }

    // Obtiene todas las resoluciones disponibles
    void InitializeResolutions()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();
        resolutionDropdown.ClearOptions();
        currentRefreshRate = (float)Screen.currentResolution.refreshRateRatio.value;

        // Filtrar resoluciones con la misma tasa de refresco.
        for (int i = 0; i < resolutions.Length; i++)
        {
            float refreshRate = (float)resolutions[i].refreshRateRatio.value;
            if (Mathf.Abs(refreshRate - currentRefreshRate) < 0.1f)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        // Preparar la lista de opciones
        List<string> options = new List<string>();

        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height;
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

    // Carga los volúmenes guardados los aplica a los mixers
    void InitializeAudio()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.3f);

        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);

        SFXMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);

        musicSlider.value = musicVolume;

        sfxSlider.value = sfxVolume;
    }
}