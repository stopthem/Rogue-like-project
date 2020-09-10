using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float movingSpeed;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;
    public Animator animator;
    public int enemyHealth = 100;
    public GameObject deathVFX;
    public GameObject[] deathSplatters;
    public bool shouldShoot;
    public GameObject enemyBullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    public float rangeToAggro;
    public SpriteRenderer enemyBody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
        ChasePlayer();
        if (shouldShoot&&Vector3.Distance(transform.position,PlayerController.instance.transform.position)<rangeToAggro)
        {
            Shoot();
        }
        }
        else
        {
            rigidBody.velocity=Vector2.zero;
        }
      
    }

    private void Shoot()
    {
        fireCounter-=Time.deltaTime;
        if (fireCounter<=0)
        {
            fireCounter=fireRate;
            Instantiate(enemyBullet,firePoint.position,firePoint.rotation);
            AudioManager.instance.PlaySFX(13);
        }
    }

    private void ChasePlayer()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
        {
            moveDirection = PlayerController.instance.transform.position - transform.position;
            animator.SetBool("isChasing",true);

        }
        else
        {
            moveDirection = Vector3.zero;
            animator.SetBool("isChasing",false);
        }
        moveDirection.Normalize();
        rigidBody.velocity = moveDirection * movingSpeed;
    }
    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        Instantiate(deathVFX,transform.position,transform.rotation);
        AudioManager.instance.PlaySFX(2);
        if (enemyHealth<=0)
        {
            // Instantiate(deathVFX,transform.position,transform.rotation);
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(1);
            int selectedSplatter=Random.Range(0,deathSplatters.Length);
            int rotation =Random.Range(0,4);
            Instantiate(deathSplatters[selectedSplatter],transform.position,Quaternion.Euler(0f,0f,rotation * 90f));
        }
    }
}
