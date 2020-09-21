using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterSelect : MonoBehaviour
{
    public GameObject message;
    private bool canSelect;
    public PlayerController playerToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSelect)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3 playerPosition = PlayerController.instance.transform.position;
                Destroy(PlayerController.instance.gameObject);
                PlayerController newPlayer = Instantiate(playerToSpawn,playerPosition,playerToSpawn.transform.rotation);
                PlayerController.instance = newPlayer;
                gameObject.SetActive(false);
                CameraController.instance.target = newPlayer.transform;
                CharachterSelectManager.instance.activePlayer = newPlayer;
                CharachterSelectManager.instance.activeCharachter.gameObject.SetActive(true);
                CharachterSelectManager.instance.activeCharachter = this;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canSelect = true;
            message.SetActive(true);
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canSelect = false;
            message.SetActive(false);
        }
    }
}
