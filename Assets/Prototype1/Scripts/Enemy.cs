using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameBehaviour
{
    Rigidbody enemyRb;
    GameObject player;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection* speed);

        if(transform.position.y < -5)
        {
            _P1GM.score++;
            _P1UI.UpdateScore(_P1GM.score);
            Destroy(gameObject);
        }
    }

    
}
