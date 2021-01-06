using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChest : MonoBehaviour
{
    public GunPickup[] potentialGuns;
    public SpriteRenderer theSR;
    public Sprite chestOpen;
    public GameObject notification;
    public Transform spawnPoint;
    private bool canOpen, isOpened;
    public float scaleSpeed = 2f;

    void Update()
    {
        if (canOpen && !isOpened)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                int gunSelect = Random.Range(0, potentialGuns.Length);
                Instantiate(potentialGuns[gunSelect], spawnPoint.position, spawnPoint.rotation);
                theSR.sprite = chestOpen;
                isOpened = true;
                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                notification.SetActive(false);
            }
        }
        if (isOpened)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * scaleSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            notification.SetActive(true);
            canOpen = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            notification.SetActive(false);
            canOpen = false;
        }
    }
}
