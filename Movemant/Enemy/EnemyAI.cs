using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;

public class EnemyAI : MonobitEngine.MonoBehaviour
{
    #region INTs
    [SerializeField]
    public MonobitView mv;
    public enum EquipType
    {
        Sword,
        BoworSpell,
    }
    public EquipType equipType = EquipType.Sword;
    public enum EnemyType
    {
        Boss,
        NotBoss
    }
    public EnemyType enemyType = EnemyType.Boss;
    public string charaname = "辺境の騎士";
    public bool ispoisoness = false;
    public bool numbness = false;
    public int level = 10;
    public int maxhp = 100;
    public int hp=100;
    public int maxmp = 100;
    public int mp = 100;
    public int PA = 30;//強100～20弱
    public int PD = 50;//強30～70弱
    public int MA = 30;//強100～20弱
    public int MD = 50;//強30～70弱
    public float motionattack = 1;
    [SerializeField]
    private Animator animator;
    //　目的地
    private Vector3 destination;
    //　向きを回転する速さ
    [SerializeField]
    private float rotateSpeed = 2f;
    [SerializeField]
    private Transform Lequip;
    [SerializeField]
    private Transform Requip;
    private int damage;
    [SerializeField]
    private GameObject dropItemObj;
    public List<GameObject> playerlist;
    public GameObject player;
    public bool chaseflag = false;
    public bool attackflag = false;
    public bool bowflag = false;
    public bool deadflag = false;
    private float time = 0f;
    private float interval = 5f;
    private int Attacktype = 0;
    public int maxAttacktype = 5;
    public float maxinterval = 3;
    public int playerpower;
    private bool selecttarget = false;
    private float speed = 0;
    public MonobitRoom monobitRoom;
    #endregion

    // Use this for initialization
    void Start()
    {
        // ホスト以外は処理をしない
        if (!MonobitNetwork.isHost)
        {
            Walk();
        }
    }
    // Update is called once per frame
    void Update()
    {
        // ホスト以外は処理をしない
        if (MonobitNetwork.isHost)
        {

            if (deadflag == false)
            {
                if (playerlist.Count <= 0)
                {
                    chaseflag = false;
                }
                else
                {
                    chaseflag = true;
                }


                time += Time.deltaTime;
                if (time > interval)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("Idle")))
                    {
                        if (chaseflag == true)
                        {
                            if (selecttarget == false)
                            {
                                player = playerlist[Random.Range(0, playerlist.Count)];
                                selecttarget = true;
                                destination = player.transform.position;
                                float direction = Vector3.Distance(destination, transform.position);
                                if (direction > 3)
                                {
                                    speed = 1f;
                                    Attacktype = Random.Range(0, maxAttacktype);
                                }
                                else
                                {
                                    speed = 0.3f;
                                    Attacktype = Random.Range(5, maxAttacktype + 5);
                                }
                            }
                            else
                            {

                                if (equipType == EquipType.Sword)
                                {

                                    if (attackflag == false)
                                    {
                                        Chase();
                                    }
                                    else
                                    {
                                        time = 0;
                                        interval = Random.Range(1f, maxinterval);
                                        selecttarget = false;
                                        Attack(Attacktype);
                                    }
                                }
                                else
                                {
                                    if (bowflag == false)
                                    {
                                        ChaseOnBow();
                                    }
                                    else
                                    {
                                        //ここで終了
                                        player = playerlist[Random.Range(0, playerlist.Count)];
                                        Attacktype = Random.Range(0, maxAttacktype);
                                        time = 0;
                                        interval = Random.Range(1f, maxinterval);
                                        selecttarget = false;
                                        Attack(Attacktype);
                                    }
                                }

                                if (interval > 5f)
                                {
                                    time = 0;
                                    interval = Random.Range(1f, maxinterval);
                                    selecttarget = false;
                                }
                            }
                            if (interval > 5f)
                            {
                                time = 0;
                                interval = Random.Range(1f, maxinterval);
                                selecttarget = false;
                            }
                        }
                        else
                        {
                            //ここで終了
                            time = 0;
                            interval = Random.Range(1f, maxinterval);
                            selecttarget = false;
                            Walk();
                        }
                    }
                }
            }
            else
            {
                Dead();
            }
        }
        
    } 
    private void Walk()
    {
        animator.SetFloat("Speed", 0.3f);
        float a = Random.Range(0f, 100f);
        float b = Random.Range(0f, 100f);
        float c = Random.Range(50f, 100f);
        destination = new Vector3(a, c, b);
        var direction = (destination - transform.position).normalized;
        var targetRot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(destination - transform.position), Time.deltaTime * rotateSpeed);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetRot.eulerAngles.y, transform.eulerAngles.z);
    }
    public void Chase()
    {
        destination = player.transform.position;
        var targetRot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(destination - transform.position), Time.deltaTime * rotateSpeed);
        animator.SetFloat("Speed", speed);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetRot.eulerAngles.y, transform.eulerAngles.z);
    }
    public void ChaseOnBow()
    {
        destination = player.transform.position;
        var targetRot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(destination - transform.position), Time.deltaTime * rotateSpeed);
        animator.SetFloat("Speed", -1);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetRot.eulerAngles.y, transform.eulerAngles.z);
    }
    public void Attack(int num)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("Idle")))
        {
            
            if (num == 0) { animator.SetInteger("AttackType", 0); motionattack = 1f; }
            else if (num == 1) { animator.SetInteger("AttackType", 1); motionattack = 1.2f; }
            else if (num == 2) { animator.SetInteger("AttackType", 2); motionattack = 1.3f; }
            else if (num == 3) { animator.SetInteger("AttackType", 3); motionattack = 1.8f; }
            else if (num == 4) { animator.SetInteger("AttackType", 4); motionattack = 2f; }
            else if (num == 5) { animator.SetInteger("AttackType", 5); motionattack = 2.3f; }
            else if (num == 6) { animator.SetInteger("AttackType", 6); motionattack = 3f; }
            else if (num == 7) { animator.SetInteger("AttackType", 7); motionattack = 5f; }
            animator.SetBool("Attack", true);
            var targetRot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), Time.deltaTime * 2f);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetRot.eulerAngles.y, transform.eulerAngles.z);
            Lequip.GetChild(0).gameObject.GetComponent<PlayerHit>().damageparamenemy = (int)(PA * motionattack);
            Requip.GetChild(0).gameObject.GetComponent<PlayerHit>().damageparamenemy = (int)(PA * motionattack);
        }
    }
    public void EnableDamaged(int Power,GameObject enemy)
    {
        // ホスト以外は処理をしない
        if (MonobitNetwork.isHost)
        {
            playerpower = Power;

            if (deadflag == false)
            {
                animator.SetFloat("Speed", 0);
                animator.SetInteger("DamageType", 0);
                animator.SetBool("Damage", true);
                double a= (double)(playerpower * (double)(100 / (100 + (double)PD)));
                damage = (int)a;
                if (damage <= 1)
                {
                    damage = 1;
                }
                hp -= damage;
                if (hp <= 0)
                {
                    Dead();
                }
                for (int i = 0; i < playerlist.Count; i++)
                {
                    if (!playerlist.Contains(enemy))
                    {
                        playerlist.Add(enemy);
                    }
                }
            }
        }
    }
    
    private void Dead()
    {
        if (deadflag == false)
        {
            deadflag = true;
            animator.SetInteger("DamageType", 1);
            animator.SetBool("Damage", true);
            if (enemyType == EnemyType.Boss)
            {
                mv.RPC("PunBossDead", MonobitTargets.All);
            }
            else
            {
                mv.RPC("PunDead", MonobitTargets.All);
            }
            
        }
    }
    [MunRPC]
    private void PunDead()
    {
        //　設定したアイテムを敵の1m上から落とす
        Instantiate<GameObject>(dropItemObj, transform.position + Vector3.up, Quaternion.identity);
    }
    [MunRPC]
    private void PunBossDead()
    {
        //　設定したアイテムを敵の1m上から落とす
        Instantiate<GameObject>(dropItemObj, transform.position + Vector3.up, Quaternion.identity);
        monobitRoom.QuestClear();
    }
    public void OnPhotonSerializeView(MonobitStream stream,MonobitMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.Enqueue(hp);
            stream.Enqueue(maxhp);
        }
        else
        {
            hp = (int)stream.Dequeue();
            maxhp = (int)stream.Dequeue();
        }
    }
}