using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gestiona el tablero de cartas disponible para el jugador
public class CharacterBoard : MonoBehaviour
{
    // Prefab base de la carta
    [SerializeField]
    private GameObject cardPrefab;

    // Lista de personajes disponibles
    [SerializeField]
    private GameObject[] characterPrefabs;

    // Número de cartas visibles
    [SerializeField]
    private int cols = 6;

    // Separación horizontal entre cartas
    [SerializeField]
    private float spacing = 142f;

    // Cartas activas actualmente en pantalla
    private List<CharacterCard> activeCards = new List<CharacterCard>();

    // Personajes generados desde las cartas
    private List<GameObject> spawnedCharacters = new List<GameObject>();

    // Límite de cartas que el jugador puede activar 
    private int maxActiveCards = 1;

    // Cuántas cartas se han activado en la ronda actual
    private int activatedCardCount = 0;

    void Start()
    {
        SpawnCards();
    }

    // Llamado por BattleManager para sincronizar el límite con maxSlots
    public void SetMaxActiveCards(int max)
    {
        maxActiveCards = max;
        activatedCardCount = 0;
    }

    // Genera una tanda de cartas iniciales
    void SpawnCards()
    {
        float startX = -(cols - 1) * spacing / (-1.55f);

        for (int col = 0; col < cols; col++)
        {
            float posX = startX + col * spacing;
            float posY = 100f;
            Vector3 spawnPos = new Vector3(posX, posY, 0);
            GameObject cardObj = Instantiate(cardPrefab, spawnPos, Quaternion.identity);
            CharacterCard card = cardObj.GetComponent<CharacterCard>();

            card.characterPrefabs = characterPrefabs;
            card.Init(this);
            activeCards.Add(card);
        }
    }

    // Convierte cartas en personajes
    public void OnCardClicked(CharacterCard card)
    {
        if (card == null)
            return;

        // Bloquear si ya se alcanzó el límite de cartas activadas
        if (activatedCardCount >= maxActiveCards)
            return;

        activatedCardCount++;

        GameObject prefab = card.characterPrefabs[
            Random.Range(0, card.characterPrefabs.Length)
        ];

        GameObject character = Instantiate(
            prefab, card.transform.position, Quaternion.identity
        );

        spawnedCharacters.Add(character);
        activeCards.Remove(card);
        Destroy(card.gameObject);
    }

    // Elimina todas las cartas activas
    public void ClearCards()
    {
        foreach (CharacterCard card in activeCards)
        {
            if (card != null)
                Destroy(card.gameObject);
        }
        activeCards.Clear();
    }

    // Elimina personajes que no están dentro de la zona de combate
    public void CleanOutsideBattleZone()
    {
        foreach (GameObject character in spawnedCharacters)
        {
            if (character == null)
                continue;

            PlayerTag tag = character.GetComponent<PlayerTag>();

            if (tag == null || !tag.isInsideBattleZone)
                Destroy(character);
        }
        spawnedCharacters.RemoveAll(c => c == null);
    }

    // Genera una nueva tanda de cartas y reinicia el contador
    public void GenerateNewCards()
    {
        ClearCards();

        // Reiniciar contador para la nueva ronda
        activatedCardCount = 0;

        SpawnCards();
    }
}
