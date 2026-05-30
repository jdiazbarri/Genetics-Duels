using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemt : MonoBehaviour
{
    [SerializeField]
    private GameObject selectionBorder;

    private GridTile currentTile;

    private bool isDragging = false;

    private bool canMove = true;

    private Vector3 offset;

    // POSICIÓN DEL GRID
    private Vector3 battlePosition;

    private static PlayerMovemt currentDragging;

    void OnMouseDown()
    {
        if (!canMove)
            return;

        if (
            currentDragging != null
            && currentDragging != this
        )
        {
            return;
        }

        isDragging = !isDragging;

        selectionBorder.SetActive(
            isDragging
        );

        if (isDragging)
        {
            currentDragging = this;

            Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(
                    Input.mousePosition
                );

            mousePos.z = 0;

            offset =
                transform.position
                - mousePos;
        }
        else
        {
            currentDragging = null;

            SnapObject();
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos =
                Camera.main.ScreenToWorldPoint(
                    Input.mousePosition
                );

            mousePos.z = 0;

            transform.position =
                mousePos + offset;
        }
    }

    void SnapObject()
    {
        Collider2D[] hits =
            Physics2D.OverlapPointAll(
                transform.position
            );

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == gameObject)
                continue;

            // FUSION SLOT
            if (hit.CompareTag("FusionSlot"))
            {
                ReleaseCurrentTile();

                transform.SetParent(
                    hit.transform
                );

                transform.position =
                    hit.transform.position;

                return;
            }

            // GRID
            if (hit.CompareTag("GridTile"))
            {
                GridTile tile =
                    hit.GetComponent<GridTile>();

                if (tile != null)
                {
                    if (!tile.playerZone)
                        return;

                    if (tile.occupied)
                        return;

                    ReleaseCurrentTile();

                    tile.occupied = true;

                    currentTile = tile;

                    // SACAR DEL SLOT
                    transform.SetParent(null);

                    transform.position =
                        hit.transform.position;

                    return;
                }
            }
        }
    }

    void ReleaseCurrentTile()
    {
        if (currentTile != null)
        {
            currentTile.occupied = false;

            currentTile = null;
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;

        if (!canMove)
        {
            isDragging = false;

            selectionBorder.SetActive(
                false
            );
        }
    }

    // GUARDAR POSICIÓN ANTES BATALLA
    public void SaveBattlePosition()
    {
        battlePosition =
            transform.position;
    }

    // VOLVER AL TILE
    public void ReturnToStartPosition()
    {
        transform.position =
            battlePosition;
    }
}
