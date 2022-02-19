using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MonobitEngine;

public class SpownEnemyData : MonobitEngine.MonoBehaviour
{
    [SerializeField]
    public string EnemyPrefab;
    [SerializeField]
    public GameObject Enemy;
    [SerializeField]
    private Transform spownpoint;
    private Quest quest;
    [SerializeField] AllDictionaryData alldata;
    public GameObject network;
    public MonobitRoom monobitRoom;

    private void Start()
    {
        quest= alldata.questdictionary.FirstOrDefault(c => c.Value == int.Parse(MonobitNetwork.room.customParameters["questname"].ToString())).Key;
        network = GameObject.Find("NetWork");
        Debug.Log(network.name);
        monobitRoom = network.GetComponent<MonobitRoom>();
    }

    [MunRPC]
    public void OnTriggerEnter(Collider other)
    {
        // ホスト以外は処理をしない
        if (MonobitNetwork.isHost)
        { 
            if (other.CompareTag("Mine"))
            {
                Enemy = MonobitNetwork.Instantiate(EnemyPrefab, spownpoint.position, spownpoint.rotation, 0);
                Enemy.GetComponent<EnemyAI>().monobitRoom = monobitRoom;
                if (MonobitNetwork.room.customParameters["difficulty"].ToString() == "Easy")
                {
                    Enemy.GetComponent<EnemyAI>().level += 0;
                    Enemy.GetComponent<EnemyAI>().maxhp += 0;
                    Enemy.GetComponent<EnemyAI>().hp += 0;
                    Enemy.GetComponent<EnemyAI>().maxmp += 0;
                    Enemy.GetComponent<EnemyAI>().mp += 0;
                    Enemy.GetComponent<EnemyAI>().PA += 0;
                    Enemy.GetComponent<EnemyAI>().PD -= 0;
                    Enemy.GetComponent<EnemyAI>().MA += 0;
                    Enemy.GetComponent<EnemyAI>().MD -= 0;
                }
                else if (MonobitNetwork.room.customParameters["difficulty"].ToString() == "Normal")
                {
                    Enemy.GetComponent<EnemyAI>().level += 10;
                    Enemy.GetComponent<EnemyAI>().maxhp += 20;
                    Enemy.GetComponent<EnemyAI>().hp += 20;
                    Enemy.GetComponent<EnemyAI>().maxmp += 20;
                    Enemy.GetComponent<EnemyAI>().mp += 20;
                    Enemy.GetComponent<EnemyAI>().PA += 2;
                    Enemy.GetComponent<EnemyAI>().PD -= 2;
                    Enemy.GetComponent<EnemyAI>().MA += 2;
                    Enemy.GetComponent<EnemyAI>().MD -= 2;
                }
                else if (MonobitNetwork.room.customParameters["difficulty"].ToString() == "Hard")
                {
                    Enemy.GetComponent<EnemyAI>().level += 20;
                    Enemy.GetComponent<EnemyAI>().maxhp += 40;
                    Enemy.GetComponent<EnemyAI>().hp += 40;
                    Enemy.GetComponent<EnemyAI>().maxmp += 40;
                    Enemy.GetComponent<EnemyAI>().mp += 40;
                    Enemy.GetComponent<EnemyAI>().PA += 4;
                    Enemy.GetComponent<EnemyAI>().PD -= 4;
                    Enemy.GetComponent<EnemyAI>().MA += 4;
                    Enemy.GetComponent<EnemyAI>().MD -= 4;
                }
                else if (MonobitNetwork.room.customParameters["difficulty"].ToString() == "Expert")
                {
                    Enemy.GetComponent<EnemyAI>().level += 30;
                    Enemy.GetComponent<EnemyAI>().maxhp += 80;
                    Enemy.GetComponent<EnemyAI>().hp += 80;
                    Enemy.GetComponent<EnemyAI>().maxmp += 80;
                    Enemy.GetComponent<EnemyAI>().mp += 80;
                    Enemy.GetComponent<EnemyAI>().PA += 8;
                    Enemy.GetComponent<EnemyAI>().PD -= 8;
                    Enemy.GetComponent<EnemyAI>().MA += 8;
                    Enemy.GetComponent<EnemyAI>().MD -= 8;
                }
                else
                {
                    Enemy.GetComponent<EnemyAI>().level += 40;
                    Enemy.GetComponent<EnemyAI>().maxhp += 160;
                    Enemy.GetComponent<EnemyAI>().hp += 160;
                    Enemy.GetComponent<EnemyAI>().maxmp += 160;
                    Enemy.GetComponent<EnemyAI>().mp += 160;
                    Enemy.GetComponent<EnemyAI>().PA += 16;
                    Enemy.GetComponent<EnemyAI>().PD -= 16;
                    Enemy.GetComponent<EnemyAI>().MA += 16;
                    Enemy.GetComponent<EnemyAI>().MD -= 16;
                }
            }
        }
    }
    [MunRPC]
    public void OnTriggerExit(Collider other)
    {
        // ホスト以外は処理をしない
        if (MonobitNetwork.isHost)
        { 
            if (other.CompareTag("Mine"))
            {
                MonobitNetwork.Destroy(Enemy);
            }
        }
    }
}
