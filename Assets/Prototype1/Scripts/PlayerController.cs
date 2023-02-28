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
    public GameObject mouseTarget;

    [Header("Grand Slam")]
    public float GSheight;
    public float GSradius;
    public float GSspeed;
    public float GScooldown;
    public Ease GSease;
    public Ease GSeaseSlam;
    bool GSonCooldown;

    public float GSexplosionForce;
    public float GSexplosionUpdwards;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        mouseTarget.SetActive(false);
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
                mouseTarget.SetActive(true);
                mouseTarget.transform.position = targetPos;
            }
            
        }

        //turn off target pos indicator
        if(Vector3.Distance(gameObject.transform.position,targetPos) < 0.5f) mouseTarget.SetActive(false);

        //Grand Slam
        if (Input.GetKeyDown(KeyCode.Alpha1) && !GSonCooldown)
        {
            GrandSlam();
            //move cool down
            
        }

        if (GSonCooldown) _P1UI.UpdateCooldownTimer(GScooldown, _P1UI.GScooldownText);

        

    }

    void GrandSlam()
    {
        moveActive = true; //player cannot move while slamming
        
        //lift player in air
        gameObject.transform.DOMoveY(GSheight, GSspeed).SetEase(GSease);

        //. slam player down
        gameObject.transform.DOMoveY(0, 0.1f).SetEase(GSeaseSlam);
        // any enemies within that radius are knocked back
        StartCoroutine(GSKnockBack());

        moveActive = false;

        // add cool down
        GSonCooldown = true;
        _P1UI.GScooldownImage.fillAmount = 1;
        _P1UI.remainingTime = GScooldown;
        // when cool down over reset move
        ExecuteAfterSeconds(GScooldown, ()=> GSonCooldown = CooldownTimer(GSonCooldown));

      
    }

    IEnumerator GSKnockBack()
    {
        yield return new WaitForSeconds(1.5f);
        
        Collider[] GSenemiesHit = Physics.OverlapSphere(gameObject.transform.position, GSradius);
        for(int i =0; i < GSenemiesHit.Length; i++)
        {
            if (GSenemiesHit[i].CompareTag("Enemy"))
            {
                print(GSenemiesHit[i].name + " has been hit");
                Vector3 dir = (gameObject.transform.position + GSenemiesHit[i].gameObject.transform.position);
                Rigidbody enemyRB = GSenemiesHit [i].GetComponent<Rigidbody>();
                enemyRB.AddExplosionForce(GSexplosionForce, gameObject.transform.position, GSradius, GSexplosionUpdwards);
                //GSenemiesHit[i].gameObject.transform.DOMove(dir, GSknockbackSpeed).SetEase(GSeaseKnockback);
            }
        }


    }

    void Rollout()
    {
        //Roll out
        //player gains momentum
        // player rolls forward
        // anything in the path is knocked away
        // add cool down
        
    }


    bool CooldownTimer(bool _moveOnCooldown)
    {
        _moveOnCooldown = false;
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
