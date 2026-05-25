using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboDeVida : MonoBehaviour
{
    [SerializeField]
    private int tier = 1;

    private CharacterStats stats;

    void Start()
    {
        stats = GetComponent<CharacterStats>();

        float lifeSteal = 0f;

        switch (tier)
        {
            case 1:
                lifeSteal = 0.10f;
                break;
            case 2:
                lifeSteal = 0.20f;

                break;
            case 3:
                lifeSteal = 0.35f;
                break;
        }

        stats.AddLifeSteal(
            lifeSteal
        );

        SkillInfo skill = new SkillInfo();

        skill.skillName =
            "RoboVital (" +
            (lifeSteal * 100f) +
            "%)" + "-" + "T" + tier;

        skill.description =
            (lifeSteal * 100f) +
            "% del daño cura al atacante";

        stats.skills.Add(skill);
    }
}
