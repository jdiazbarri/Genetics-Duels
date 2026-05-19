using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionMachine : MonoBehaviour
{
    [Header("Slots")]
    public Transform slotA;
    public Transform slotB;
    public Transform resultSlot;

    private GameObject currentResult;

    void Update()
    {
        CheckFusion();
    }

    void CheckFusion()
    {
        if (slotA.childCount > 0 && slotB.childCount > 0)
        {
            if (currentResult == null)
            {
                GameObject original;

                if (Random.value < 0.5f)
                    original = slotA.GetChild(0).gameObject;
                else
                    original = slotB.GetChild(0).gameObject;

                currentResult = Instantiate(
                    original,
                    resultSlot.position,
                    Quaternion.identity,
                    resultSlot
                );

                SpriteRenderer sr = currentResult.GetComponentInChildren<SpriteRenderer>();

                if (sr != null)
                {
                    sr.color = new Color(
                        Random.value,
                        Random.value,
                        Random.value
                    );
                }
            }
        }
        else
        {
            if (currentResult != null)
            {
                Destroy(currentResult);
                currentResult = null;
            }
        }
    }
}
