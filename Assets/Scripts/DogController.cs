using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    public Transform playerTransform;
    public PlayerController playerScript;
    public SlotScript slotScript;
    public GameObject dog;
    public Rigidbody2D dogRb;
    public GameObject key;
    public textcontroller text;

    int boneIndex = 0;
    bool hasMoved = false;

    public Sprite dogWithBone;

    public float speed = 15.0f;

    void FixedUpdate() {
        for (int i = 0; i < playerScript.inventory.Count; i++) {
            Vector2 position = dogRb.position;
            if (playerScript.inventory[i].ToString() == "bone(Clone) (UnityEngine.GameObject)" && playerScript.inventory[i].activeSelf && playerTransform.position.y < -1.5 && playerTransform.position.x > -4.2 && playerTransform.position.x < -3.5) {
                if (position.x > -3.75) {
                    position.x = position.x - speed * Time.deltaTime;
                    dogRb.MovePosition(position);
                    boneIndex = i;

                }
                else{
                    hasMoved = true;
                }
            }
        }
    }

    void Update(){
        if (hasMoved && Input.GetKeyDown(KeyCode.E)) {
            dog.GetComponent<SpriteRenderer>().sprite = dogWithBone;
            playerScript.RemoveFromInventory(playerScript.inventory[boneIndex]);
            hasMoved = false;

            playerScript.AddToInventory(key);
            slotScript.NoSlot();

            if (SlotScript.gotSecondItem == false)
            {
                text.tutorialMessage("use the number keys to select which item to carry");
                SlotScript.gotSecondItem = true;
            }
            
        }
    }
}
