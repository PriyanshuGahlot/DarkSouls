using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;
    public Transform boxCastPos;
    public LayerMask groundLayer;

    [HideInInspector]
    public int maxLightRadius;
    [HideInInspector]
    public float currLightRadius;
    Rigidbody2D rb;
    [HideInInspector]
    public float health;
    Light2D healthLight;
    [HideInInspector]
    public float currOuterRadius;
    [HideInInspector]
    public float currInnerRadius;

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        health = 100;
        healthLight = GetComponent<Light2D>();

        float aspect = (float)Screen.width / Screen.height;
        float worldHeight = Camera.main.orthographicSize * 2;
        float worldWidth = worldHeight * aspect;
        maxLightRadius = (int)(worldWidth/2f);
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        //float y = Input.GetAxis ("Vertical");
        transform.position += new Vector3(x * movementSpeed * Time.deltaTime, 0);

        healthLight.pointLightOuterRadius = maxLightRadius * health / 100;
        healthLight.pointLightInnerRadius = healthLight.pointLightOuterRadius - 3;
        currLightRadius = healthLight.pointLightInnerRadius;
        if (healthLight.pointLightInnerRadius < 0) healthLight.pointLightInnerRadius = 0;
        if (healthLight.pointLightOuterRadius <= 0) healthLight.pointLightOuterRadius = 3;
        currInnerRadius = healthLight.pointLightInnerRadius;
        currOuterRadius = healthLight.pointLightOuterRadius;
        if (health < 0) Death();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded()) rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    bool isGrounded()
    {
        return Physics2D.BoxCast(boxCastPos.position, new Vector2(1,0.1f),0,Vector2.down, 0.1f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy")) Death();
    }

    private void Death()
    {
        Debug.Log("player died");
    }
}
