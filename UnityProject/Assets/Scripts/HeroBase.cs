using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBase : MonoBehaviour
{
   [SerializeField] private string Name;
   [SerializeField] private int MaxHP;
   private int actualHP;
   [SerializeField] private int Damage;

    [SerializeField] private HPBarLogic hpbar;

    public void Awake()
    {
        actualHP = MaxHP;
    }
    public void TakeDamage(int DamageAmount)
    {
        int newHPvalue = actualHP - DamageAmount;
        if (newHPvalue < 0)
        {
            hpbar.SetHealth(newHPvalue);
            actualHP = newHPvalue;
        }
        else
        {
            Dead();
        }
    }

    public void Heal(int HealAmount)
    {
        int newHPvalue = actualHP + HealAmount;
        if (newHPvalue >= MaxHP)
        {
            actualHP = MaxHP;
        }
        else
        {
            actualHP = newHPvalue;
        }
    }

    public void MoralityUp()
    {

    }

    public void MoralityDown()
    {

    }

    public void updateHPBAR()
    {

    }

    

    public void Dead()
    {
        Debug.Log("hero died");
    }
}
