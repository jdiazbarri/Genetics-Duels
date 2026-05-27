using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScaler : MonoBehaviour
{
    public void ScaleCharacter(
        GameObject character,
        int level,
        string type
    )
    {
        CharacterStats stats =
            character.GetComponent<CharacterStats>();

        if (stats == null)
        {
            return;
        }

  

        float statBonus = 1f;

        // ENEMIGOS
        if (type == "Enemy")
        {

            // RESTO +5%
            statBonus =
                1f + (level * 0.05f);
        }

        // PLAYER
        if (type == "Player")
        {
     

            // RESTO +7%
            statBonus =
                1f + (level * 0.07f);
        }



        // DAÑO
        stats.baseDFisico *=
            statBonus;

        // DEFENSA
        stats.baseDefensa *=
            statBonus;

        // RECALCULAR
        stats.UpdateStats();

        // CURAR
        stats.vida =
            stats.vidaMaxima;

        Debug.Log(
            stats.nombre +
            " VIDA FINAL: " +
            stats.vidaMaxima
        );
    }
}
