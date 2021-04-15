using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandFunction : MonoBehaviour
{

    public GameObject player;
    public PlayerController playerScript;
    
    public GameObject projectilePrefab;
    public float projectileForce = 2f;
    public float spawnRadiusMultiplier = 0.5f;

    void Update() {
        for (int i = 0; i < playerScript.inventory.Count; i++) {
            if (playerScript.inventory[i].ToString() == "wandtest(Clone) (UnityEngine.GameObject)" && playerScript.inventory[i].activeSelf) {
                //If player is holding the wand
                if(Input.GetKeyDown(KeyCode.Q)) {
                    //Shoot object on press of Q
                    Shoot();
                }
            }
        }
    }

    public void Shoot() {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 worldMousePos = Input.mousePosition;
        Vector2 direction = (Vector2)((worldMousePos - player.GetComponent<Transform>().position));
        direction.Normalize();
        //GameObject projectile = (GameObject)Instantiate(projectilePrefab, transform.position + (Vector3)(direction * spawnRadiusMultiplier), Quaternion.identity);
        GameObject projectile = (GameObject)Instantiate(projectilePrefab, player.GetComponent<Transform>().position + (Vector3)(direction * spawnRadiusMultiplier), transform.rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
        Destroy(projectile, 10);    
    }

}
