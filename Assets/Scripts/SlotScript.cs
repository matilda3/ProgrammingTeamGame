using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour {

    public GameObject player;
    public PlayerController playerScript;
    public AudioClip switchNoise;

    public Sprite emptySlot;

    public textcontroller text;

    //variable to store if the player has swapped their slot so the tutorial text knows what to say
    bool swappedSlot;
    public static bool gotSecondItem;

    void Start()
    {
        swappedSlot = false;
        gotSecondItem = false;
    }

    void Update() {
        for(int i = 0; i < playerScript.inventory.Count; i++) {
            Sprite objectSprite = playerScript.inventory[i].GetComponent<SpriteRenderer>().sprite;
            SwapImage(objectSprite, i);
        }

        if (playerScript.inventory.Count > 1 && gotSecondItem == false)
        {
            gotSecondItem = true;

            text.tutorialMessage("use the number keys to select which item to carry");
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            SwapSlot(0);

            if (swappedSlot == false)
            {
                swappedSlot = true;

                text.tutorialMessage("");

            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            SwapSlot(1);

            if (swappedSlot == false)
            {
                swappedSlot = true;

                text.tutorialMessage("");

            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            SwapSlot(2);

            if (swappedSlot == false)
            {
                swappedSlot = true;

                text.tutorialMessage("");

            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            SwapSlot(3);

            if (swappedSlot == false)
            {
                swappedSlot = true;

                text.tutorialMessage("");

            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)){
            SwapSlot(4);

            if (swappedSlot == false)
            {
                swappedSlot = true;

                text.tutorialMessage("");

            }
        }
    }

    public void setSlotEmpty(int slot){
        this.transform.GetChild(slot).GetChild(0).GetComponent<Image>().sprite = emptySlot;
    }

    public void setItemsLayer(){
        for(int i = 0; i < playerScript.inventory.Count; i++) {
            player.transform.GetChild(i + 1).GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder - 1;
        }
    }

    public void SwapSlot(int slot){
        playerScript.PlaySound(switchNoise);
            for(int i = 0; i < playerScript.inventory.Count; i++) {
                if(i == slot) {
                    playerScript.inventory[i].SetActive(true);
                    this.transform.GetChild(i).GetComponent<Image>().color = Color.yellow;
                }
                else {
                    playerScript.inventory[i].SetActive(false);
                    this.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                }
            }

        
    }

    public void NoSlot(){
        playerScript.PlaySound(switchNoise);
            for(int i = 0; i < playerScript.inventory.Count; i++) {
                playerScript.inventory[i].SetActive(false);
                this.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
    }

    void SwapImage(Sprite newSprite, int slot) {
        this.transform.GetChild(slot).GetChild(0).GetComponent<Image>().sprite = newSprite;
    }

}


