﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        // direction=PlayerController.instance.transform.position - transform.position;
        // direction.Normalize(); 
        direction = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        if (!BossController.instance.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            PlayerHealth.instance.DamagePlayer();   
        }
        Destroy(gameObject);
    }
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
