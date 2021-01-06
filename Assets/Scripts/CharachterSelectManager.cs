using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterSelectManager : MonoBehaviour
{
    public static CharachterSelectManager instance;
    public PlayerController activePlayer;
    public CharachterSelect activeCharachter;

    private void Awake()
    {
        instance = this;
    }
}
