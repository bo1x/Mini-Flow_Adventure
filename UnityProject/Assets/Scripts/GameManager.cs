using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HeroBase hero1;
    [SerializeField] private HeroBase hero2;
    [SerializeField] private HeroBase hero3;
    [SerializeField] private gameState _gamestate;
    [SerializeField] private combatState _combatstate;

    [SerializeField] private GameObject HeroMove;
    //27.5
    [SerializeField] private List<float> pointsInterest;
    [SerializeField] private List<GameObject> EnemyPrefabs;
    private float lastPointOfInterest = 0;

    private List<EnemyBase> enemiesAlive = new List<EnemyBase>();
    private List<HeroBase> heroesAlive = new List<HeroBase>();

    [SerializeField] private EnemyBase enemy1;
    [SerializeField] private EnemyBase enemy2;
    [SerializeField] private EnemyBase enemy3;


    private bool heroIsAttacking = false;
    private bool enemyIsAttacking = false;
    public enum gameState
    {
        Idle,
        Walking,
        Fight,
        ReadingQuest,
        bossFight
    }

    public enum combatState
    {
        Start,
        HerosTurn,
        EnemysTurn,
        Lost,
        Win
    }

    public void Awake()
    {
        heroesAlive.Add(hero1);
        heroesAlive.Add(hero2);
        heroesAlive.Add(hero3);

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
            
            Combat();
        }
    }
    public void Combat()
    {
        if (_combatstate == combatState.Lost)
        {
            Debug.Log("YOU LOST");
            return;
        }

        if (_combatstate == combatState.Win)
        {
            Debug.Log("YOU WON");
            foreach(var coso in enemiesAlive)
            {
                Debug.Log(gameObject.name);
                Destroy(coso.gameObject);
            }
            enemiesAlive.Clear();

            _combatstate = combatState.Start;
            _gamestate = gameState.Walking;
            return;
        }

        if (_combatstate == combatState.Start)
        {
            SetUpBattle();
            _combatstate = combatState.HerosTurn;
            return;
        }

        if (_combatstate == combatState.HerosTurn)
        {
            if (heroIsAttacking)
            {
                return;
            }
            if(hero1.alreadyAttacked == false)
            {
                StartCoroutine(AttackEnemy(hero1));
                return;
            }

            if (hero2.alreadyAttacked == false)
            {

                StartCoroutine(AttackEnemy(hero2));
                return;
            }

            if (hero3.alreadyAttacked == false)
            {

                StartCoroutine(AttackEnemy(hero3));
                return;
            }

            _combatstate = combatState.EnemysTurn;
            hero1.alreadyAttacked = false;
            hero2.alreadyAttacked = false;
            hero3.alreadyAttacked = false;

        }

        if (_combatstate == combatState.EnemysTurn)
        {
            if (enemyIsAttacking)
            {
                return;
            }
            if (enemy1.alreadyAttacked == false)
            {
                StartCoroutine(AttackHero(enemy1));
                return;
            }

            if (enemy2.alreadyAttacked == false)
            {

                StartCoroutine(AttackHero(enemy2));
                return;
            }

            if (enemy3.alreadyAttacked == false)
            {

                StartCoroutine(AttackHero(enemy3));
                return;
            }

            _combatstate = combatState.HerosTurn;
            enemy1.alreadyAttacked = false;
            enemy2.alreadyAttacked = false;
            enemy3.alreadyAttacked = false;
        }













    }

    public void SetUpBattle()
    {
        
        int random = Random.Range(0, EnemyPrefabs.Count);
        enemy1 = Instantiate(EnemyPrefabs[random]).GetComponent<EnemyBase>();
        enemy1.transform.position = new Vector3(hero1.transform.position.x+5, hero1.transform.position.y, 0);
        random = Random.Range(0, EnemyPrefabs.Count);
        enemy2 = Instantiate(EnemyPrefabs[random]).GetComponent<EnemyBase>();
        enemy2.transform.position = new Vector3(hero2.transform.position.x+5, hero2.transform.position.y, 0);
        random = Random.Range(0, EnemyPrefabs.Count);
        enemy3 = Instantiate(EnemyPrefabs[random]).GetComponent<EnemyBase>();
        enemy3.transform.position = new Vector3(hero3.transform.position.x + 5, hero3.transform.position.y, 0);

        enemiesAlive.Add(enemy1);
        enemiesAlive.Add(enemy2);
        enemiesAlive.Add(enemy3);

        _combatstate = combatState.HerosTurn;
    }


    IEnumerator AttackEnemy(HeroBase hb)
    {
        if (enemiesAlive.Count == 0)
        {
            _combatstate = combatState.Win;
        }
        heroIsAttacking = true;
        hb.alreadyAttacked = true;
        hb.PlayAttack();
        int random = Random.Range(0, enemiesAlive.Count);
        enemiesAlive[random].TakeDamage(hb.Damage);
        if (enemiesAlive[random].actualHP<=0)
        {
            Debug.Log(random);
            enemiesAlive[random].transform.position = new Vector3(0, 100, 0);
            enemiesAlive.RemoveAt(random);
            
        }
        
        yield return new WaitForSeconds(1f);
        heroIsAttacking = false;
        if (enemiesAlive.Count == 0)
        {
            _combatstate = combatState.Win;
        }
        
    }

    IEnumerator AttackHero(EnemyBase eb)
    {
        if(heroesAlive.Count == 0)
        {
          _combatstate = combatState.Lost;
        }
        enemyIsAttacking = true;
        eb.alreadyAttacked = true;
        eb.PlayAttack();
        int random = Random.Range(0, enemiesAlive.Count);
        heroesAlive[random].TakeDamage(eb.Damage);
        if (heroesAlive[random].actualHP <= 0)
        {
            heroesAlive.RemoveAt(random);
        }
        yield return new WaitForSeconds(1f);
        enemyIsAttacking = false;
        if (enemiesAlive.Count == 0)
        {
            _combatstate = combatState.Lost;
        }

    }

    public void CheckEnemiesAlive()
    {

    }
    public void NextTurn()
    {

    }

    public void ScrollEvent()
    {
        Debug.Log("event");
        PlayHeroAnim(hero1, "Idle");
        PlayHeroAnim(hero2, "Idle");
        PlayHeroAnim(hero3, "Idle");
        _gamestate = gameState.Fight;
        _combatstate = combatState.Start;
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
