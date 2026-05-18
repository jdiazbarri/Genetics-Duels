using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int rows = 7;

    [SerializeField]
    private int cols = 8;

    [SerializeField]
    private float tileSize = 100f;

    [SerializeField]
    private float wallScale = 0.65f;

    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        GameObject iceTile = Resources.Load<GameObject>("ice");
        GameObject wallTile = Resources.Load<GameObject>("muralla");

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject prefabToSpawn;

                if (row == 0 || row == rows - 1 || col == 0 || col == cols - 1)
                {
                    prefabToSpawn = wallTile;
                }
                else
                {
                    prefabToSpawn = iceTile;
                }

                GameObject tile = Instantiate(prefabToSpawn, transform);

                float posX = col * tileSize;
                float posY = row * tileSize;

                tile.transform.position = new Vector2(posX, posY);

                tile.transform.localScale = Vector3.one * tileSize;

                if (prefabToSpawn == wallTile)
                {
                    tile.transform.localScale = new Vector3(wallScale, wallScale, 1f);
                }
            }
        }

        // CENTRAR GRID
        float gridWidth = cols * tileSize;
        float gridHeight = rows * tileSize;

        transform.position = new Vector2(
            -gridWidth / 2 + tileSize / 2,
            -gridHeight / 2 + tileSize / 2
        );
    }

    void Update()
    {

    }
}
