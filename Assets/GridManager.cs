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

    [SerializeField]
    private List<WaveData> levels;

    private int currentLevel = 0;

    // TODOS LOS TILES
    private GameObject[,] gridTiles;

    void Start()
    {
        GenerateGrid();

        SpawnLevelEnemies();
    }

    private void GenerateGrid()
    {
        // MATRIZ
        gridTiles =
            new GameObject[rows, cols];

        GameObject iceTile =
            Resources.Load<GameObject>(
                "ice"
            );

        GameObject wallTile =
            Resources.Load<GameObject>(
                "muralla"
            );

        for (
            int row = 0;
            row < rows;
            row++
        )
        {
            for (
                int col = 0;
                col < cols;
                col++
            )
            {
                GameObject prefabToSpawn;

                // BORDES
                if (
                    row == 0
                    || row == rows - 1
                    || col == 0
                    || col == cols - 1
                )
                {
                    prefabToSpawn =
                        wallTile;
                }
                else
                {
                    prefabToSpawn =
                        iceTile;
                }

                // CREAR TILE
                GameObject tile =
                    Instantiate(
                        prefabToSpawn,
                        transform
                    );

                gridTiles[row, col] =
                    tile;

                // GRID TILE
                if (
                    prefabToSpawn
                    == iceTile
                )
                {
                    GridTile gridTile =
                        tile.AddComponent<GridTile>();

                    gridTile.row = row;

                    gridTile.col = col;

                    // ZONA JUGADOR
                    if (col < 4)
                    {
                        gridTile.playerZone =
                            true;
                    }
                }

                // POSICIÓN
                float posX =
                    col * tileSize;

                float posY =
                    row * tileSize;

                tile.transform.position =
                    new Vector2(
                        posX,
                        posY
                    );

                // ESCALA NORMAL
                tile.transform.localScale =
                    Vector3.one
                    * tileSize;

                // ESCALA MURALLA
                if (
                    prefabToSpawn
                    == wallTile
                )
                {
                    tile.transform.localScale =
                        Vector3.one
                        * tileSize
                        * wallScale;
                }
            }
        }

        // CENTRAR GRID
        float gridWidth =
            cols * tileSize;

        float gridHeight =
            rows * tileSize;

        transform.position =
            new Vector2(
                -gridWidth / 2
                + tileSize / 2,

                -gridHeight / 2
                + tileSize / 2
            );
    }

    void SpawnLevelEnemies()
    {
        // FIN JUEGO
        if (
            currentLevel
            >= levels.Count
        )
        {
            Debug.Log(
                "NO MÁS NIVELES"
            );

            return;
        }

        List<GameObject> enemies =
            levels[currentLevel]
            .enemies;

        // COLUMNA DERECHA
        int spawnCol =
            cols - 2;

        // FILA SUPERIOR
        int startRow =
            rows - 2;

        for (
            int i = 0;
            i < enemies.Count;
            i++
        )
        {
            // ARRIBA ? ABAJO
            int row =
                startRow - i;

            // EVITAR MURO
            if (row <= 0)
                break;

            GameObject targetTile =
                gridTiles[row, spawnCol];

            Instantiate(
                enemies[i],
                targetTile.transform.position,
                Quaternion.identity
            );
        }

        Debug.Log(
            "Nivel actual: "
            + (currentLevel + 1)
        );
    }

    public void NextLevel()
    {
        currentLevel++;

        SpawnLevelEnemies();
    }
}