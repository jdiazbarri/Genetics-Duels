using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetivosMultiples: MonoBehaviour, Habilidad
{
    [SerializeField]
    private int tier = 1;

    private CharacterStats stats;

    void Start()
    {
        stats = GetComponent<CharacterStats>();

        int extraTargets = 1;

        switch (tier)
        {
            case 1:
                extraTargets = 1;
                break;

            case 2:
                extraTargets = 2;
                break;

            case 3:
                extraTargets = 3;
                break;
        }

        stats.AddTargets(
            extraTargets
        );

        SkillInfo skill = new SkillInfo();

        skill.skillName =
            "MultiTarget " +
            " (+" + extraTargets + ")" + "-" + tier;

        skill.description =
            "Ataca " +
            extraTargets +
            " enemigos extra";

        if (!stats.HasSkill(skill.skillName))
        {
            stats.skills.Add(skill);
        }
    }
}
