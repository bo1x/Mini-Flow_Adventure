using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HeroBase hero1;
    [SerializeField] private HeroBase hero2;
    [SerializeField] private HeroBase hero3;
    [SerializeField] public gameState _gamestate;
    [SerializeField] private combatState _combatstate;

    [SerializeField] private GameObject HeroMove;
    //27.5
    [SerializeField] private List<float> pointsInterest;
    [SerializeField] private List<GameObject> EnemyPrefabs;
    [SerializeField] private List<GameObject> EnemyBoss;
    private bool bossspawned = false;
    private bool bossdead = false;
    private float lastPointOfInterest = 0;

    private List<EnemyBase> enemiesAlive = new List<EnemyBase>();
    private List<HeroBase> heroesAlive = new List<HeroBase>();

    [SerializeField] private EnemyBase enemy1;
    [SerializeField] private EnemyBase enemy2;
    [SerializeField] private EnemyBase enemy3;

    [SerializeField] private int flowBar;
    [SerializeField] private FlowBarController fbc;

    private int dificulty = 0;
    


    private bool heroIsAttacking = false;
    private bool enemyIsAttacking = false;

    //Skills Variables

    public bool SkipHeroesTurn = false;
    public bool SkipEnemiesTurn = false;
    public bool PACTwithTheDevil = false;

    public static GameManager instance;


    public enum gameState
    {
        Idle,
        Walking,
        Fight,
        ReadingQuest,
        bossKilled
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
        instance = this;
        heroesAlive.Add(hero1);
        heroesAlive.Add(hero2);
        heroesAlive.Add(hero3);
        flowBar = 5;

    }
    public void Start()
    {
        
        
        UpdateAllHPbars();

        

    }
    public void Update()
    {

        if(_gamestate == gameState.bossKilled)
        {
            Debug.Log("WE WIN");
        }

        if(_gamestate == gameState.Walking)
        {
            foreach (var hero in heroesAlive)
            {
                PlayHeroAnim(hero, "Walk");
            }
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
            foreach (var hero in heroesAlive)
            {
                PlayHeroAnim(hero, "Idle");
            }
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
            enemiesAlive.Clear();
            _combatstate = combatState.Start;
            PACTwithTheDevil = false;
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
            foreach (var hero in heroesAlive)
            {
                if (hero.alreadyAttacked == false)
                {
                    StartCoroutine(AttackEnemy(hero));
                    return;
                }
            }

            if (SkipEnemiesTurn)
            {
                _combatstate = combatState.HerosTurn;
                SkipEnemiesTurn = false;
            }
            else
            {
                _combatstate = combatState.EnemysTurn;
            }
            foreach (var hero in heroesAlive)
            {
                hero.alreadyAttacked = false;
            }

        }

        if (_combatstate == combatState.EnemysTurn)
        {
            if (enemyIsAttacking)
            {
                return;
            }
            foreach (var enemy in enemiesAlive)
            {
                if (enemy.alreadyAttacked == false)
                {
                    StartCoroutine(AttackHero(enemy));
                    return;
                }
            }

            if (SkipHeroesTurn)
            {
                _combatstate = combatState.EnemysTurn;
                SkipHeroesTurn = false;
            }
            else
            {
                _combatstate = combatState.HerosTurn;
            }
            foreach (var enemy in enemiesAlive)
            {
                enemy.alreadyAttacked = false;
            }
        }













    }

    public void SetUpBattle()
    {
        dificulty += 1;

        if (dificulty != 4)
        {
            foreach (var hero in heroesAlive)
            {
                int random = Random.Range(0, EnemyPrefabs.Count);
                EnemyBase eb = Instantiate(EnemyPrefabs[random]).GetComponent<EnemyBase>();
                eb.transform.position = new Vector3(hero.transform.position.x + 5, hero.transform.position.y, 0);
                enemiesAlive.Add(eb);

            }

            foreach (var enemy in enemiesAlive)
            {
                enemy.SetMaxHP(5 * dificulty);
                enemy.SetDamage(1 * dificulty);
            }

            _combatstate = combatState.HerosTurn;
        }
        else
        {
            bossspawned = true;
            enemiesAlive.Add(Instantiate(EnemyBoss[0]).GetComponent<EnemyBase>());
            enemiesAlive[0].transform.position = new Vector3(hero2.transform.position.x + 5, 0, 0);
            enemiesAlive[0].SetMaxHP(100);
            enemiesAlive[0].SetDamage(10);
            _combatstate = combatState.HerosTurn;
        }
        
    }


    IEnumerator AttackEnemy(HeroBase hb)
    {
        if (enemiesAlive.Count == 0)
        {
            _combatstate = combatState.Win;

            if (bossspawned)
            {
                _gamestate = gameState.bossKilled;
            }
        }
        heroIsAttacking = true;
        hb.alreadyAttacked = true;
        hb.PlayAttack();
        int random = Random.Range(0, enemiesAlive.Count);
        if (PACTwithTheDevil)
        {
            enemiesAlive[random].TakeDamage(hb.Damage*3);

        }
        else
        {
            enemiesAlive[random].TakeDamage(hb.Damage);
        }

        if (enemiesAlive[random].actualHP<=0)
        {
            flowBar -= 2;
            UpdateFlowBar();
            Destroy(enemiesAlive[random].gameObject);
            enemiesAlive.RemoveAt(random);  
        }
        
        yield return new WaitForSeconds(1f);
        heroIsAttacking = false;
        if (enemiesAlive.Count == 0)
        {
            _combatstate = combatState.Win;

            if (bossspawned)
            {
                _gamestate = gameState.bossKilled;
            }
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

        if (PACTwithTheDevil)
        {
            heroesAlive[random].TakeDamage(eb.Damage * 2);

        }
        else
        {
            heroesAlive[random].TakeDamage(eb.Damage);
        }

        if (heroesAlive[random].actualHP <= 0)
        {
            flowBar += 5;
            UpdateFlowBar();
            Destroy(heroesAlive[random].gameObject);
            heroesAlive.RemoveAt(random);
        }
        else
        {
            flowBar += 1;
            UpdateFlowBar();
        }
        yield return new WaitForSeconds(1f);
        enemyIsAttacking = false;
        if (heroesAlive.Count == 0)
        {
            PACTwithTheDevil = false;
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
        foreach (var hero in heroesAlive)
        {
            hero.PlayIdle();
        }
        _gamestate = gameState.Fight;
        _combatstate = combatState.Start;
    }

    

    

    public void Meteor()
    {

    }

    public void UpdateAllHPbars()
    {
        foreach (var hero in heroesAlive)
        {
            hero.updateHPBAR();
        }
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

    public void UpdateFlowBar()
    {
        if (flowBar > 10)
        {
            Debug.Log("GAME IS TOO DIFFICULT");
            flowBar = 10;
        }
        if (flowBar<0)
        {
            Debug.Log("GAME IS TOO EASY");
            flowBar = 0;
        }
        fbc.SetFlow(flowBar);
    }

    public void MakeCamp()
    {
        _gamestate = gameState.Idle;
        StartCoroutine(HealWithDelay(heroesAlive));
    }

    IEnumerator HealWithDelay(List<HeroBase> listHero)
    {
        foreach (var hero in listHero)
        {
            yield return new WaitForSeconds(1f);
            hero.Heal(hero.MaxHP);
        }
        _gamestate = gameState.Walking;

    }

    public void CheckIfInsideRadiusDamage(Vector3 pos,float radius, int dmg)
    {
        List<HeroBase> deadhero = new List<HeroBase>();
        List<EnemyBase> deadEnemy = new List<EnemyBase>();
        foreach (var hero in heroesAlive)
        {
            if(Vector3.Distance(hero.gameObject.transform.position, pos) < radius)
            {
                
                hero.TakeDamage(dmg);
                if (hero.actualHP <= 0)
                {
                    flowBar += 5;
                    UpdateFlowBar();
                    deadhero.Add(hero);
                    
                }
                else
                {
                    flowBar += 1;
                    UpdateFlowBar();
                }
            }
        }

        foreach (var hero in deadhero)
        {
            Destroy(hero.gameObject);
            heroesAlive.Remove(hero);
        }
        

        foreach (var enemy in enemiesAlive)
        {
            if (Vector3.Distance(enemy.gameObject.transform.position, pos) < radius)
            {
                enemy.TakeDamage(dmg);
                if (enemy.actualHP <= 0)
                {
                    flowBar += 5;
                    UpdateFlowBar();
                    deadEnemy.Add(enemy);
                }
                else
                {
                    flowBar += 1;
                    UpdateFlowBar();
                }
            }
        }

        foreach (var enemy in deadEnemy)
        {
            Destroy(enemy.gameObject);
            enemiesAlive.Remove(enemy);
        }

    }


    public void CheckIfInsideRadiusHeal(Vector3 pos, float radius, int heal)
    {

        foreach (var hero in heroesAlive)
        {
            if (Vector3.Distance(hero.gameObject.transform.position, pos) < radius)
            {

                hero.Heal(heal);
            }
        }


        foreach (var enemy in enemiesAlive)
        {
            if (Vector3.Distance(enemy.gameObject.transform.position, pos) < radius)
            {
                enemy.Heal(heal);
            }
        }

    }

}
