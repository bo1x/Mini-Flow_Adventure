using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsounds : MonoBehaviour
{
    public static NPCsounds instance;

    [SerializeField] private AudioSource Dead;
    [SerializeField] private AudioSource _MobAttack;
    [SerializeField] private AudioSource _OkButton;
    [SerializeField] private AudioSource _Playerlosehp;
    [SerializeField] private AudioSource _MobLoseHP;
    [SerializeField] private AudioSource _PlayerAttack;
    [SerializeField] private AudioSource _FlowBarIncrease;
    [SerializeField] private AudioSource _FlowBarDecrease;

    [SerializeField] private AudioSource _HealingSound;

    public void Awake()
    {
        instance = this;
    }

    public void DeadSound()
    {
        Dead.Play();
    }

    public void MobAttack()
    {
        _MobAttack.Play();
    }

    public void OKbutton()
    {
        _OkButton.Play();
    }

    public void PlayerLoseHP()
    {
        _Playerlosehp.Play();
    }

    public void MOBLOSEHP()
    {
        _MobLoseHP.Play();
    }

    public void PlayerAttack()
    {
        _PlayerAttack.Play();
    }

    public void FlowBarIncrease()
    {
        _FlowBarIncrease.Play();
    }

    public void FlowBarDecrease()
    {
        _FlowBarDecrease.Play();
    }


    public void PlayHealSound()
    {
        _HealingSound.Play();
    }

}
