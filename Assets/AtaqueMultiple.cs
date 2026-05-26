using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueMultiple : MonoBehaviour, Habilidad
{
    [SerializeField]
    private int tier = 1;

    private CharacterStats stats;

    void Start()
    {
        stats = GetComponent<CharacterStats>();

        int extraProjectiles = 0;

        switch (tier)
        {
            case 1:
                extraProjectiles = 2;
                break;

            case 2:
                extraProjectiles = 3;
                break;

            case 3:
                extraProjectiles = 4;
                break;
        }

        stats.AddProjectiles(
            extraProjectiles
        );

        SkillInfo skill = new SkillInfo();

        skill.skillName =
            "MultiShot "+
            " (+" + extraProjectiles + ")" + "-T" + tier;

        skill.description =
            "Dispara " +
            extraProjectiles +
            " proyectiles extra";

        if (!stats.HasSkill(skill.skillName))
        {
            stats.skills.Add(skill);
        }
    }
}
