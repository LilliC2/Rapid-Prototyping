using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; //tweening library

public class Playtime : GameBehaviour
{
    public float tweenTime = 2;
    public float moveDistance = 5;
    public GameObject player; //temp\
    public Ease moveEase;

    public enum Direction { Up,Down,Left,Right}

    // Start is called before the first frame update
    void Start()
    {

        //ExecuteAfterSeconds(2, ()=> ScaleToZero(player));

        ////How to do multiple things after x 
        //ExecuteAfterFrames(2, () =>
        //{
        //    ChangePlayerColour();
        //    print("wow");
        //});
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) MovePlayer(Direction.Right);
        if (Input.GetKeyDown(KeyCode.A)) MovePlayer(Direction.Left);
        if (Input.GetKeyDown(KeyCode.W)) MovePlayer(Direction.Up);
        if (Input.GetKeyDown(KeyCode.S)) MovePlayer(Direction.Down);
        if (Input.GetKeyDown(KeyCode.R)) ScalePlayer(Vector3.one);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangePlayerColour();
        }
    }

    void MovePlayer(Direction _direction)
    {
        ScalePlayer(player.transform.localScale*2);

        switch(_direction)
        {
            case Direction.Up:
                player.transform.DOMoveZ(player.transform.position.z + moveDistance, tweenTime).SetEase(moveEase).OnComplete(()=> ShakeCamera());
                break;
            case Direction.Down:
                player.transform.DOMoveZ(player.transform.position.z - moveDistance, tweenTime).SetEase(moveEase).OnComplete(() => ShakeCamera()); ;
                break;
            case Direction.Left:
                player.transform.DOMoveX(player.transform.position.x - moveDistance, tweenTime).SetEase(moveEase).OnComplete(() => ShakeCamera()); ;
                break;
            case Direction.Right:
                player.transform.DOMoveX(player.transform.position.x + moveDistance, tweenTime).SetEase(moveEase).OnComplete(() => ShakeCamera()); ;
                break;
        }
        

        
    }

    void ScalePlayer(Vector3 _scaleTo)
    {
        player.transform.DOScale(_scaleTo, tweenTime).SetEase(moveEase);
    }

    void ChangePlayerColour()
    {
        //player.GetComponent<Renderer>().material.color = GetRandomColour();
        player.GetComponent<Renderer>().material.DOColor(GetRandomColour(),0.5f); //smooth colour transition
    }

    void ShakeCamera()
    {
        _UI.TweenScore();
        Camera.main.DOShakePosition(tweenTime /2, 0.4f);
    }
}
