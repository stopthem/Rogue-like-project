using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterTracker : MonoBehaviour
{
    public static CharachterTracker instance;
    public int currentHealth, maxHealth, currentCoin;

    private void Awake()
    {
        instance = this;
    }
}
