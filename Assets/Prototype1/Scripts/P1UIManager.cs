using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class P1UIManager : GameBehaviour<P1UIManager>
{
    float GScooldownTimer = 6;
    public TMP_Text GScooldownText;
    public Image GScooldownImage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCooldownTimer(float _cooldown, TMP_Text _cooldownText)
    {
        _cooldown -= Time.deltaTime;
        print(_cooldown);
        _cooldownText.text = _cooldown.ToString("F0");
    }

}
