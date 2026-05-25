using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolpeMultiple : MonoBehaviour
{
    [SerializeField]
    private int tier = 1;

    private CharacterStats stats;

    void Start()
    {
        stats = GetComponent<CharacterStats>();

        int extraHits = 0;

        switch (tier)
        {
            case 1:
                extraHits = 2;
                break;

            case 2:
                extraHits = 3;
                break;

            case 3:
                extraHits = 4;
                break;
        }

        stats.AddAttacks(extraHits);

        SkillInfo skill = new SkillInfo();

        skill.skillName =
            "MultiHit " +
            " (+" + extraHits + ")" + "-" + tier;

        skill.description =
            "Realiza " +
            extraHits +
            " golpes extra";

        stats.skills.Add(skill);
    }

}
