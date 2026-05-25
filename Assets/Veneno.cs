using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Veneno : MonoBehaviour
{
    [SerializeField]
    private int tier = 1;

    private CharacterStats stats;

    void Start()
    {
        stats = GetComponent<CharacterStats>();

        float poisonPercent = 0f;
        float poisonDuration = 0f;

        switch (tier)
        {
            case 1:

                // 3% durante 3s
                poisonPercent = 0.03f;
                poisonDuration = 2f;

                break;

            case 2:

                // 5% durante 4s
                poisonPercent = 0.05f;
                poisonDuration = 3f;

                break;

            case 3:

                // 8% durante 5s
                poisonPercent = 0.08f;
                poisonDuration = 5f;

                break;

        }

        SkillInfo skill = new SkillInfo();

        skill.skillName =
            "Veneno (" +
            (poisonPercent * 100f) +
            "%)";

        skill.description =
            "Aplica " +
            (poisonPercent * 100f) +
            "% del daño durante " +
            poisonDuration +
            "s";

        stats.skills.Add(skill);
    }
}
