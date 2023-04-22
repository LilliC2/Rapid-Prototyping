using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class PlayerControllerP2 : GameBehaviour<PlayerControllerP2>
{
    [Header("Movement Setting")]
    public CharacterController characterController;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    

    [Header("Player Stats")]
    public float playerHealth = 100; //temp\
    public float playerHealthMax = 100; //temp\
    public float skillPoints = 0;
    public float expPoints = 0;
    public float expTilLvlUp = 10;
    public float playerLvl = 0;
    public Image playerHPBar;
    bool tookDamage;

    [Header("Bullet Stats")]
    public GameObject firingPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    bool bulletShot = false;
    public float timeBetweenShots;
    public float bulletDmg;
    public float bulletForce;

    [Header("UI")]
    public GameObject skillTree;
    float tweenTime = 2;

    public ParticleSystem gun;
    Camera camCam;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        skillTree.SetActive(false);
        camCam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Raycast();
        
        //Shoot
        if (Input.GetMouseButton(0))
        {
            FireProjectile();
            ParticleSystem dirtParticle = gun.GetComponentInChildren<ParticleSystem>(true);
            gun.Play(true);
        }

        //open skill tree
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(skillTree.active == false) skillTree.SetActive(true);
            else skillTree.SetActive(false);

        }


        //exit skill tree
        if (Input.GetKeyDown(KeyCode.Escape) && skillTree.activeSelf == true) skillTree.SetActive(false);

        EXPHandler();

        #region movement
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


        //inputs
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        characterController.Move(direction.normalized * speed * Time.deltaTime);

        velocity += Physics.gravity * Time.deltaTime;

        characterController.Move(velocity);


        if (gameObject.transform.position.y != 0.5) gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.5f, gameObject.transform.position.z);

        #endregion

    }

    void FireProjectile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if(!bulletShot)
            {
                Vector3 target = hit.point;
                target = new Vector3(target.x, 0.5f, target.z);

                
                //spawn bullet
                GameObject bullet = Instantiate(bulletPrefab, firingPoint.transform.position, firingPoint.transform.rotation);
                bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * bulletForce);
                Destroy(bullet, 2.5f);

                //shoot direction

                

                bulletShot = true;
                ExecuteAfterSeconds(timeBetweenShots, () => ResetBullet());

                //https://www.youtube.com/watch?v=Nke5JKPiQTw 6:22
            }
        }


    }

    void Raycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 target = hit.point;
            target = new Vector3(target.x, 0.5f, target.z);
            gameObject.transform.LookAt(target);
        }
            
    }

    void EXPHandler()
    {
        if(expPoints >= expTilLvlUp)
        {
            playerLvl++;
            expTilLvlUp += 2;
            expPoints = 0;
            skillPoints += 4;

        }
    }

    void Hit()
    {
            
        print(playerHealth);
        playerHPBar.fillAmount = playerHealth / playerHealthMax;
        playerHealth -= _EM2.enemyDmg;
        if(playerHealth <= 0)
        {
            _EM2.waveStatus = EnemyManagerP2.WaveStatus.Lost;
        }
        this.transform.GetComponent<Renderer>().material.DOColor(Color.red, 0.5f);
        this.transform.GetComponent<Renderer>().material.DOColor(Color.white,0.5f);
    }

    void ShakeCamera()
    {
        _UI.TweenScore();
        camCam.DOShakePosition(tweenTime / 2, 0.4f);
    }

    void ResetBullet()
    {
        bulletShot = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if((collision.gameObject.CompareTag("Enemy")))
        {

            Hit();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Enemy")))
        {
            if (!tookDamage)
            {
                Hit();
                ExecuteAfterSeconds(1, () => ResetHit());
                tookDamage = true;
                
            }
            
            
        }
    }

    void ResetHit()
    {
        tookDamage = false;
    }

}
