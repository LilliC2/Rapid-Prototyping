using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float skillPoints = 0;
    public float expPoints = 0;
    public float expTilLvlUp = 10;
    public float playerLvl = 0;
    

    [Header("Bullet Stats")]
    public GameObject firingPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    bool bulletShot = false;
    public float timeBetweenShots;
    public float bulletDmg;

    [Header("UI")]
    public GameObject skillTree;
    float tweenTime = 2;


    private void Start()
    {

        skillTree.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Shoot
        if (Input.GetMouseButton(0))
        {
            print("mouse 0");
            FireProjectile();
        }

        //open skill tree
        if (Input.GetKeyDown(KeyCode.E))
        {
            print("open tree");
            skillTree.SetActive(true);
        }


        //exit skill tree
        if (Input.GetKeyDown(KeyCode.Escape) && skillTree.activeSelf == true) skillTree.SetActive(false);

        EXPHandler();

        #region movement
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        //inputs
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        characterController.Move(direction.normalized * speed * Time.deltaTime);

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
                GameObject bullet = Instantiate(bulletPrefab, firingPoint.transform.position, Quaternion.identity);
                Transform bulletTransform = bullet.transform;

                //shoot direction
                Vector3 shootDir = target - firingPoint.transform.position.normalized;

                bulletTransform.GetComponent<Bullet>().Setup(shootDir,bulletSpeed);

                bulletShot = true;
                ExecuteAfterSeconds(timeBetweenShots, () => ResetBullet());

                //https://www.youtube.com/watch?v=Nke5JKPiQTw 6:22
            }
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
        playerHealth -= _EM2.enemyDmg;
        this.transform.GetComponent<Renderer>().material.DOColor(Color.red, 0.5f);
        this.transform.GetComponent<Renderer>().material.DOColor(Color.white,0.5f);
    }

    void ShakeCamera()
    {
        _UI.TweenScore();
        Camera.main.DOShakePosition(tweenTime / 2, 0.4f);
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

}
