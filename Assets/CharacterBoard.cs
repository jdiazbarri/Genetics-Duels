using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBoard : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;

    [SerializeField]
    private GameObject[] characterPrefabs;

    [SerializeField]
    private int cols = 6;

    [SerializeField]
    private float spacing = 142f;

    private List<CharacterCard> activeCards = new List<CharacterCard>();

    private List<GameObject> spawnedCharacters = new List<GameObject>();

    void Start()
    {
        GenerateBoard();
    }

    void GenerateBoard()
    {
        float startX =
            -(cols - 1)
            * spacing
            / (-1.55f);

        for (int col = 0; col < cols; col++)
        {
            float posX = startX + col * spacing;
            float posY = 100f;

            Vector3 spawnPos =
                new Vector3(posX, posY, 0);

            GameObject cardObj =
                Instantiate(cardPrefab, spawnPos, Quaternion.identity);

            CharacterCard card =
                cardObj.GetComponent<CharacterCard>();

            if (card == null)
            {
                Debug.LogError("Card prefab no tiene CharacterCard");
                continue;
            }

            card.characterPrefabs = characterPrefabs;
            card.Init(this);

            activeCards.Add(card);
        }
    }

    public void OnCardClicked(CharacterCard card)
    {
        if (card == null) return;

        GameObject prefab =
            card.characterPrefabs[
                Random.Range(0, card.characterPrefabs.Length)
            ];

        GameObject character =
            Instantiate(prefab, card.transform.position, Quaternion.identity);

        // ?? GUARDAR PERSONAJE GENERADO
        spawnedCharacters.Add(character);

        activeCards.Remove(card);

        Destroy(card.gameObject);
    }

    // ?? LIMPIEZA DESDE BATTLEMANAGER
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

    public void CleanOutsideBattleZone()
    {
        foreach (GameObject character in spawnedCharacters)
        {
            if (character == null) continue;

            PlayerTag tag =
                character.GetComponent<PlayerTag>();

            if (tag == null || !tag.isInsideBattleZone)
            {
                Destroy(character);
            }
        }

        spawnedCharacters.RemoveAll(c => c == null);
    }

    public void GenerateNewCards()
    {
        ClearCards();

        float startX =
            -(cols - 1)
            * spacing
            / (-1.55f);

        for (int col = 0; col < cols; col++)
        {
            float posX =
                startX + col * spacing;

            float posY = 100f;

            Vector3 spawnPos =
                new Vector3(posX, posY, 0);

            GameObject cardObj =
                Instantiate(
                    cardPrefab,
                    spawnPos,
                    Quaternion.identity
                );

            CharacterCard card =
                cardObj.GetComponent<CharacterCard>();

            card.characterPrefabs =
                characterPrefabs;

            card.Init(this);

            // IMPORTANTE
            activeCards.Add(card);
        }
    }
}
