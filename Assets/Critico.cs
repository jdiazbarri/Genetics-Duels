using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critico : MonoBehaviour
{
    [SerializeField]
    private int tier = 1;

    private CharacterStats stats;

    void Start()
    {
        stats = GetComponent<CharacterStats>();

        float critChance = 0f;

        switch (tier)
        {
            case 1:

                critChance = 0.30f;

                break;

            case 2:

                critChance = 0.50f;

                break;

            case 3:

                critChance = 0.75f;

                break;
        }

        stats.AddCritChance(
            critChance
        );

        SkillInfo skill = new SkillInfo();

        skill.skillName =
            "Crítico (" +
            (critChance * 100f) +
            "%)" + "-" + "T" + tier;

        skill.description =
            "+" +
            (critChance * 100f) +
            "% probabilidad crítico";

        stats.skills.Add(skill);
    }
}
