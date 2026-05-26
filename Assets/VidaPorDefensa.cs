using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaPorDefensa : MonoBehaviour, Habilidad
{
    [SerializeField]
    private int tier = 1;

    private CharacterStats stats;

    void Start()
    {
        stats = GetComponent<CharacterStats>();

        float defenseMultiplier = 1f;
        float vidaMultiplier = 1f;

        switch (tier)
        {
            case 1:

                // -50% defensa
                defenseMultiplier = 0.5f;

                // +50% vida
                vidaMultiplier = 1.5f;

                break;

            case 2:
                defenseMultiplier = 0.4f;
                vidaMultiplier = 2f;

                break;

            case 3:
                defenseMultiplier = 0.3f;
                vidaMultiplier = 2.5f;

                break;
        }

        stats.SetDefenseMultiplier(
            defenseMultiplier
        );

        stats.SetVidaMultiplier(
            vidaMultiplier
        );

        SkillInfo skill = new SkillInfo();

        skill.skillName =
            "VidaPorDefensa " + ((1f - defenseMultiplier) * 100f) + "%" +  "-"  + "T" + tier;

        skill.description =
            "-" +
            ((1f - defenseMultiplier) * 100f) +
            "% defensa, +" +
            ((vidaMultiplier - 1f) * 100f) +
            "% vida";

        if (!stats.HasSkill(skill.skillName))
        {
            stats.skills.Add(skill);
        }
    }
}
