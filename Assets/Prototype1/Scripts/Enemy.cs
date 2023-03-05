using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameBehaviour<Enemy>
{
    Rigidbody enemyRb;
    public GameObject player;
    public float speed;

    [SerializeField] ParticleSystem impactParticle = null;
    public bool once = true;

    public AudioSource impactSound;

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

        if(transform.position.y < -2)
        {
            _P1GM.score++;
            _P1UI.UpdateScore(_P1GM.score);
            Destroy(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            impactSound.Play();
            impactParticle.Play();
        }
    }





}
