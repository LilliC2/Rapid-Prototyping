using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : GameBehaviour
{
    private Rigidbody rb;
    
    public GameObject focalPoint;
    public bool hasPowerup;
    float powerupStrength = 15f;
    public GameObject powerupIndicator;
    int enemyLayerMask = 3;


    [Header("Movement")]
    Vector3 targetPos;
    bool moveActive;
    public float speed;
    public Ease speedEase;

    [Header("Grand Slam")]
    public float GSheight;
    public float GSradius;
    public float GSspeed;
    public float GScooldown;
    public Ease GSease;
    public Ease GSeaseSlam;
    public Ease GSeaseKnockback;
    bool GSonCooldown;
    public float GSknockbackSpeed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        //float forwardInput = Input.GetAxis("Vertical");
        //rb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        //powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        //Move to mouse click
        if(Input.GetMouseButtonDown(0) && !moveActive)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                targetPos = hit.point;
                targetPos = new Vector3(targetPos.x, gameObject.transform.position.y, targetPos.z);
                gameObject.transform.DOMove(targetPos, speed).SetEase(speedEase);
            }
            
        }

        //Grand Slam
        if(Input.GetKeyDown(KeyCode.Alpha1) && !GSonCooldown)
        {
            GrandSlam();
            //move cool down
            
        }
        
        if(GSonCooldown) _P1UI.UpdateCooldownTimer(GScooldown, _P1UI.GScooldownText);

        //Roll out
        /*player gains momentum
         * player rolls forward
         * anything in the path is knocked away
         * add cool down
         */

    }

    void GrandSlam()
    {
        //moveActive = true; //player cannot move while slamming
        
        //lift player in air
        gameObject.transform.DOMoveY(GSheight, GSspeed).SetEase(GSease);

        //. slam player down
        gameObject.transform.DOMoveY(0, 0.1f).SetEase(GSeaseSlam);
        // any enemies within that radius are knocked back
        Collider[] GSenemiesHit = Physics.OverlapSphere(gameObject.transform.position, GSradius);
        foreach(var GSenemyHit in GSenemiesHit)
        {
            if(GSenemyHit.CompareTag("Enemy"))
            {
                print(GSenemyHit.name + " has been hit");
                Vector3 dir = (gameObject.transform.position + GSenemyHit.gameObject.transform.position);
                GSenemyHit.gameObject.transform.DOMove(dir, GSknockbackSpeed).SetEase(GSeaseKnockback);
            }
        }
        
        
        moveActive = false;

        // add cool down
        GSonCooldown = true;
        
        // when cool down over reset move
        ExecuteAfterSeconds(GScooldown, ()=> GSonCooldown = CooldownTimer(GSonCooldown));
      
        //


    }

    bool CooldownTimer(bool _moveOnCooldown)
    {
        _moveOnCooldown = false;
        print("CAN USE MOVE AGAIN");
        return _moveOnCooldown;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.transform.position - enemyRigidbody.transform.position);

            print("hit");
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(gameObject.transform.position, GSradius);
    }
}
