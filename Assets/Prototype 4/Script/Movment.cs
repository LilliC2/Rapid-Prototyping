using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.Controls;
using TMPro;

public class Movment : GameBehaviour<Movment>
{

    public float moveSpeed = 5;
    public float steerSpeed = 180;
    public int gap = 10;

    public float antSpeed = 5;
    public GameObject antPrefab;
    public GameObject spawnAnt;

    private List<GameObject> AntLine = new List<GameObject>();
    //positions queen ant has travelled
    private List<Vector3> PositionHistory = new List<Vector3>();
    bool antTunnel = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(_GM4.gameStates == prototype4.GameManager.GameStates.Solving)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            //steering
            float steerDirection = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * steerDirection * steerSpeed * Time.deltaTime);

            //store pos
            PositionHistory.Insert(0, spawnAnt.transform.position);

            //move ant line
            int index = 0;
            foreach (var ant in AntLine)
            {
                Vector3 point = PositionHistory[Mathf.Min(index * gap, PositionHistory.Count - 1)];

                Vector3 moveDirection = point - ant.transform.position;
                ant.transform.position += moveDirection * antSpeed * Time.deltaTime;
                ant.transform.LookAt(point);
                index++;
            }

            if (Input.GetKeyDown(KeyCode.Space)) GrowColony();
        }

        
    }

    public void GrowColony()
    {
        GameObject ant = Instantiate(antPrefab);
        AntLine.Add(ant);
    }

    private void AntTunnel(string _wallName)
    {
        antTunnel = true;
        switch(_wallName)
        {
            case "TopWall":
                transform.position = new Vector3(transform.position.x, transform.position.y, -27);
                break;
            case "BottomWall":
                transform.position = new Vector3(transform.position.x, transform.position.y, 27);
                break;
            case "LeftWall":
                transform.position = new Vector3(27f, transform.position.y, transform.position.z);
                break;
            case "RightWall":
                transform.position = new Vector3(-27f, transform.position.y, transform.position.z);
                break;



        }
        ExecuteAfterSeconds(2f, () => ResetTunnel());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Walls"))
        {
            if(!antTunnel)
            {
                ExecuteAfterSeconds(0.5f, () => AntTunnel(other.name));
            }
            
            
        }
        if (other.CompareTag("Food"))
        {
            TMP_Text tempTxt = other.GetComponentInChildren<TMP_Text>();
            if (tempTxt.text == _EG.correctAnswer.ToString())
            {
                _GM4.solved = true;
            }

        }

    }

    void ResetTunnel()
    {
        antTunnel=false;
    }
}
