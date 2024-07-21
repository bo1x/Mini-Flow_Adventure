using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsManager : MonoBehaviour
{
    [SerializeField] private GameObject fireballGO;
    [SerializeField] private int fireballDMG;
    [SerializeField] private float fireballRadius;
    private bool clickToFireball = false;


    [SerializeField] private GameObject healGO;
    [SerializeField] private int healAmount;
    [SerializeField] private float healRadius;
    [SerializeField] private int healTicks;
    [SerializeField] private float healtime;
    private bool clickforHeal = false;
    public void FireBall(GameObject go)
    {
        clickToFireball = true;
        StartCoroutine(HideCard(go, 1f));
 
    }
    public void Heal(GameObject go)
    {
        clickforHeal = true;
        StartCoroutine(HideCard(go, 6f));
    }

    public void PactWithDevil(GameObject go)
    {
        GameManager.instance.PACTwithTheDevil = true;

        StartCoroutine(HideCard(go,20f));
    }



    public void MakeCamp(GameObject go)
    {
        if(GameManager.instance._gamestate == GameManager.gameState.Fight) { return; }
        if(GameManager.instance._gamestate == GameManager.gameState.ReadingQuest) { return; }
        if(GameManager.instance._gamestate == GameManager.gameState.bossKilled) { return; }
        GameManager.instance.MakeCamp();
        StartCoroutine(HideCard(go, 20f));
    }

    public void SkipHeroesTurn(GameObject go)
    {
        GameManager.instance.SkipHeroesTurn = true;
        StartCoroutine(HideCard(go, 10f));

    }

    public void SkipEnemyTurn(GameObject go)
    {
        GameManager.instance.SkipEnemiesTurn = true;
        StartCoroutine(HideCard(go, 14f));
    }

    IEnumerator HideCard(GameObject go,float time)
    {
        go.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        go.SetActive(false);
        Debug.Log("Before yield return");
        yield return new WaitForSeconds(time);
        Debug.Log("beforewhile");

        while (!go.transform.parent.gameObject.activeSelf)
        {
            Debug.Log("while");

            yield return new WaitForSeconds(time);
        }
        Debug.Log("active");
        
        go.SetActive(true);
    }

    private void Update()
    {
        if (clickToFireball)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickToFireball = false;
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = 0;
                GameObject go =  Instantiate(fireballGO,mouseWorldPos,Quaternion.identity);
                go.transform.localScale = Vector3.one*fireballRadius;
                GameManager.instance.CheckIfInsideRadiusDamage(mouseWorldPos, fireballRadius, fireballDMG);
                Destroy(go, 0.40f);
            }
        }

        if (clickforHeal)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Heal");
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = 0;
                clickforHeal = false;
                StartCoroutine(spawnHealAreaTick(mouseWorldPos, healTicks));
            }
        }

    }

    IEnumerator spawnHealAreaTick(Vector3 pos,int ticks)
    {
        for (int i = 0; i < ticks; i++)
        {
            GameObject go = Instantiate(healGO, pos, Quaternion.identity);
            go.transform.localScale = Vector3.one * healRadius;
            GameManager.instance.CheckIfInsideRadiusHeal(pos, healRadius, healAmount);
            Destroy(go, healtime/4*3);

            yield return new WaitForSeconds(healtime);
        }

    }

    public void Hover(GameObject go)
    {
        go.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
    }

    public void UnHover(GameObject go)
    {
        go.GetComponent<Image>().color = new Color(1f,1f,1f);
    }
}
