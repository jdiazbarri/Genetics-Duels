using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private int vidas = 3;

    [SerializeField]
    private TextMeshProUGUI vidasText;

    [SerializeField]
    private List<GameObject> levels;

    [SerializeField]
    private BattleZone battleZone;

    [SerializeField]
    private PlayerDetector playerDetector;

    [SerializeField]
    private Button battleButton;

    private int currentLevel = 0;

    private bool battleStarted = false;

    private bool battleEnded = false;

    void Start()
    {
        UpdateLivesUI();

        // DESACTIVAR TODOS
        for (int i = 0;
            i < levels.Count;
            i++)
        {
            levels[i].SetActive(false);
        }

        // ACTIVAR PRIMER NIVEL
        levels[currentLevel].SetActive(true);
    }

    void Update()
    {
        CheckBattleEnd();

        bool hasPlayers =
            battleZone.HasPlayers();

        // ACTIVAR/DESACTIVAR BOTÓN
        battleButton.interactable =
            hasPlayers;

        // COLORES BOTÓN
        ColorBlock colors =
            battleButton.colors;

        // COLOR NORMAL
        colors.normalColor =
            Color.white;

        // COLOR BLOQUEADO
        colors.disabledColor =
            Color.red;

        battleButton.colors =
            colors;
    }

    // INICIAR PELEA
    public void StartBattle()
    {
        if (battleStarted)
            return;

        battleStarted = true;

        battleEnded = false;

        // ALIADOS
        GameObject[] players =
            GameObject.FindGameObjectsWithTag(
                "Player"
            );

        foreach (GameObject player
            in players)
        {
            // MOVIMIENTO
            PlayerMovemt movement =
                player.GetComponent<PlayerMovemt>();

            if (movement != null)
            {
                // GUARDAR POSICIÓN GRID
                movement.SaveBattlePosition();

                // BLOQUEAR MOVIMIENTO
                movement.SetCanMove(false);
            }

            // IA MELEE
            GenericAI ai =
                player.GetComponent<GenericAI>();

            if (ai != null)
            {
                ai.ActivateAI();
            }

            // IA DISTANCIA
            AliadoDistanciaIA archer =
                player.GetComponent<AliadoDistanciaIA>();

            if (archer != null)
            {
                archer.ActivateAI();
            }
        }

        // ENEMIGOS
        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag(
                "Enemigo"
            );

        foreach (GameObject enemy
            in enemies)
        {
            // IA MELEE
            GenericAI ai =
                enemy.GetComponent<GenericAI>();

            if (ai != null)
            {
                ai.ActivateAI();
            }

            // IA DISTANCIA
            AliadoDistanciaIA archer =
                enemy.GetComponent<AliadoDistanciaIA>();

            if (archer != null)
            {
                archer.ActivateAI();
            }
        }
    }

    // FIN PELEA
    void CheckBattleEnd()
    {
        if (!battleStarted
            || battleEnded)
            return;

        // DERROTA
        if (!playerDetector.HasPlayers())
        {
            battleEnded = true;

            battleStarted = false;

            Debug.Log("DERROTA");

            LoseLife(1);

            // RESETEAR ALIADOS
            GameObject[] players =
                GameObject.FindGameObjectsWithTag(
                    "Player"
                );

            foreach (GameObject player
                in players)
            {
                // IA MELEE
                GenericAI ai =
                    player.GetComponent<GenericAI>();

                if (ai != null)
                {
                    ai.StopAI();
                }

                // IA DISTANCIA
                AliadoDistanciaIA ranged =
                    player.GetComponent<AliadoDistanciaIA>();

                if (ranged != null)
                {
                    ranged.StopAI();
                }

                // MOVIMIENTO
                PlayerMovemt movement =
                    player.GetComponent<PlayerMovemt>();

                if (movement != null)
                {
                    movement.ReturnToStartPosition();

                    movement.SetCanMove(true);
                }
            }

            return;
        }

        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag(
                "Enemigo"
            );

        // VICTORIA
        if (enemies.Length == 0)
        {
            battleEnded = true;

            battleStarted = false;

            Debug.Log("VICTORIA");

            // RESETEAR ALIADOS
            GameObject[] players =
                GameObject.FindGameObjectsWithTag(
                    "Player"
                );

            foreach (GameObject player
                in players)
            {
                // IA MELEE
                GenericAI ai =
                    player.GetComponent<GenericAI>();

                if (ai != null)
                {
                    ai.StopAI();
                }

                // IA DISTANCIA
                AliadoDistanciaIA ranged =
                    player.GetComponent<AliadoDistanciaIA>();

                if (ranged != null)
                {
                    ranged.StopAI();
                }

                // MOVIMIENTO
                PlayerMovemt movement =
                    player.GetComponent<PlayerMovemt>();

                if (movement != null)
                {
                    movement.ReturnToStartPosition();

                    movement.SetCanMove(true);
                }
            }

            NextLevel();
        }
    }

    // SIGUIENTE NIVEL
    void NextLevel()
    {
        // DESACTIVAR NIVEL
        levels[currentLevel]
            .SetActive(false);

        currentLevel++;

        // FIN JUEGO
        if (currentLevel
            >= levels.Count)
        {
            Debug.Log("GANASTE");

            return;
        }

        // ACTIVAR NUEVO NIVEL
        levels[currentLevel]
            .SetActive(true);
    }

    // PERDER VIDA
    public void LoseLife(int amount)
    {
        vidas -= amount;

        if (vidas < 0)
        {
            vidas = 0;
        }

        UpdateLivesUI();

        if (vidas <= 0)
        {
            GameOver();
        }
    }

    void UpdateLivesUI()
    {
        vidasText.text =
            vidas.ToString();
    }

    void GameOver()
    {
        Debug.Log("GAME OVER");
    }

    public int GetLives()
    {
        return vidas;
    }
}
