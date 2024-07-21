using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EventScriptableObject", order = 1)]
public class EventScriptable : ScriptableObject
{
    public string Description;
    public string option1;
    public option _option1;
    public string option2;
    public option _option2;



    public enum option
    {
        Combat,
        Boring
    }
}


