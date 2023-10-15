using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    public float healDistance;
    public float healRate;
    Transform playerObj;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = FindObjectOfType<Player>().transform;
        player = playerObj.GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, playerObj.position) < healDistance)
        {
            if(player.health<100) player.health += healRate * Time.deltaTime;
        }
    }
}
