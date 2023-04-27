using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//player and enemy �ΐ�̊Ǘ�
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

    //�@�T���v���R���[�`��
    /*
    IEnumerator SampleCol()
    {
        Debug.Log("�R���[�`���J�n");
        yield return new WaitForSeconds(2f);
        Debug.Log("2�b�o��");
    }
    */

    //�����ݒ�
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
        //Player��Enemy�ɍU��
        StopAllCoroutines();
        SoundManager.instance.PlaySE(1);
        int damage = player.Attack(enemy);
        enemyUI.UpdateUI(enemy);
        DialogTextManager.instance.SetScenarios(new string[] {
            "Player's attack\nThe monsters takes"+damage+"damage�B"
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
        // Enemy��Player�ɍU��
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
            // player�����񂾏ꍇ�̎���
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
