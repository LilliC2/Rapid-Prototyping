using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;

    // Update is called once per frame
    void Update()
    {
        
        if(Vector3.Distance(gameObject.transform.position, pointA.transform.position) <0.2f)
        {
            gameObject.transform.DOMove(pointB.transform.position, 2f);
        }
        else if(Vector3.Distance(gameObject.transform.position, pointB.transform.position) < 0.2f)
        {
            gameObject.transform.DOMove(pointA.transform.position, 2f);
        }

    }
}
