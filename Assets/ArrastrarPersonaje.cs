using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemt : MonoBehaviour
{
    [SerializeField]
    private float tileSize = 1f;

    [SerializeField]
    private GameObject selectionBorder;

    private bool isDragging = false;
    private Vector3 offset;

    void OnMouseDown()
    {
        // Cambiar estado al hacer click
        isDragging = !isDragging;

        // Activar/desactivar borde
        selectionBorder.SetActive(isDragging);

        // Calcular offset solo al empezar a arrastrar
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            offset = transform.position - mousePos;
        }
        else
        {
            // Ajustar a la cuadrícula al soltar
            float x = Mathf.Round(transform.position.x / tileSize) * tileSize;
            float y = Mathf.Round(transform.position.y / tileSize) * tileSize;

            transform.position = new Vector3(x, y, 0);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FusionSlot"))
        {
            transform.SetParent(other.transform);

            // Ajustar posición al centro del slot
            transform.position = other.transform.position;
        }
    }
}
