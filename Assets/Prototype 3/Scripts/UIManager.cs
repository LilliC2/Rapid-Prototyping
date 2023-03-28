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
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GripMeterCountdown()
        {
            timeRemaining -= 1 *Time.deltaTime;
            gripMeter.fillAmount -= 1 / 3 * Time.deltaTime;
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
    }
}
