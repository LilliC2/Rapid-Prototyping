using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class P1UIManager : GameBehaviour<P1UIManager>
{
    public TMP_Text GScooldownText;
    public GameObject GScooldownTextGO;
    public Image GScooldownImage;

    public float remainingTime;
    bool isCoolDown;
    

    // Start is called before the first frame update
    void Start()
    {
        GScooldownImage.fillAmount = 0;
        GScooldownTextGO.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void UpdateCooldownTimer(float _cooldown, TMP_Text _cooldownText)
    {
        GScooldownTextGO.SetActive(true);

        remainingTime -=  1 * Time.deltaTime;

        GScooldownImage.fillAmount -= 1 / _cooldown * Time.deltaTime;
        if(GScooldownImage.fillAmount <= 0) GScooldownImage.fillAmount = 0;
        GScooldownText.text = remainingTime.ToString("F0");
        if (remainingTime <= 0) GScooldownTextGO.SetActive(false);
    }

    

}
