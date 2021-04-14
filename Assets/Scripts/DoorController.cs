using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public PlayerController playerScript;
    public Rigidbody2D player;
    public AudioClip doorUnlock;
    AudioSource audioSource;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            openDoor();
        }
    }

    void openDoor(){
        for (int i = 0; i < playerScript.inventory.Count; i++) {
            if(playerScript.inventory[i].ToString() == "key(Clone) (UnityEngine.GameObject)" && playerScript.inventory[i].activeSelf){
                RaycastHit2D hit = Physics2D.CircleCast(player.position + Vector2.down*0.8f, 0.275f, playerScript.lookDirection, 0.7f, LayerMask.GetMask("Door"));
                if(hit.collider != null){
                    GameObject door = hit.collider.gameObject;
                    door.transform.Rotate(0f, 180f, 0f);
                    door.transform.position = new Vector3(door.transform.position.x+1.1f, -2.01f, .05f);
                    audioSource.PlayOneShot(doorUnlock);
                    Destroy(door.GetComponent<BoxCollider2D>());
                }
            }
        }
    }
}
