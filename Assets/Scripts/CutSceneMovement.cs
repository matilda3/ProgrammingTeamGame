using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public Rigidbody2D player;

    void FixedUpdate()
    {
        float step = speed * Time.deltaTime;
        Vector2 target = new Vector2(-10, 0);
        player.position = Vector2.MoveTowards(player.position, target, step);
    }
}