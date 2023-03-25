using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyP2 : GameBehaviour
{
    // Start is called before the first frame update
    Rigidbody enemyRb;
    public GameObject player;
    public float speed;

    float enemyHealth = _EM2.enemyHealth;
    float enemyDmg = _EM2.enemyDmg;

    public float knockBack = 50;

    private NavMeshAgent enemyAgent;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyAgent.SetDestination(player.transform.position);

        if (enemyHealth == 0)
        {
            print("Dead");
            //give player xp
            _PC2.expPoints += 2;

            //destroy
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            print("Hit");
            enemyHealth -= _PC2.bulletDmg;
            
            //knock back
            Vector3 moveDirectrion = this.transform.position - collision.transform.position;
            this.GetComponent<Rigidbody>().AddForce(moveDirectrion * -knockBack);

           
        }
 
    }
}
