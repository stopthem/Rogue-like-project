using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public int currentHealth;
    public int maxHealth;
    public float damageInvincibleLenght=1f;
    private float invincibleCount;
    private void Awake() {
        instance=this;
    }
    void Start()
    {
        currentHealth=maxHealth;
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text=currentHealth.ToString()+"/"+maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCount > 0)
        {
            invincibleCount -= Time.deltaTime;
            if (invincibleCount <= 0)
            {
                PlayerController.instance.bodySprite.color=new Color(PlayerController.instance.bodySprite.color.r,PlayerController.instance.bodySprite.color.g,PlayerController.instance.bodySprite.color.b,1f);
            }
        }
    }
    public void DamagePlayer()
    {
        if (invincibleCount <= 0)
        {
            
            currentHealth--;
            AudioManager.instance.PlaySFX(11);
            invincibleCount=damageInvincibleLenght;
            PlayerController.instance.bodySprite.color=new Color(PlayerController.instance.bodySprite.color.r,PlayerController.instance.bodySprite.color.g,PlayerController.instance.bodySprite.color.b,.5f);
            if (currentHealth<=0)
            {
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.deathScreen.SetActive(true);
                AudioManager.instance.PlaySFX(9);
                AudioManager.instance.PlayGameOver();
            }
            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text=currentHealth.ToString()+"/"+maxHealth.ToString();
        }
    }
    public void MakePlayerInvincible(float length)
    {
        invincibleCount = length;
        PlayerController.instance.bodySprite.color=new Color(PlayerController.instance.bodySprite.color.r,PlayerController.instance.bodySprite.color.g,PlayerController.instance.bodySprite.color.b,.5f);
    }
    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text=currentHealth.ToString()+"/"+maxHealth.ToString();
    }
}
