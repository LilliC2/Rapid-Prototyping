using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyP2 : GameBehaviour
{
    // Start is called before the first frame update
    Rigidbody enemyRb;
    public GameObject player;
    public float speed;

    float enemyHealth = _EM2.enemyHealth;
    float enemyHealthMax = _EM2.enemyHealth;
    float enemyDmg = _EM2.enemyDmg;

    public float knockBack = 50;

    private NavMeshAgent enemyAgent;

    public Image healthBar;

    public GameObject canvas;

    public Camera mainCam;

    public ParticleSystem deathPuff;
    public GameObject model;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        enemyAgent = GetComponent<NavMeshAgent>();
        healthBar = GetComponentInChildren<Image>();
        mainCam = FindObjectOfType<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyAgent.SetDestination(player.transform.position);

        healthBar.fillAmount = enemyHealth / enemyHealthMax;
        canvas.transform.LookAt(mainCam.transform);

        

    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            print("Hit");
            enemyHealth -= _PC2.bulletDmg;
            if (enemyHealth <= 0)
            {
                print("Dead");
                //give player xp
                _PC2.expPoints += 2;
                ParticleSystem dirtParticle = deathPuff.GetComponentInChildren<ParticleSystem>(true);
                deathPuff.Play(true);
                model.SetActive(false);
                //destroy
                ExecuteAfterSeconds(0.5f, () => Destroy(gameObject));
            }
            
            //knock back
            Vector3 moveDirectrion = this.transform.position - collision.transform.position;
            this.GetComponent<Rigidbody>().AddForce(moveDirectrion * -knockBack);

           
        }
 
    }
}
