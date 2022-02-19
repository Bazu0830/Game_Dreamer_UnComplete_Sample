using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;

public class BowAttackChara : MonobitEngine.MonoBehaviour
{
    [SerializeField]
    private EnemyAI enemyai;

    private void OnTriggerEnter(Collider other)
    {// ホスト以外は処理をしない
        if (MonobitNetwork.isHost)
        { 
            //　攻撃状態でない時に攻撃（アニメーションが攻撃状態でない時も条件に含める）
            if (other.CompareTag("Player") || other.CompareTag("Mine"))
            {
                enemyai.bowflag = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {// ホスト以外は処理をしない
        if (MonobitNetwork.isHost) 
        { 
            //　攻撃状態でない時に攻撃（アニメーションが攻撃状態でない時も条件に含める）
            if (other.CompareTag("Player") || other.CompareTag("Mine"))
            {
                enemyai.bowflag = true;
            } 
        }
    }
}
