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

    // Guardamos todos los tiles
    private GameObject[,] gridTiles;

    void Start()
    {
        GenerateGrid();

        SpawnLevelEnemies();
    }

    private void GenerateGrid()
    {
        // Inicializar matriz
        gridTiles = new GameObject[rows, cols];

        GameObject iceTile = Resources.Load<GameObject>("ice");
        GameObject wallTile = Resources.Load<GameObject>("muralla");

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject prefabToSpawn;

                // Bordes = muralla
                if (row == 0 || row == rows - 1 || col == 0 || col == cols - 1)
                {
                    prefabToSpawn = wallTile;
                }
                else
                {
                    prefabToSpawn = iceTile;
                }

                // Crear tile
                GameObject tile = Instantiate(prefabToSpawn, transform);

                // Guardar referencia
                gridTiles[row, col] = tile;

                // Configuración solo para tiles normales
                if (prefabToSpawn == iceTile)
                {
                    GridTile gridTile = tile.AddComponent<GridTile>();
                    gridTile.row = row;
                    gridTile.col = col;

                    // Zona jugador = primeras 4 columnas
                    if (col < 4)
                    {
                        gridTile.playerZone = true;
                    }
                }

                // Posición
                float posX = col * tileSize;
                float posY = row * tileSize;

                tile.transform.position = new Vector2(posX, posY);

                // Escala
                tile.transform.localScale = Vector3.one * tileSize;

                // Escala especial murallas
                if (prefabToSpawn == wallTile)
                {
                    tile.transform.localScale = Vector3.one * tileSize * wallScale;
                }
            }
        }

        // Centrar grid
        float gridWidth = cols * tileSize;
        float gridHeight = rows * tileSize;

        transform.position = new Vector2(
            -gridWidth / 2 + tileSize / 2,
            -gridHeight / 2 + tileSize / 2
        );
    }

    //void SpawnEnemy()
    //{
    //    // Casilla enemiga
    //    int enemyCol = cols - 2;
    //    int enemyRow = rows / 2;

    //    // Obtener tile real
    //    GameObject targetTile = gridTiles[enemyRow, enemyCol];

    //    // Crear enemigo centrado
    //    Instantiate(
    //        enemyPrefab,
    //        targetTile.transform.position,
    //        Quaternion.identity
    //    );
    //}

    void Update()
    {

    }

    void SpawnLevelEnemies()
    {
        if (currentLevel >= levels.Count)
        {
            Debug.Log("NO MÁS NIVELES");

            return;
        }

        List<GameObject> enemies =
            levels[currentLevel].enemies;

        int spawnCol =
            cols - 2;

        int startRow = 1;

        for (int i = 0;
            i < enemies.Count;
            i++)
        {
            int row =
                startRow + i;

            if (row >= rows - 1)
                break;

            GameObject targetTile =
                gridTiles[row, spawnCol];

            Instantiate(
                enemies[i],
                targetTile.transform.position,
                Quaternion.identity
            );
        }
    }

    public void NextLevel()
    {
        currentLevel++;

        SpawnLevelEnemies();
    }
}