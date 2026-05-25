using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private int vidas = 3;

    [SerializeField]
    private TextMeshProUGUI vidasText;

    void Start()
    {
        UpdateLivesUI();
    }

    // INICIAR PELEA
    public void StartBattle()
    {
        // ACTIVAR ALIADOS
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
                ai.ActivateAI();
            }

            // IA DISTANCIA
            AliadoDistanciaIA archer =
                player.GetComponent<AliadoDistanciaIA>();

            if (archer != null)
            {
                archer.ActivateAI();
            }

            // BLOQUEAR MOVIMIENTO
            PlayerMovemt movement =
                player.GetComponent<PlayerMovemt>();

            if (movement != null)
            {
                movement.SetCanMove(false);
            }
        }

        // ACTIVAR ENEMIGOS
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
            "" + vidas;
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
