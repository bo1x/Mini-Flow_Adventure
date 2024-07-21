using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBase : MonoBehaviour
{
   [SerializeField] public string Name;
   [SerializeField] public int MaxHP;
   public int actualHP;
   [SerializeField] public int Damage;

    [SerializeField] private HPBarLogic hpbar;
    public bool alreadyAttacked = false;
    [SerializeField] private Animator anim;
    public void Awake()
    {
        actualHP = MaxHP;
        hpbar.SetBarMaxValue(MaxHP);
    }
    public void TakeDamage(int DamageAmount)
    {
        int newHPvalue = actualHP - DamageAmount;
        if (newHPvalue > 0)
        {
            hpbar.SetHealth(newHPvalue);
            actualHP = newHPvalue;
            NPCsounds.instance.PlayerLoseHP();
        }
        else
        {
            actualHP = 0;
            Dead();
        }
    }

    public void Heal(int HealAmount)
    {
        int newHPvalue = actualHP + HealAmount;
        if (newHPvalue >= MaxHP)
        {
            actualHP = MaxHP;
            NPCsounds.instance.PlayHealSound();
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
        hpbar.SetHealth(actualHP);
    }

    

    public void Dead()
    {
        NPCsounds.instance.DeadSound();
        Debug.Log("hero died");
    }

    public void PlayAttack()
    {
        anim.Play("Attack");
        NPCsounds.instance.PlayerAttack();
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
