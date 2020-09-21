using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterSelectManager : MonoBehaviour
{
    public static CharachterSelectManager instance;
    public PlayerController activePlayer;
    public CharachterSelect activeCharachter; 
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
