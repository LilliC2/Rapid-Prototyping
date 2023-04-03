using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace prototype3
{
    public class PlayerController : GameBehaviour<PlayerController>
    {
        public GameObject hookshotObject;
        public LineRenderer hookshot;
        public GameObject firingPoint;

        public float movementSpeed;
        Vector3 destination;
        Vector3 destinationPoint;

        public bool gravityBool;
        float gravity = 9.8f;
        float distance;

        Rigidbody rb;
        bool calledGrip; 

        bool shot = false;

        // Start is called before the first frame update
        void Start()
        {

            hookshotObject.SetActive(false);
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            float horizontal = -Input.GetAxis("Horizontal");

            gameObject.transform.Rotate(0, 0, horizontal);
            hookshot.SetPosition(0, firingPoint.transform.position);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Physics.Raycast(firingPoint.transform.position, firingPoint.transform.forward, out RaycastHit raycastHit))
                {
                    if(raycastHit.collider.CompareTag("Walls"))
                    {
                        shot = true;
                        hookshotObject.SetActive(true);
                        destination = raycastHit.transform.position;
                        destinationPoint = raycastHit.point;
                        hookshot.SetPosition(1, raycastHit.point);

                        ExecuteAfterSeconds(movementSpeed, () => MovePlayer());
                        
                    }
                }
            }


            //MovePlayer();

            if(shot)
            {
                distance = (Vector3.Distance(destination, gameObject.transform.position));
               
                if (distance < 3f)
                {
                    print("at destinaation");
                    StartCoroutine(Grip(destination));
                }
            }


            if (rb.useGravity == false)
            {
                print("grav off");

            }
            else print("grave on");
        }

        void MovePlayer()
        {
            print("Move!");
            //rb.MovePosition(destination * 3 * Time.deltaTime);
            rb.AddForce(destination * 50);
        }

        IEnumerator Grip(Vector3 _lastDestination)
        {

            hookshotObject.SetActive(false);
            float count = 3;

            count  -= 1 * Time.deltaTime;
            _UI2.GripMeterCountdown(count);
            calledGrip = true;

            print("grip");

            yield return new WaitForSeconds(3);

            if(destination == _lastDestination)
            {
                shot = false;
                rb.useGravity = true;
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.collider.CompareTag("Ground"))
            {
                print("on ground");
                rb.useGravity = false;
                calledGrip = false;
            }
        }

    }

    
}