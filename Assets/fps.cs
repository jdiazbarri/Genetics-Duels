using UnityEngine;

// Clase auxiliar para test de rendimiento
public class FPS : MonoBehaviour
{
    float deltaTime = 0.0f;

    // FPS mínimo mostrado
    float minFps = Mathf.Infinity;

    // FPS mínimo temporal del segundo actual
    float secondMinFps = Mathf.Infinity;

    // Temporizador
    float timer = 0f;

    void Update()
    {
        deltaTime += ( Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float currentFps = 1.0f / deltaTime;

        // Guardar el más bajo DURANTE este segundo
        if (currentFps < secondMinFps)
        {
            secondMinFps = currentFps;
        }

        timer += Time.unscaledDeltaTime;

        if (timer >= 1f)
        {
            minFps = secondMinFps;
            secondMinFps = Mathf.Infinity;
            timer = 0f;
        }
    }

    // Visualización
    void OnGUI()
    {
        int w = Screen.width;
        int h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 6 / 100);

        style.alignment = TextAnchor.UpperLeft;

        style.fontSize = h * 2 / 100;

        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);

        float msec = deltaTime * 1000.0f;

        float fps = 1.0f / deltaTime;

        // Aliados
        int playerCount = GameObject.FindGameObjectsWithTag("Player").Length; ;

        // Enemigos
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemigo").Length; ; 

        string text = string.Format( "{0:0.0} ms ({1:0.} fps)\n" + "MIN FPS (1s): {2:0.}\n" + "Players: {3}\n" + "Enemies: {4}", msec, fps, minFps, playerCount,enemyCount);
        GUI.Label(rect, text, style);
    }
}
