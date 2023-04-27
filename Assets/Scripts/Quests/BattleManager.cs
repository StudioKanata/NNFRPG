using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//player and enemy 対戦の管理
public class BattleManager : MonoBehaviour
{
    public Transform playerDamagePanel;
    public QuestManager questManager;
    public PlayerUIManager playerUI;
    public EnemyUIManager enemyUI;
    public PlayerManager player;
    EnemyManager enemy;

    private void Start()
    {
        enemyUI.gameObject.SetActive(false);
        //StartCoroutine(SampleCol());
        playerUI.SetupUI(player);
    }

    //　サンプルコルーチン
    /*
    IEnumerator SampleCol()
    {
        Debug.Log("コルーチン開始");
        yield return new WaitForSeconds(2f);
        Debug.Log("2秒経過");
    }
    */

    //初期設定
    public void Setup(EnemyManager enemyManager)
    {
        SoundManager.instance.PlayBGM("Battle");
        enemyUI.gameObject.SetActive(true);
        enemy = enemyManager;
        enemyUI.SetupUI(enemy);
        playerUI.SetupUI(player);

        enemy.AddeventListenerOnTap(PlayerAttack);

        // enemy.transform.DOMove(new Vector3(0,10,0), 5f);
    }

    void PlayerAttack()
    {
        //PlayerがEnemyに攻撃
        StopAllCoroutines();
        SoundManager.instance.PlaySE(1);
        int damage = player.Attack(enemy);
        enemyUI.UpdateUI(enemy);
        DialogTextManager.instance.SetScenarios(new string[] {
            "Player's attack\nThe monsters takes"+damage+"damage。"
        });

        if (enemy.hp <= 0)
        {
            StartCoroutine(EndBattle());
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        // EnemyがPlayerに攻撃
        yield return new WaitForSeconds(2f);
        SoundManager.instance.PlaySE(1);
        playerDamagePanel.DOShakePosition(0.3f, 0.5f, 20, 0, false, true);
        int damage = enemy.Attack(player);
        playerUI.UpdateUI(player);
        DialogTextManager.instance.SetScenarios(new string[] { 
            "Monsters' attack\nPlayer takes"+damage+"damage!" 
        });
        yield return new WaitForSeconds(2f);
        if (player.hp <= 0)
        {
            // playerが死んだ場合の実装
            questManager.PlayerDeath();
        }
    }

     IEnumerator EndBattle()
    {
        yield return new WaitForSeconds(2f);
        DialogTextManager.instance.SetScenarios(new string[] {
            "Player did"
        });

        enemyUI.gameObject.SetActive(false);
        Destroy(enemy.gameObject);
        SoundManager.instance.PlayBGM("Quest");
        questManager.EndBattle();
    }

}
