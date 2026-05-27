using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCard : MonoBehaviour
{
    private CharacterBoard board;

    public GameObject[] characterPrefabs;

    public void Init(CharacterBoard b)
    {
        board = b;
    }

    void OnMouseDown()
    {
        if (board == null) return;

        board.OnCardClicked(this);
    }
}
