using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarLogic : MonoBehaviour
{
    public Slider slider;
    public void SetHealth(int Value)
    {
        slider.value = Value;
    }
}
