using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform player;
    public float speed;
    public float yOffset;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 playerPos = player.position;
        rb.velocity = (new Vector2(playerPos.x,playerPos.y+yOffset) - (Vector2)transform.position) * speed;
    }
}
