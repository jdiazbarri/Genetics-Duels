using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private int vidas = 1;

    [SerializeField]
    private TextMeshProUGUI vidasText;

    [SerializeField]
    private int monedas = 0;

    [SerializeField]
    private int maxMonedas = 4;

    [SerializeField]
    private TextMeshProUGUI monedasText;

    [SerializeField]
    private int maxSlots = 1;

    [SerializeField]
    private TextMeshProUGUI slotsText;

    [SerializeField]
    private List<GameObject> levels;

    [SerializeField]
    private BattleZone battleZone;

    [SerializeField]
    private PlayerDetector playerDetector;

    [SerializeField]
    private Button battleButton;

    [SerializeField]
    private CharacterBoard characterBoard;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private GameObject victoryPanel;

    private int currentLevel = 0;

    private bool battleStarted = false;

    private bool battleEnded = false;

    public static BattleManager instance;

    void Start()
    {
        UpdateLivesUI();

        UpdateCoinsUI();

        UpdateSlotsUI();

        // DESACTIVAR TODOS
        for (int i = 0;
            i < levels.Count;
            i++)
        {
            levels[i].SetActive(false);
        }

        // ACTIVAR PRIMER NIVEL
        levels[currentLevel]
            .SetActive(true);
    }

    void Update()
    {
        CheckBattleEnd();

        bool hasPlayers =
            battleZone.HasPlayers();

        // ACTIVAR BOTÓN
        battleButton.interactable =
            hasPlayers;

        // COLORES BOTÓN
        ColorBlock colors =
            battleButton.colors;

        colors.normalColor =
            Color.white;

        colors.disabledColor =
            Color.red;

        battleButton.colors =
            colors;

    }

    void Awake()
    {
        instance = this;
    }

    public void StartBattle()
    {
        if (battleStarted)
            return;

        battleStarted = true;
        battleEnded = false;

        SoundManager.instance.PlayBattleStartSound();

        // =========================
        // ?? LIMPIAR CARTAS
        // =========================
        if (characterBoard != null)
        {
            characterBoard.ClearCards();
            characterBoard.CleanOutsideBattleZone(); // ?? NUEVO
        }

        // ?? AJUSTAR SLOTS SEGÚN NIVEL
        

        // ?? VALIDAR EQUIPO FINAL
        ValidatePlayerSlots();

        // =========================
        // ?? SOLO PLAYER EN GRID
        // =========================
        GameObject[] players =
            GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            PlayerTag tag =
                player.GetComponent<PlayerTag>();

            // ? IGNORAR FUERA DEL GRID
            if (tag != null && !tag.isInsideBattleZone)
            {
                Destroy(player);
                continue;
            }

            // =========================
            // MOVIMIENTO
            // =========================
            PlayerMovemt movement =
                player.GetComponent<PlayerMovemt>();

            if (movement != null)
            {
                movement.SaveBattlePosition();
                movement.SetCanMove(false);
            }

            // =========================
            // IA
            // =========================
            MeleeAI ai =
                player.GetComponent<MeleeAI>();

            if (ai != null)
            {
                ai.ActivateAI();
            }

            RangedAI ranged =
                player.GetComponent<RangedAI>();

            if (ranged != null)
            {
                ranged.ActivateAI();
            }
        }

        // =========================
        // ENEMIGOS (SIN CAMBIOS)
        // =========================
        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag("Enemigo");

        foreach (GameObject enemy in enemies)
        {
            MeleeAI ai =
                enemy.GetComponent<MeleeAI>();

            if (ai != null)
            {
                ai.ActivateAI();
            }

            RangedAI ranged =
                enemy.GetComponent<RangedAI>();

            if (ranged != null)
            {
                ranged.ActivateAI();
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
                MeleeAI ai =
                    player.GetComponent<MeleeAI>();

                if (ai != null)
                {
                    ai.StopAI();
                }

                // IA DISTANCIA
                RangedAI ranged =
                    player.GetComponent<RangedAI>();

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

            SoundManager.instance.PlayLevelCompleteSound();

            if (monedas < maxMonedas)
            {
                monedas++;

                UpdateCoinsUI();
            }

            

            // =========================
            //  NUEVAS CARTAS PARA SIGUIENTE RONDA
            // =========================
            if (characterBoard != null)
            {
                characterBoard.GenerateNewCards();
            }

            // RESETEAR ALIADOS
            GameObject[] players =
                GameObject.FindGameObjectsWithTag(
                    "Player"
                );

            foreach (GameObject player
                in players)
            {
                // IA MELEE
                MeleeAI ai =
                    player.GetComponent<MeleeAI>();

                if (ai != null)
                {
                    ai.StopAI();
                }

                // IA DISTANCIA
                RangedAI ranged =
                    player.GetComponent<RangedAI>();

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

                // CURAR VIDA
                CharacterStats stats =
                    player.GetComponent<CharacterStats>();

                if (stats != null)
                {
                    stats.health =
                        stats.maxHealth;
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
            Victory();

            return;
        }

        // ACTIVAR NUEVO NIVEL
        levels[currentLevel]
            .SetActive(true);

        UpdateSlots();
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

    void UpdateCoinsUI()
    {
        monedasText.text =
            monedas.ToString();
    }

    void GameOver()
    {
        Debug.Log("GAME OVER");

        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    void Victory()
    {
        Debug.Log("VICTORIA FINAL");

        victoryPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public int GetLives()
    {
        return vidas;
    }

    public bool UseCoin(int amount)
    {
        // NO HAY SUFICIENTES
        if (monedas < amount)
        {
            return false;
        }

        monedas -= amount;

        UpdateCoinsUI();

        return true;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    private void UpdateSlots()
    {
        // escala hasta 5 máximo
        maxSlots = Mathf.Clamp(currentLevel + 1, 1, 5);

        UpdateSlotsUI();
    }

    void UpdateSlotsUI()
    {
        slotsText.text =
            maxSlots.ToString();
    }

    private void ValidatePlayerSlots()
    {
        GameObject[] players =
            GameObject.FindGameObjectsWithTag("Player");

        List<GameObject> validPlayers = new List<GameObject>(players);

        // si hay más de los permitidos
        while (validPlayers.Count > maxSlots)
        {
            int randomIndex =
                Random.Range(0, validPlayers.Count);

            GameObject toRemove =
                validPlayers[randomIndex];

            validPlayers.RemoveAt(randomIndex);

            Destroy(toRemove);
        }
    }
}
