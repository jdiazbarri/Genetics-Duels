using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaduraPorVida : MonoBehaviour
{
    [SerializeField]
    private int tier = 1;

    private CharacterStats stats;

    void Start()
    {
        stats = GetComponent<CharacterStats>();

        float vidaMultiplier = 1f;
        float defenseMultiplier = 1f;

        switch (tier)
        {
            case 1:
                vidaMultiplier = 0.6f;
                defenseMultiplier = 1.8f;

                break;

            case 2:
                vidaMultiplier = 0.5f;
                defenseMultiplier = 2.2f;

                break;

            case 3:

                vidaMultiplier = 0.4f;
                defenseMultiplier = 2.8f;

                break;
        }

        stats.SetVidaMultiplier(
            vidaMultiplier
        );

        stats.SetDefenseMultiplier(
            defenseMultiplier
        );

        SkillInfo skill = new SkillInfo();

        skill.skillName =
            "ArmaduraPorVida "  + ((1f - vidaMultiplier) * 100f) + "%" + "-" + "T" +tier;

        skill.description =
            "-" +
            ((1f - vidaMultiplier) * 100f) +
            "% vida, +" +
            ((defenseMultiplier - 1f) * 100f) +
            "% defensa";

        stats.skills.Add(skill);
    }
}
