using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    float horizontal;
    float vertical;
    public List<GameObject> inventory = new List<GameObject>();

    public SlotScript slotScript;

    public textcontroller text;

    public float speed = 3.0f;

    bool hasPassedDoor = false;

    //variables to see if the player has done something yet - used to turn off the tutorial message when they do something the first time
    bool moved;
    bool interacted;

    Animator animator;
    public Vector2 lookDirection = new Vector2(1, 0);

    AudioSource audioSource;
    public AudioClip collectedAudio;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();



        moved = false;
        interacted = false;

        text.tutorialMessage("use the arrow keys or WASD to move");
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
                
        //animator.SetFloat("Look X", lookDirection.x);
        //animator.SetFloat("Look Y", lookDirection.y);
        //animator.SetFloat("Speed", move.magnitude);

        //when they press the arrow key/wasd for the first time, the tutorial message goes away (there's probably an easier way to do this but also i'm lazy)
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            if(moved == false)
            {
                moved = true;
                text.tutorialMessage("press e to interact with objects");
            }
        }

        if(Input.GetKeyDown(KeyCode.E)){
            Pickup();
            if(interacted == false)
            {
                interacted = true;
                text.tutorialMessage("");
            }
            
        }

        if(rb.position.y < -2.3 && !hasPassedDoor){
            GetComponent<SpriteRenderer>().sortingOrder = 5;
            hasPassedDoor = true;
            slotScript.setItemsLayer();
        }
        else if(rb.position.y > -2.3){
            GetComponent<SpriteRenderer>().sortingOrder = 1;
            hasPassedDoor = false;
            slotScript.setItemsLayer();
        }
    }

    void FixedUpdate(){
        Vector2 position = rb.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rb.MovePosition(position);
    }

    public bool IsInInventory(GameObject item){
        if(inventory.Contains(item)){
            return true;
        }
        return false;
    }

    public void RemoveFromInventory(GameObject item){
        inventory.Remove(item);
        Destroy(item);
        slotScript.setSlotEmpty(inventory.Count);
    }

    public void AddToInventory(GameObject item){
        //float scaleOfCopy = item.transform.localScale.x / transform.localScale.x;
        //Debug.Log(scaleOfCopy);
        GameObject copy = Instantiate(
            item, gameObject.transform.GetChild(0).position, 
            gameObject.transform.GetChild(0).rotation, transform);
        //copy.transform.localScale = new Vector3(scaleOfCopy, scaleOfCopy, scaleOfCopy);
        copy.transform.localScale = new Vector3(0.355f, 0.355f, 0.355f);
        //All pick-up-able items need a BoxCollider and CircleCollider; two BoxColliders is a no-no.
        Destroy(copy.GetComponent<BoxCollider2D>());
        Destroy(copy.GetComponent<CircleCollider2D>());
        copy.SetActive(false);
        inventory.Add(copy);
        Destroy(item);
    }

    void Pickup() { //Item needs to be in the Item Layer
        RaycastHit2D hit = Physics2D.CircleCast(rb.position + Vector2.down*0.8f, 0.275f, lookDirection, 0.7f, LayerMask.GetMask("Items"));
        if(hit.collider != null){
            GameObject interactable = hit.collider.gameObject;
            AddToInventory(interactable);
            audioSource.PlayOneShot(collectedAudio);
            slotScript.SwapSlot(inventory.Count-1);
        }
    }

    public void PlaySound(AudioClip clip){
        audioSource.PlayOneShot(clip);
    }


    //needs NonPlayerCharacterScript
    //for dialogue and interaction
    /*void Interaction(){
        RaycastHit2D hit = 
            Physics2D.Raycast(rb.position +Vector2.up * 
            0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if(hit.collider != null){
                NonPlayerCharacter character =
                hit.collider.GetComponent<NonPlayerCharacter>();
                if(character != null){
                    character.DisplayDialog();
                }
            }
    }*/
}
