using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gestiona el movimiento manual de los personajes antes del combate
public class PlayerMovemt : MonoBehaviour
{
    // =========================
    // Referencia visual
    // =========================

    [SerializeField]
    private GameObject selectionBorder;

    // =========================
    // Estado actual
    // =========================

    private GridTile currentTile;
    private bool isDragging = false;
    private bool canMove = true;
    private Vector3 offset;
    private Vector3 battlePosition;
    private static PlayerMovemt currentDragging;

    // Seleccionar y arrastrar unidad
    void OnMouseDown()
    {
        // Bloquear arrastre durante combate
        if (!canMove)
        {
            return;
        }

        // Evitar seleccionar otra unidad mientras una ya está siendo movida
        if ( currentDragging != null && currentDragging != this)
        {
            return;
        }

        isDragging = !isDragging;
        selectionBorder.SetActive(isDragging);

        if (isDragging)
        {
            currentDragging = this;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition);
            mousePos.z = 0;
            offset = transform.position - mousePos;
        }
        // Colocar la unidad
        else
        {
            currentDragging = null;
            SnapObject();
        }
    }

    // Seguir al ratón mientras se arrastra
    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos + offset;
        }
    }

    // Ubicar la unidad en una cuadricula exacta o en un slot de la máquina de fusión
    void SnapObject()
    {
        Collider2D[] hits = Physics2D.OverlapPointAll( transform.position);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == gameObject)
            {
                continue;
            }

            // ===========================
            // Casilla del máquina fusión
            // ===========================

            if (hit.CompareTag("FusionSlot"))
            {
                ReleaseCurrentTile();
                transform.SetParent(hit.transform);
                transform.position = hit.transform.position;
                return;
            }

            // =========================
            // Casilla del tablero
            // =========================

            if (hit.CompareTag("GridTile"))
            {
                GridTile tile = hit.GetComponent<GridTile>();

                if (tile != null)
                {
                    if (!tile.playerZone)
                    {
                        return;
                    }
                    if (tile.occupied)
                    {
                        return;
                    }

                    ReleaseCurrentTile();
                    tile.occupied = true;
                    currentTile = tile;
                    transform.SetParent(null);
                    transform.position = hit.transform.position;
                    return;
                }
            }
        }
    }

    // Libera la casilla actual ocupada
    void ReleaseCurrentTile()
    {
        if (currentTile != null)
        {
            currentTile.occupied = false;

            currentTile = null;
        }
    }

    // Activa o desactiva el movimiento
    public void SetCanMove(bool value)
    {
        canMove = value;

        if (!canMove)
        {
            isDragging = false;
            selectionBorder.SetActive(false);
        }
    }

    // Guarda la posición antes del combate
    public void SaveBattlePosition()
    {
        battlePosition = transform.position;
    }

    // Devuelve la unidad a la posición guardada
    public void ReturnToStartPosition()
    {
        transform.position = battlePosition;
    }
}
