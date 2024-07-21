using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
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
        PlayAttack();
    }
    public void TakeDamage(int DamageAmount)
    {
        int newHPvalue = actualHP - DamageAmount;
        if (newHPvalue > 0)
        {
            hpbar.SetHealth(newHPvalue);
            actualHP = newHPvalue;
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
        }
        else
        {
            actualHP = newHPvalue;
        }
        updateHPBAR();
    }


    public void updateHPBAR()
    {
        Debug.Log(actualHP);
        hpbar.SetHealth(actualHP);
    }

    public void SetMaxHP(int _maxHP)
    {
        MaxHP = _maxHP;
        actualHP = MaxHP;
        hpbar.SetBarMaxValue(MaxHP);
        updateHPBAR();
    }

    public void SetDamage(int _damage)
    {
        Damage = _damage;
    }

    public void Dead()
    {
        Debug.Log("monster died");
        
    }

    public void PlayAttack()
    {
        anim.Play("EnemyAttack");
    }


    public void PlayIdle()
    {
        anim.Play("EnemyIdle");
    }
}
