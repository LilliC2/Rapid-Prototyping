using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerP2 : MonoBehaviour
{

    public enum Direction { Up, Down, Left, Right }

    public float moveDistance = 3;
    public float tweenTime = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.D)) MovePlayer(Direction.Right);
        if (Input.GetKeyDown(KeyCode.A)) MovePlayer(Direction.Left);
        if (Input.GetKeyDown(KeyCode.W)) MovePlayer(Direction.Up);
        if (Input.GetKeyDown(KeyCode.S)) MovePlayer(Direction.Down);


    }

    void MovePlayer(Direction _direction)
    {

        switch (_direction)
        {
            case Direction.Up:
                this.transform.DOMoveZ(this.transform.position.z + moveDistance, tweenTime).SetEase(moveEase));
                break;
            case Direction.Down:
                this.transform.DOMoveZ(this.transform.position.z - moveDistance, tweenTime).SetEase(moveEase));
                break;
            case Direction.Left:
                this.transform.DOMoveX(this.transform.position.x - moveDistance, tweenTime).SetEase(moveEase));
                break;
            case Direction.Right:
                this.transform.DOMoveX(this.transform.position.x + moveDistance, tweenTime).SetEase(moveEase));
                break;
        }



    }

}
