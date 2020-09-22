using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public static BossController instance;

    public BossAction[] actions;
    private int currentAction;
    private float actionCounter;

    private float shotCounter;
    private Vector2 moveDirection;
    public Rigidbody2D theRB2D;

    public int currentHealth, maxHealth;

    public GameObject deathFX, hitEffect;
    public GameObject levelExit;

    public BossSequence[] sequences;
    public int currentSequence;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        actions = sequences[currentSequence].actions;
        actionCounter = actions[currentAction].actionLength;
        UIController.instance.bossHealthSlider.maxValue = currentHealth;
        UIController.instance.bossHealthSlider.value = currentHealth;
        AudioManager.instance.PlayBossMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (actionCounter > 0)
        {
            actionCounter -= Time.deltaTime;
            HandleMovement();
            if (actions[currentAction].shouldShoot)
            {
                Shoot();
            }
        }
        else
        {
            currentAction++;
            if (currentAction >= actions.Length)
            {
                currentAction = 0;
            }
            actionCounter = actions[currentAction].actionLength;
        }
    }

    private void Shoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            shotCounter = actions[currentAction].timeBetweenShots;
            foreach (Transform s in actions[currentAction].shotPoints)
            {
                AudioManager.instance.PlaySFX(17);
                Instantiate(actions[currentAction].bulletToShoot, s.position, s.rotation);
            }
        }
    }

    private void HandleMovement()
    {
        moveDirection = Vector2.zero;
        if (actions[currentAction].shouldMove)
        {
            if (actions[currentAction].shouldChasePlayer)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
                moveDirection.Normalize();
            }
            if (actions[currentAction].moveToPoints && Vector3.Distance(transform.position,actions[currentAction].pointToMove.position)> .5f)
            {
                moveDirection = actions[currentAction].pointToMove.position - transform.position;
                moveDirection.Normalize();
            }
        }

        theRB2D.velocity = moveDirection * actions[currentAction].moveSpeed;
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            AudioManager.instance.PlaySFX(10);
            gameObject.SetActive(false);
            AudioManager.instance.bossMusic.Stop();
            Instantiate(deathFX,transform.position, transform.rotation);
            levelExit.SetActive(true);
            if (Vector3.Distance(PlayerController.instance.transform.position, levelExit.transform.position)< 2f)
            {
                levelExit.transform.position += new Vector3(4f,0f,0f);
            }
            UIController.instance.bossHealthSlider.gameObject.SetActive(false);
        }
        else
        {
            if (currentHealth <= sequences[currentSequence].endSequenceHealth && currentSequence < sequences.Length - 1)
            {
                currentSequence++;
                actions = sequences[currentSequence].actions;
                currentAction = 0;
                actionCounter = actions[currentAction].actionLength;
            }
        }
        UIController.instance.bossHealthSlider.value = currentHealth;
        
    }
}
[System.Serializable]
public class BossAction
{
    [Header("Action")]
    public float actionLength;

    public bool shouldMove;
    public bool shouldChasePlayer;
    public float moveSpeed;

    public bool moveToPoints;
    public Transform pointToMove;

    public bool shouldShoot;
    public GameObject bulletToShoot;
    public float timeBetweenShots;
    public Transform[] shotPoints;
}
[System.Serializable]
public class BossSequence
{
    [Header("Sequence")]
    public BossAction[] actions;


    public int endSequenceHealth;
}
