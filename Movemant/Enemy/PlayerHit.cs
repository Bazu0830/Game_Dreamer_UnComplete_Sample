using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;

public class PlayerHit : MonobitEngine.MonoBehaviour
{
    [SerializeField]
    public EnemyAI enemyai;
    public int damageparamenemy=0;

    //オブジェクトと接触した瞬間に呼び出される
    public void OnTriggerEnter(Collider other)
    {
        // ホスト以外は処理をしない
        if (MonobitNetwork.isHost)
        {
            //攻撃した相手がPlayerの場合
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Mine")
            {
                other.gameObject.GetComponent<UnityChanScript>().EnableDamaged(damageparamenemy);
            }
        }
    }
    
}

