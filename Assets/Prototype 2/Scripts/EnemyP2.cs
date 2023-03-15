using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyP2 : GameBehaviour
{
    // Start is called before the first frame update
    Rigidbody enemyRb;
    public GameObject player;
    public float speed;

    public float enemyHealth = 20;
    public float enemyDmg;

    public float knockBack = 50;

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
        enemyRb.AddForce(lookDirection * speed);

        //if (enemyHealth < 0)
        //{
        //    print("Dead");
        //    //give player xp
        //    _PC2.expPoints += 2;

        //    //destroy
        //    Destroy(gameObject);
        //}

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
