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

    public TMP_Text RcooldownText;
    public GameObject RcooldownTextGO;
    public Image RcooldownImage;

    public TMP_Text scoreText;
    public TMP_Text waveText;

    public float GSremainingTime;
    public float RremainingTime;
    bool isCoolDown;

    public AudioSource waveUpdateSound;
    

    // Start is called before the first frame update
    void Start()
    {
        GScooldownImage.fillAmount = 0;
        RcooldownImage.fillAmount = 0;
        GScooldownTextGO.SetActive(false);
        RcooldownTextGO.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void UpdateCooldownTimerGS(float _cooldown, TMP_Text _cooldownText)
    {
        GScooldownTextGO.SetActive(true);

        GSremainingTime -=  1 * Time.deltaTime;

        GScooldownImage.fillAmount -= 1 / _cooldown * Time.deltaTime;
        if(GScooldownImage.fillAmount <= 0) GScooldownImage.fillAmount = 0;
        GScooldownText.text = GSremainingTime.ToString("F0");
        if (GSremainingTime <= 0) GScooldownTextGO.SetActive(false);
    }
    public void UpdateCooldownTimerR(float _cooldown, TMP_Text _cooldownText)
    {
        RcooldownTextGO.SetActive(true);

        RremainingTime -=  1 * Time.deltaTime;

        RcooldownImage.fillAmount -= 1 / _cooldown * Time.deltaTime;
        if(RcooldownImage.fillAmount <= 0) RcooldownImage.fillAmount = 0;
        RcooldownText.text = RremainingTime.ToString("F0");
        if (RremainingTime <= 0) RcooldownTextGO.SetActive(false);
    }

    public void UpdateScore(int _score)
    {
        scoreText.text = "Score: " + _score;
    }

    public void UpdateWave(int _waveNum)
    {
        waveUpdateSound.Play();
        waveText.text = "Wave: " + _waveNum;
    }

}
