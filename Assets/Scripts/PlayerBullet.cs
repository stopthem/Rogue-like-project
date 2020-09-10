using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed=7.5f;
    public Rigidbody2D rigidBody;
    public GameObject bulletImpactVFX;
    public int damageToGive=50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity=transform.right * bulletSpeed;

    }
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Instantiate(bulletImpactVFX,transform.position,transform.rotation);
        if (other.tag=="Enemy")
        {
            other.GetComponent<EnemyController>().TakeDamage(damageToGive);
        }
        Destroy(gameObject);
    }
}
