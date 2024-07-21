using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    [SerializeField] private GameObject fireballGO;
    [SerializeField] private int fireballDMG;
    [SerializeField] private float fireballRadius;
    private bool clickToFireball = false;
    public void FireBall(GameObject go)
    {
        clickToFireball = true;
        StartCoroutine(HideCard(go, 1f));

        
        
    }

    public void PactWithDevil(GameObject go)
    {
        GameManager.instance.PACTwithTheDevil = true;

        StartCoroutine(HideCard(go,20f));
    }

    public void Heal(GameObject go)
    {
        StartCoroutine(HideCard(go, 6f));
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
        go.SetActive(false);
        yield return new WaitForSeconds(time);
        go.SetActive(true);
    }

    private void Update()
    {
        if (clickToFireball)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("FireBall");
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = 0;
                clickToFireball = false;
                GameObject go =  Instantiate(fireballGO,mouseWorldPos,Quaternion.identity);
                go.transform.localScale = Vector3.one*fireballRadius;
                GameManager.instance.CheckIfInsideRadiusDamage(mouseWorldPos, fireballRadius, fireballDMG);
                Destroy(go, 2f);
            }
        }
        
    }
}
