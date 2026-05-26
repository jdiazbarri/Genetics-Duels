using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBoard : MonoBehaviour
{
    [SerializeField]
    private GameObject[] characterPrefabs;

    [SerializeField]
    private int cols = 6;

    [SerializeField]
    private float spacing = 150f;

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

        for (int col = 0;
            col < cols;
            col++)
        {
            if (col >= characterPrefabs.Length)
                return;

            float posX =
                startX + col * spacing;

            float posY = 100f;

            Vector3 spawnPos =
                new Vector3(
                    posX,
                    posY,
                    0
                );

            GameObject obj =
    Instantiate(
        characterPrefabs[col],
        spawnPos,
        Quaternion.identity
    );

            
        }
    }
}
