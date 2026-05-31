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

            "- Arrastra personajes dentro de las murallas para activar el botón de batalla\n\n" +

            "- Para fusionar aliados, colado un personaje dentro del primer recuadro\n\n" +

            "  y otro en el segundo y se genera un nuevo personaje\n\n" +

            "- Las fusiones consumen monedas de fusión que se obtienen al subir de ronda\n\n" +

            "- Los hijos heredan estadísticas y habilidades de los padres\n\n" +

            "- Dos personajes con el mismo tipo de sangre activan la habildiad especial endogamia\n\n" +

            "- El botón de volver, permite volver al menú principal sin reiniciar la partida\n\n" +
            
            "- El sistema decide que personajes usar si se seleccionan más del límite";
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
