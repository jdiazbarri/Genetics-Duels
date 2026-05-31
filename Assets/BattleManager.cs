using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Gestor principal del combate.
//
// Se encarga de controlar:
// - Vidas del jugador.
// - Monedas disponibles.
// - Límite de unidades.
// - Progresión entre niveles.
// - Inicio y fin de batalla.
// - Interfaces de victoria y derrota.
public class BattleManager : MonoBehaviour
{
    // =========================
    // Vidas del jugador
    // =========================
    [SerializeField]
    private int vidas = 1;

    [SerializeField]
    private TextMeshProUGUI vidasText;

    // =========================
    // Economía
    // =========================

    [SerializeField]
    private int monedas = 0;

    [SerializeField]
    private int maxMonedas = 4;

    [SerializeField]
    private TextMeshProUGUI monedasText;

    // =========================
    // Límite de unidades
    // =========================

    [SerializeField]
    private int maxSlots = 1;

    [SerializeField]
    private TextMeshProUGUI slotsText;

    // =========================
    // Niveles
    // =========================

    [SerializeField]
    private List<GameObject> levels;

    // =========================
    // Referencias del sistema
    // =========================

    [SerializeField]
    private BattleZone battleZone;

    [SerializeField]
    private PlayerDetector playerDetector;

    [SerializeField]
    private Button battleButton;

    [SerializeField]
    private CharacterBoard characterBoard;

    // =========================
    // Pantallas finales
    // =========================

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private GameObject victoryPanel;

    // =========================
    // Estado actual
    // =========================

    private int currentLevel = 0;

    private bool battleStarted = false;

    private bool battleEnded = false;

    private bool battleCheckReady = false;

    public static BattleManager instance;

    private int maxCardsPerRound = 1;
    // =========================
    // Límite de cartas
    // =========================

    [SerializeField]
    private TextMeshProUGUI cardsText;

    void Start()
    {
        // Inicializar interfaz
        UpdateLivesUI();
        UpdateCoinsUI();
        UpdateSlotsUI();

        // Aplicar límite de cartas del nivel inicial
        maxCardsPerRound = currentLevel == 0 ? 1 : 2;

        if (characterBoard != null)
        {
            characterBoard.SetMaxActiveCards(maxCardsPerRound);
        }

        UpdateCardsUI();

        // =========================
        // Activar nivel inicial
        // =========================

        // Desactivar todos los niveles
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].SetActive(false);
        }

        levels[currentLevel].SetActive(true);
    }

    void Update()
    {
        // Comprobar condiciones de victoria o derrota
        CheckBattleEnd();
        // Detectar si hay unidades preparadas
        bool hasPlayers = battleZone.HasPlayers();
        // Activar o desactivar botón de batalla
        battleButton.interactable = hasPlayers;
        ColorBlock colors = battleButton.colors;
        colors.normalColor = Color.white;
        colors.disabledColor = Color.red;
        battleButton.colors = colors;
    }

    void Awake()
    {
        instance = this;
    }

    // Activa la pelea
    public void StartBattle()
    {
        if (battleStarted)
        {
            return;
        }
        StartCoroutine(StartBattleRoutine());
    }

    // Rutina de pelea
    private IEnumerator StartBattleRoutine()
    {
        battleStarted = true;
        battleEnded = false;
        battleCheckReady = false;

        SoundManager.instance.PlayBattleStartSound();

        // =========================
        // FASE 1: limpiar escena
        // =========================

        // Eliminar cartas sobrantes
        if (characterBoard != null)
        {
            characterBoard.ClearCards();
            characterBoard.CleanOutsideBattleZone();
        }

        // Eliminar personajes fuera del grid usando isInsideBattleZone
        // y ValidatePlayerSlots para respetar maxSlot
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in allPlayers)
        {
            PlayerTag tag = player.GetComponent<PlayerTag>();

            if (tag != null && !tag.isInsideBattleZone)
            {
                Destroy(player);
            }
        }

        ValidatePlayerSlots();

        // Delay para evitar errores con la física del motor
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        // =========================
        // FASE 2: activar IA
        // =========================

        // Ahora FindGameObjectsWithTag solo devuelve los supervivientes reales
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            PlayerMovemt movement = player.GetComponent<PlayerMovemt>();

            if (movement != null)
            {
                movement.SaveBattlePosition();
                movement.SetCanMove(false);
            }

            MeleeAI ai = player.GetComponent<MeleeAI>();

            if (ai != null)
            {
                ai.ActivateAI();
            }

            RangedAI ranged = player.GetComponent<RangedAI>();

            if (ranged != null)
            {
                ranged.ActivateAI();
            }
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemigo");

        foreach (GameObject enemy in enemies)
        {
            MeleeAI ai = enemy.GetComponent<MeleeAI>();

            if (ai != null)
            {
                ai.ActivateAI();
            }

            RangedAI ranged = enemy.GetComponent<RangedAI>();

            if (ranged != null)
            {
                ranged.ActivateAI();
            }
        }

        // ==========================================
        // FASE 3: habilitar vigilancia de derrota
        // ============================================

        // Esperar a que el PlayerDetector confirme al menos 1 jugador vivo
        // Timeout de seguridad de 2s para evitar bucle infinito
        float timeout = 2f;
        float elapsed = 0f;

        while (!playerDetector.HasPlayers() && elapsed < timeout)
        {
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        battleCheckReady = true;
    }

    // Comprueba continuamente si la batalla ha terminado
    // Existen dos posibles resultados:
    // - Derrota: no quedan aliados vivos
    // - Victoria: no quedan enemigos vivos
    void CheckBattleEnd()
    {
        if (!battleStarted || battleEnded || !battleCheckReady)
        {
            return;
        }

        // =========================
        // DERROTA
        // =========================

        if (!playerDetector.HasPlayers())
        {
            battleEnded = true;
            battleStarted = false;

            // Quitar vida
            LoseLife(1);

            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject player in players)
            {
                MeleeAI ai = player.GetComponent<MeleeAI>();

                if (ai != null)
                {
                    ai.StopAI();
                }

                RangedAI ranged = player.GetComponent<RangedAI>();

                if (ranged != null)
                {
                    ranged.StopAI();
                }

                PlayerMovemt movement = player.GetComponent<PlayerMovemt>();

                if (movement != null)
                {
                    movement.ReturnToStartPosition();
                    movement.SetCanMove(true);
                }
            }
            return;
        }

        // =========================
        // VICTORIA
        // =========================

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemigo");

        if (enemies.Length == 0)
        {
            battleEnded = true;
            battleStarted = false;

            SoundManager.instance.PlayLevelCompleteSound();

            // Recompensa de monedas
            if (monedas < maxMonedas)
            {
                monedas++;
                UpdateCoinsUI();
            }

            // Generar nuevas cartas para la siguiente ronda.
            if (characterBoard != null)
            {
                characterBoard.GenerateNewCards();
            }

            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject player in players)
            {
                MeleeAI ai =
                    player.GetComponent<MeleeAI>();

                if (ai != null)
                {
                    ai.StopAI();
                }

                RangedAI ranged = player.GetComponent<RangedAI>();

                if (ranged != null)
                {
                    ranged.StopAI();
                }

                PlayerMovemt movement = player.GetComponent<PlayerMovemt>();

                if (movement != null)
                {
                    movement.ReturnToStartPosition();
                    movement.SetCanMove(true);
                }

                // Restaurar vida completa
                CharacterStats stats = player.GetComponent<CharacterStats>();

                if (stats != null)
                {
                    stats.health = stats.maxHealth;
                }
            }
            NextLevel();
        }
    }

    // Permite avanzar de nivel, desactivando el anterior
    void NextLevel()
    {
        levels[currentLevel].SetActive(false);
        currentLevel++;

        if (currentLevel >= levels.Count)
        {
            Victory();
            return;
        }

        levels[currentLevel].SetActive(true);
        UpdateSlots();
    }

    // Quitar vida al jugador
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

    // =========================
    // Actualizar UI
    // =========================

    void UpdateLivesUI()
    {
        vidasText.text = vidas.ToString();
    }

    void UpdateCoinsUI()
    {
        monedasText.text = monedas.ToString();
    }

    void UpdateSlotsUI()
    {
        slotsText.text = maxSlots.ToString();
    }

    void UpdateCardsUI()
    {
        if (cardsText != null)
            cardsText.text = maxCardsPerRound.ToString();
    }

    // =============================
    // Eventos de derrota y victoria
    // =============================

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void Victory()
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // Obteber número de vidas
    public int GetLives()
    {
        return vidas;
    }

    // Gasta la monedas de fusión
    public bool UseCoin(int amount)
    {
        if (monedas < amount)
        {
            return false;
        }

        monedas -= amount;
        UpdateCoinsUI();
        return true;
    }

    // Obtener el nivel actual
    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    // Actuliza limites
    private void UpdateSlots()
    {
        // Slots de personajes
        maxSlots = Mathf.Clamp(currentLevel + 1, 1, 5);
        UpdateSlotsUI();

        // Cartas activables
        maxCardsPerRound = currentLevel == 0 ? 1 : 2;
        if (characterBoard != null)
        {
            characterBoard.SetMaxActiveCards(maxCardsPerRound);
        }

        UpdateCardsUI();
    }

    // Comprueba que el número de personajes colocados en el tablero no supere el límite permitido
    private void ValidatePlayerSlots()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        List<GameObject> playersOnGrid = new List<GameObject>();

        foreach (GameObject player in players)
        {
            Transform parent = player.transform.parent;
            bool isInFusionSlot = parent != null && parent.CompareTag("FusionSlot");

            if (!isInFusionSlot)
            {
                playersOnGrid.Add(player);
            }
        }

        while (playersOnGrid.Count > maxSlots)
        {
            int randomIndex = Random.Range(0, playersOnGrid.Count);
            GameObject toRemove = playersOnGrid[randomIndex];
            playersOnGrid.RemoveAt(randomIndex);
            Destroy(toRemove);
        }
    }
}
