using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Sistema encargado de gestionar la pausa de la partida y mostrar las instrucciones básicas del juego al usuario.
public class MenuPausa : MonoBehaviour
{
    // Panel principal de pausa
    [SerializeField]
    private GameObject panel;

    // Botón para abrir el menú pausa
    [SerializeField]
    private GameObject pauseButton;

    // Botón para cerrar el menú pausa
    [SerializeField]
    private GameObject resumeButton;

    // Título del panel
    [SerializeField]
    private TextMeshProUGUI titleText;

    // Texto con instrucciones
    [SerializeField]
    private TextMeshProUGUI instructionsText;

    void Start()
    {
        panel.SetActive(false);

        pauseButton.SetActive(true);

        resumeButton.SetActive(false);

        titleText.gameObject.SetActive(false);

        instructionsText.gameObject.SetActive(false);

        // Título y instrucciones
        titleText.text = "INSTRUCCIONES";

        instructionsText.text =

            "- Arrastra personajes al tablero\n\n" +

            "- Fusiona aliados usando monedas\n\n" +

            "- Gana batallas para obtener fichas\n\n" +

            "- Los hijos heredan estadísticas y habilidades\n\n" +

            "- La misma sangre activa Endogamia\n\n" +

            "- Usa el botón pausa para detener la partida";
    }

    // Pausar partida y mostrar menú
    public void Pause()
    {
        panel.SetActive(true);

        pauseButton.SetActive(false);

        resumeButton.SetActive(true);

        titleText.gameObject.SetActive(true);

        instructionsText.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }

    // Ocultar menú
    public void Resume()
    {
        panel.SetActive(false);

        pauseButton.SetActive(true);

        resumeButton.SetActive(false);

        titleText.gameObject.SetActive(false);

        instructionsText.gameObject.SetActive(false);

        Time.timeScale = 1f;
    }
}
