using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase encargada de gestionar la generación de la cuadrícula de combate y enemigos.
public class GridManager : MonoBehaviour
{
    // =========================
    // Configuración del tablero
    // =========================

    [SerializeField]
    private int rows = 7;

    [SerializeField]
    private int cols = 8;

    [SerializeField]
    private float tileSize = 100f;

    [SerializeField]
    private float wallScale = 0.65f;

    // =========================
    // Datos internos
    // =========================

    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private CharacterScaler characterScaler;

    [SerializeField]
    private BattleManager battleManager;

    private GameObject[,] gridTiles;

    void Start()
    {
        GenerateGrid();
        SpawnEnemies();
    }

    // Construir la cuadrícula
    private void GenerateGrid()
    {
        // Objetos visuales para la cuadrícula
        gridTiles = new GameObject[rows, cols];

        GameObject insideTile = Resources.Load<GameObject>("MargenMapa");

        GameObject wallTile = Resources.Load<GameObject>("Muro");

        // Creación automÁtica de la cuadrícula
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0;col < cols; col++)
            {
                GameObject prefabToSpawn;

                // Limites de la cuadrícula
                if ( row == 0 || row == rows - 1 || col == 0 || col == cols - 1)
                {
                    prefabToSpawn = wallTile;
                }
                else
                {
                    prefabToSpawn = insideTile;
                }

                // Crear tile
                GameObject tile =Instantiate(prefabToSpawn, transform);

                gridTiles[row, col] = tile;

                // Crear base
                if (prefabToSpawn == insideTile)
                {
                    GridTile gridTile = tile.AddComponent<GridTile>();

                    gridTile.row = row;
                    gridTile.col = col;

                    if (col < 4)
                    {
                        gridTile.playerZone = true;
                    }
                }

                // Posición de la cuadricula
                float posX = col * tileSize;
                float posY = row * tileSize;

                tile.transform.position = new Vector2( posX, posY);

                // Escala de tamaño
                tile.transform.localScale = Vector3.one* tileSize;

                // Escala de tamaño muralla
                if (prefabToSpawn == wallTile)
                {
                    tile.transform.localScale = Vector3.one * tileSize * wallScale;
                }
            }
        }

        // Centrar cuadrícula
        float gridWidth = cols * tileSize;

        float gridHeight = rows * tileSize;

        transform.position = new Vector2( -gridWidth / 2 + tileSize / 2, - gridHeight / 2 + tileSize / 2);
    }

    // Generar enemigos
    void SpawnEnemies()
    {
        int startCol = cols - 2;
        int startRow = rows - 2;

        int currentCol = startCol;
        int currentRow = startRow;

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            if (currentRow <= 0)
            {
                currentCol--;

                currentRow = startRow;
            }

            if (currentCol <= 0)
            {
                break;
            }

            GameObject targetTile =
                gridTiles[currentRow, currentCol];

            GameObject enemy =
                Instantiate(
                    enemyPrefabs[i],
                    targetTile.transform.position,
                    Quaternion.identity
                );

            characterScaler.ScaleCharacter(
                enemy,
                battleManager.GetCurrentLevel(),
                "Enemy"
            );

            currentRow--;
        }
    }
}