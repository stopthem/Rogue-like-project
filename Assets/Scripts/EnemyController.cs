using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float movingSpeed;

    [Header("Chasing Player")]
    public bool shouldChasePlayer;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    [Header("Run away")]
    public bool shouldRunAway;
    public float runawayRange;

    [Header("Wandering")]
    public bool shouldWander;
    public float wanderLength, pauseLength;
    private float wanderCounter, pauseCounter;
    private Vector3 wanderDirection;

    [Header("Patrol")]
    public bool shouldPatrol;
    public Transform[] patrolPoints;
    private int currentPatrolPoint;

    [Header("animator")]
    public Animator animator;

    [Header("Enemy Health-Death")]
    public int enemyHealth = 100;
    public GameObject deathVFX;
    public GameObject[] deathSplatters;
    public SpriteRenderer enemyBody;

    [Header("Shooting")]
    public bool shouldShoot;
    public GameObject enemyBullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    public float rangeToAggro;
    
    // Start is called before the first frame update
    void Start()
    {
        if (shouldWander)
        {
            pauseCounter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance =  Vector3.Distance(transform.position,PlayerController.instance.transform.position);
        if (enemyBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            moveDirection = Vector2.zero;
            ChasePlayer();
            if (shouldShoot && distance < rangeToAggro)
            {
                Shoot();
            }
            if (shouldRunAway && distance < runawayRange)
            {
                moveDirection = transform.position - PlayerController.instance.transform.position;
            }
            if (shouldWander)
            {
                if (wanderCounter > 0)
                {
                    wanderCounter -= Time.deltaTime;
                    moveDirection = wanderDirection;
                    animator.SetBool("isChasing", true);
                    if (wanderCounter <= 0)
                    {
                        pauseCounter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);
                        
                    }
                }
                if (pauseCounter > 0)
                {
                    pauseCounter -=Time.deltaTime;
                    if (pauseCounter <=0)
                    {
                        wanderCounter=Random.Range(wanderLength * .75f,wanderLength * 1.25f);
                        wanderDirection = new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),0f);
                    }
                }
            }
            if (shouldPatrol)
            {
                moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;
                animator.SetBool("isChasing", true);

                if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < .2f)
                {
                    currentPatrolPoint++;
                    if (currentPatrolPoint >= patrolPoints.Length)
                    {
                        currentPatrolPoint = 0;
                    }
                }
            }
            moveDirection.Normalize();
            rigidBody.velocity = moveDirection * movingSpeed;
        }
        
        // else
        // {
        //     rigidBody.velocity=Vector2.zero;
        // }
      
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
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer && shouldChasePlayer)
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
