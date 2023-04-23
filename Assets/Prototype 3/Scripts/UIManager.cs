using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace prototype3
{
    public class UIManager : GameBehaviour<UIManager>
    {
        public GameObject instructionPanel;
        public Image gripMeter;
        float timeRemaining;
        
        public TMP_Text flyCountText;
        public TMP_Text fliesLeftText;
        public GameObject fliesLeftPanel;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GripMeterCountdown(float _timeRemaining)
        {
            print("grip");
            _timeRemaining -= 1 *Time.deltaTime;
            gripMeter.fillAmount -= 1 / _timeRemaining * Time.deltaTime;
            if (gripMeter.fillAmount <= 0) gripMeter.fillAmount = 0;

        }

        public void ResetGripMeter()
        {
            gripMeter.fillAmount = 1;
        }

        public void CloseInstructions()
        {
            instructionPanel.SetActive(false);
        }

        public void UpdateFlyCount(int _count)
        {
            flyCountText.text = "Flies Caught: " + _count;
        }

        public void FliesLeft(int _count)
        {
            fliesLeftText.text = "Collect the remaining " + _count + " flies to exit!";
        }
    }
}
