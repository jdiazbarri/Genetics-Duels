using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private GameObject pauseButton;

    [SerializeField]
    private GameObject resumeButton;

    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI instructionsText;

    void Start()
    {
        panel.SetActive(false);

        pauseButton.SetActive(true);

        resumeButton.SetActive(false);

        titleText.gameObject
            .SetActive(false);

        instructionsText.gameObject
            .SetActive(false);

        // TITLE
        titleText.text =
            "INSTRUCCIONES";

        // INSTRUCTIONS
        instructionsText.text =

            "- Arrastra personajes al tablero\n\n" +

            "- Fusiona aliados usando monedas\n\n" +

            "- Gana batallas para obtener fichas\n\n" +

            "- Los hijos heredan estadísticas y habilidades\n\n" +

            "- La misma sangre activa Endogamia\n\n" +

            "- Usa el botón pausa para detener la partida";
    }

    public void Pause()
    {
        panel.SetActive(true);

        pauseButton.SetActive(false);

        resumeButton.SetActive(true);

        titleText.gameObject
            .SetActive(true);

        instructionsText.gameObject
            .SetActive(true);

        Time.timeScale = 0f;
    }

    public void Resume()
    {

        panel.SetActive(false);

        pauseButton.SetActive(true);

        resumeButton.SetActive(false);

        titleText.gameObject
            .SetActive(false);

        instructionsText.gameObject
            .SetActive(false);

        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;

        Application.Quit();
    }
}
