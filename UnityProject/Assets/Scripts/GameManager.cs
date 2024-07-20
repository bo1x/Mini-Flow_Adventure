using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] HeroBase hero1;
    [SerializeField] HeroBase hero2;
    [SerializeField] HeroBase hero3;
    [SerializeField] private gameState _gamestate;

    [SerializeField] private GameObject HeroMove;
    //27.5
    [SerializeField] private List<float> pointsInterest;
    private float lastPointOfInterest = 0;
    public enum gameState
    {
        Idle,
        Walking,
        Fight,
        ReadingQuest,
        bossFight
    }

    public void Awake()
    {
        
    }
    public void Start()
    {
        ChangeGameState(gameState.Idle);
        
        UpdateAllHPbars();
        
    }
    public void Update()
    {
        if(_gamestate == gameState.Walking)
        {
            PlayHeroAnim(hero1,"Walk");
            PlayHeroAnim(hero2, "Walk");
            PlayHeroAnim(hero3, "Walk");
            HeroMove.transform.position = new Vector3(HeroMove.transform.position.x + 10 * Time.deltaTime, 0, 0);
            foreach (var item in pointsInterest)
            {
                if (lastPointOfInterest < pointsInterest.IndexOf(item))
                {
                    if (HeroMove.transform.position.x > item)
                    {
                        lastPointOfInterest = pointsInterest.IndexOf(item);
                        ChangeGameState(gameState.ReadingQuest);
                        ScrollEvent();
                    }
                }
            }
        }

        if (_gamestate == gameState.Idle)
        {
            PlayHeroAnim(hero1, "Idle");
            PlayHeroAnim(hero2, "Idle");
            PlayHeroAnim(hero3, "Idle");
        }

        if(_gamestate == gameState.Fight)
        {

        }
    }
    public void Combat()
    {
        













    }

    public void ScrollEvent()
    {
        Debug.Log("event");
        PlayHeroAnim(hero1, "Idle");
        PlayHeroAnim(hero2, "Idle");
        PlayHeroAnim(hero3, "Idle");
    }

    public void CreateEnemies()
    {

    }

    public void HealParty()
    {

    }

    public void Meteor()
    {

    }

    public void UpdateAllHPbars()
    {
        hero1.updateHPBAR();
        hero2.updateHPBAR();
        hero3.updateHPBAR();
    }

    public void ChangeGameState(gameState gs)
    {
        _gamestate = gs;
    }

    public void PlayHeroAnim(HeroBase hb, string Anim)
    {
        switch (Anim)
        {
            case "Attack":
                hb.PlayAttack();
                break;
            case "Idle":
                hb.PlayIdle();
                break;
            case "Walk":
                hb.PlayWalk();
                break;
            default:
                break;
        }
    }
}
