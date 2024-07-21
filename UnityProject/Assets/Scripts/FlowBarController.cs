using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowBarController : MonoBehaviour
{
    public Slider slider;

    public int Bored = 0;
    public int RageQuit = 0;
    public bool firstTime = true;
    public bool isLastCounterLow = false;

    private void Awake()
    {
        slider.maxValue = 100;
    }
    public void SetFlow(int Value)
    {
        if (Value<30)
        {
            if (firstTime)
            {
                Bored += 1;

            }
            else
            {
                if (isLastCounterLow)
                {
                    Debug.Log("Didnt reach hight");
                }
                else
                {
                    isLastCounterLow = true;
                    Bored += 1;
                }
            }
            
        }

        if (Value > 70)
        {
            if (firstTime)
            {
                RageQuit += 1;

            }
            else
            {
                if (!isLastCounterLow)
                {
                    Debug.Log("Didnt reach low");
                }
                else
                {
                    isLastCounterLow = false;
                    RageQuit += 1;
                }
            }
        }
        slider.value = Value;
    }
}
