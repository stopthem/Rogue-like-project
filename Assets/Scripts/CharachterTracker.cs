﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterTracker : MonoBehaviour
{
    public static CharachterTracker instance;
    public int currentHealth, maxHealth, currentCoin;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
