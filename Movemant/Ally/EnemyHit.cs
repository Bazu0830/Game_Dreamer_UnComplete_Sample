using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;

public class EnemyHit : MonobitEngine.MonoBehaviour
{
    public int attackpower = 0;
    [SerializeField]
    private GameObject particleHit;
    [SerializeField]
    private Transform effectpoint;

    //オブジェクトと接触した瞬間に呼び出される
    public void OnTriggerEnter(Collider other)
    {
        // ホスト以外は処理をしない
        if (MonobitNetwork.isHost)
        {
            //攻撃した相手がEnemyの場合
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyAI>().EnableDamaged(attackpower, gameObject.transform.parent.gameObject);
                var particleHitIns = Instantiate(particleHit, effectpoint.position, effectpoint.rotation, effectpoint);
                Destroy(particleHitIns, 1f);
            }
            if (MonobitNetwork.room.parametersListedInLobby[5] == "有り")
            {
                if (other.gameObject.tag == "Player")
                {
                    if (!this.gameObject.transform.root)
                    {
                        other.gameObject.GetComponent<UnityChanScript>().EnableDamaged(attackpower);
                        var particleHitIns = Instantiate(particleHit, effectpoint.position, effectpoint.rotation, effectpoint);
                        Destroy(particleHitIns, 1f);
                    }
                }
            }
        }
    }
}
