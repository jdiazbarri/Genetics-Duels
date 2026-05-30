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

    void Start()
    {
        GenerateBoard();
    }

    //Creación de las cartas
    void GenerateBoard()
    {
        // Punto de inicio
        float startX =  -(cols - 1)* spacing / (-1.55f);
        
        // Creación dinámica de las cartas
        for (int col = 0; col < cols; col++)
        {
            float posX = startX + col * spacing;
            float posY = 100f;
            Vector3 spawnPos = new Vector3(posX, posY, 0);
            GameObject cardObj = Instantiate(cardPrefab, spawnPos, Quaternion.identity);
            CharacterCard card = cardObj.GetComponent<CharacterCard>();

            // Asignar personajes posibles y vincular con tablero
            card.characterPrefabs = characterPrefabs;
            card.Init(this);
            activeCards.Add(card);
        }
    }

    // Convierte cartas en personajes
    public void OnCardClicked(CharacterCard card)
    {
        if (card == null)
        {
            return;
        }

        // Seleccionar personaje aleatorio.
        GameObject prefab = card.characterPrefabs[Random.Range(0, card.characterPrefabs.Length)];

        // Crear personaje.
        GameObject character = Instantiate(prefab, card.transform.position, Quaternion.identity);

        // Eliminar castas usadas
        spawnedCharacters.Add(character);
        activeCards.Remove(card);
        Destroy(card.gameObject);
    }

    // Elimina todas las cartas activas.
    public void ClearCards()
    {
        foreach (CharacterCard card in activeCards)
        {
            if (card != null)
            {
                Destroy(card.gameObject);
            }
        }
        activeCards.Clear();
    }

    // Elimina personajes que no se encuentran dentro de la zona de combate
    public void CleanOutsideBattleZone()
    {
        foreach (GameObject character in spawnedCharacters)
        {
            if (character == null)
            {
                continue;
            }

            PlayerTag tag = character.GetComponent<PlayerTag>();

            if (tag == null || !tag.isInsideBattleZone)
            {
                Destroy(character);
            }
        }
        spawnedCharacters.RemoveAll(c => c == null);
    }

    // Genera una nueva tanda de cartas
    public void GenerateNewCards()
    {
        ClearCards();

        float startX = -(cols - 1) * spacing / (-1.55f);

        // Creación dinámica de las cartas
        for (int col = 0; col < cols; col++)
        {
            float posX = startX + col * spacing;
            float posY = 100f;

            Vector3 spawnPos = new Vector3(posX, posY, 0);
            GameObject cardObj = Instantiate( cardPrefab, spawnPos, Quaternion.identity);
            CharacterCard card = cardObj.GetComponent<CharacterCard>();

            card.characterPrefabs = characterPrefabs;
            card.Init(this);
            activeCards.Add(card);
        }
    }
}
