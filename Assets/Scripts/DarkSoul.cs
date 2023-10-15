using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class DarkSoul : MonoBehaviour
{
    public enum TypeOfEnemy { petroling,following};
    public TypeOfEnemy typeOfEnemy;
    public float movementSpeed;
    public Transform left;
    public Transform right;
    public Transform head;
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public LayerMask raycastLayer;
    public int damageRate;

    int dir;
    Rigidbody2D rb;
    bool backOnGrnd;
    bool hasChangedDir;
    Player player;
    Transform playerObj;
    Color color;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        dir = 1;
        rb = GetComponent<Rigidbody2D>();
        backOnGrnd = true;
        hasChangedDir = true;
        playerObj = FindObjectOfType<Player>().transform;
        player = playerObj.GetComponent<Player>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
    }

    void FixedUpdate()
    {
        if(Physics2D.Raycast(transform.position, (playerObj.position - transform.position).normalized, player.currLightRadius, raycastLayer).collider != null)
        {
            if(Physics2D.Raycast(transform.position, (playerObj.position - transform.position).normalized, player.currLightRadius, raycastLayer).collider.gameObject.tag == "Player")
            {
                if (player.health > 0) player.health -= damageRate * Time.deltaTime;
            }
        }

        if (typeOfEnemy == TypeOfEnemy.petroling) petrol();
        else if(typeOfEnemy == TypeOfEnemy.following)
        {

        }
    }

    private void Update()
    {
        float currDis = Vector2.Distance(playerObj.position, transform.position);
        float currAlpha = 0;
        if (currDis < player.currInnerRadius) currAlpha = 1;
        else if (currDis > player.currInnerRadius && currDis < player.currOuterRadius) currAlpha = (player.currOuterRadius - currDis) / (player.currOuterRadius - player.currInnerRadius);
        spriteRenderer.color = new Color(color.r, color.g, color.b, currAlpha);
    }

    void petrol()
    {
        rb.velocity = Vector2.right * movementSpeed * dir;
        RaycastHit2D hit = Physics2D.Raycast(head.position, Vector2.up, 0.1f, playerLayer);
        if(hit)
        {
            //death
            Destroy(transform.gameObject);
            hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(player.jumpForce * Vector2.up, ForceMode2D.Impulse);
        }
        bool isLeftGrounded = Physics2D.Raycast(left.position, Vector2.down, 1, groundLayer).collider != null;
        bool isRightGrounded = Physics2D.Raycast(right.position, Vector2.down, 1, groundLayer).collider != null;
        bool isLeftBlocked = Physics2D.Raycast(left.position, Vector2.left, 0.1f, groundLayer).collider != null;
        bool isRightBlocked = Physics2D.Raycast(right.position, Vector2.right, 0.1f, groundLayer).collider != null;

        if (backOnGrnd && hasChangedDir && (!isLeftGrounded || !isRightGrounded || isLeftBlocked || isRightBlocked))
        {
            dir *= -1;
            if (!isLeftGrounded || !isRightGrounded) backOnGrnd = false;
            if (isLeftBlocked || isRightBlocked) hasChangedDir = false;
        }

        backOnGrnd = isLeftGrounded && isRightGrounded;
        hasChangedDir = !isLeftBlocked && !isRightBlocked;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //death
        }
    }

}
