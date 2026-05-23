using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image fillImage;

    private CharacterStats stats;

    void Start()
    {
        stats = GetComponentInParent<CharacterStats>();
    }

    void Update()
    {
        fillImage.fillAmount =
            stats.vida / stats.vidaMaxima;
    }
}
