using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Barra de vida visual asociada a un personaje.
//
// Actualiza dinámicamente el porcentaje de vida mostrado en función de las estadísticas actuales.
public class HealthBar : MonoBehaviour
{
    // Imagen utilizada como relleno de la barra
    [SerializeField]
    private Image fillImage;

    private CharacterStats stats;

    // Obtener el valor de vida del personaje
    void Start()
    {
        stats = GetComponentInParent<CharacterStats>();
    }

    // Actualizar porcentaje de vida visible
    void Update()
    {
        fillImage.fillAmount = stats.health / stats.maxHealth;
    }
}
