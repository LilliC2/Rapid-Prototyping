using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    public CanvasGroup cvs;
    // Start is called before the first frame update
    void Start()
    {
        cvs.alpha = 1;
        FadeCanvas(0);
    }

    // Update is called once per frame
    void FadeCanvas(float _fadeTo)
    {
        cvs.DOFade(_fadeTo, 2);
    }
}
