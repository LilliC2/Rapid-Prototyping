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
        bool move = false;

        public int flyCount = 0;
        public int fliesInScene;

        public GameObject victoryPanel;
        int currentFlyCount;

        // Start is called before the first frame update
        void Start()
        {

            hookshotObject.SetActive(false);
            rb = GetComponent<Rigidbody>();
            _UI3.UpdateFlyCount(flyCount);
            var flies = GameObject.FindGameObjectsWithTag("Fly");
            fliesInScene = flies.Length;
            currentFlyCount = 0;
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
                    if (raycastHit.collider.CompareTag("Walls") )
                    {
                        
                        destination = new Vector3(raycastHit.point.x,raycastHit.point.y,5);

                        if(Vector3.Distance(destination,gameObject.transform.position) < 30)
                        {
                            shot = true;
                            hookshotObject.SetActive(true);
                            destinationPoint = raycastHit.point;
                            hookshot.SetPosition(1, raycastHit.point);

                            move = true;
                        }
                        

                    }
                }


            }

            print("grav status: " +rb.useGravity);

            //MovePlayer();

            if (shot)
            {
                distance = (Vector3.Distance(destination, gameObject.transform.position));

                if (distance < 1f)
                {
                    print("at destinaation");
                    StartCoroutine(Grip(destination));

                }
            }

        }

        private void FixedUpdate()
        {
            if (move) MovePlayer();
        }

        void MovePlayer()
        {
            print("turning off grav");
            rb.useGravity = false;
            var step = movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,destination, step);
        }

        IEnumerator Grip(Vector3 _lastDestination)
        {

                hookshotObject.SetActive(false);
                float count = 3;

                count -= 1 * Time.deltaTime;
                _UI3.GripMeterCountdown(count);
                calledGrip = true;

                print("Holding!");

                yield return new WaitForSeconds(3);

                if (destination == _lastDestination)
                {
                    print("Turning on Grav");

                    shot = false;
                    rb.useGravity = true;
                }

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Walls"))
            {
                move = false;
                //StartCoroutine(Grip(destination));
                print("turning off grav");
                rb.useGravity = false;

            }


            
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Victory"))
            {
                //if they have all the flies
                if(flyCount== fliesInScene)
                {
                    victoryPanel.SetActive(true);
                }
                else
                {
                    _UI3.FliesLeft(fliesInScene - flyCount);
                    _UI3.fliesLeftPanel.SetActive(true);
                }


            }
            if (other.CompareTag("Fly"))
            {
                print("COLLIDE WITH FLYU");
                //make sure its only once
                
                flyCount++;
                if(currentFlyCount + 1 != flyCount)
                {
                    flyCount = currentFlyCount + 1;
                    currentFlyCount = flyCount;
                }
                _UI3.UpdateFlyCount(flyCount);
                Destroy(other.gameObject);
            }

            if (other.CompareTag("Enemy"))
            {
                print("BIRD");
                victoryPanel.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Victory"))
            {
                _UI3.fliesLeftPanel.SetActive(false);
            }
        }

    }
}