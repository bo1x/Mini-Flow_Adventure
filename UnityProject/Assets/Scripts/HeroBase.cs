using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBase : MonoBehaviour
{
   [SerializeField] public string Name;
   [SerializeField] public int MaxHP;
   private int actualHP;
   [SerializeField] public int Damage;

    [SerializeField] private HPBarLogic hpbar;
    public bool alreadyAttacked = false;
    [SerializeField] private Animator anim;
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
        updateHPBAR();
    }

    public void MoralityUp()
    {

    }

    public void MoralityDown()
    {

    }

    public void updateHPBAR()
    {
        Debug.Log(actualHP);
        hpbar.SetHealth(actualHP);
    }

    

    public void Dead()
    {
        Debug.Log("hero died");
    }

    public void PlayAttack()
    {
        anim.Play("Attack");
    }

    public void PlayWalk()
    {
        anim.Play("HeroWalk");
    }

    public void PlayIdle()
    {
        anim.Play("HeroIdle");
    }
}
