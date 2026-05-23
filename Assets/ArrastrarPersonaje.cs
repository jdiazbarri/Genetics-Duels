using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemt : MonoBehaviour
{
    [SerializeField]
    private GameObject selectionBorder;
    private GridTile currentTile;
    private bool isDragging = false;
    private Vector3 offset;

    void OnMouseDown()
    {
        isDragging = !isDragging;

        selectionBorder.SetActive(isDragging);

        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            offset = transform.position - mousePos;
        }
        else
        {
            SnapObject();
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            transform.position = mousePos + offset;
        }
    }

    void SnapObject()
    {
        Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);

        // PRIMERO comprobar FusionSlot
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == gameObject)
                continue;

            if (hit.CompareTag("FusionSlot"))
            {
                ReleaseCurrentTile();

                transform.SetParent(hit.transform);
                transform.position = hit.transform.position;

                return;
            }
        }

        // DESPUÉS comprobar GridTile
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == gameObject)
                continue;

            if (hit.CompareTag("GridTile"))
            {
                GridTile tile = hit.GetComponent<GridTile>();

                if (tile != null)
                {
                    // Solo zona jugador
                    if (!tile.playerZone)
                        return;

                    // Solo una unidad
                    if (tile.occupied)
                        return;

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

    void ReleaseCurrentTile()
    {
        if (currentTile != null)
        {
            currentTile.occupied = false;
        }
    }
}
