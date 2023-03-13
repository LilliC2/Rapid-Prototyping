using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerP2 : GameBehaviour
{
    [Header("Movement Setting")]
    public CharacterController characterController;
    public Transform cam;
    public float speed = 6f;

    //directinal
    public float turnSmoothTime = 0.1f;

    //jumping
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;

    public GameObject firingPoint;
    public GameObject bulletPrefab;

    [Header("Player Stats")]

    public float playerHealth = 100; //temp
    
    //[Header("UI")]

    public GameObject testObject;

    [Header("Projectile")]
    public float bulletSpeed;
    bool bulletShot = false;
    public float timeBetweenShots;


    private void Start()
    {
        



    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            print("mouse 0");
            FireProjectile();
        }


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

        if (direction.magnitude >= 0.1f)
        {
            
        }


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
                testObject.transform.position = target;


                //spawn bullet
                Transform bulletTransform = Instantiate(bulletPrefab, firingPoint.transform.position, Quaternion.identity);

                bulletShot = true;

                //https://www.youtube.com/watch?v=Nke5JKPiQTw 6:22
            }
        }

        if (bulletShot) ExecuteAfterSeconds(timeBetweenShots, () => ResetBullet());

    }

    void ResetBullet()
    {
        bulletShot = false;
    }

}
