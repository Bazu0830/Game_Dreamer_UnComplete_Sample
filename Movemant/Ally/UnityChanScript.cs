
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using MonobitEngine;

public class UnityChanScript : MonobitEngine.MonoBehaviour
{
    [SerializeField] MonobitView mv;

    //オンライン化に必要なコンポーネントを設定
    [SerializeField]
    public PlayerData playerdata;

    #region INTs
    public int hp;
    public int maxhp;
    public double Attack1 = 1;
    public double Attack2 = 0.8;
    public double Attack3 = 1.2;
    public double Attack4 = 1.5;
    public double Attack5 = 2;
    public double Attack6 = 3;
    public double buffpower = 1;
    public double damageparam = 1;
    public int attackpower;
    [SerializeField]
    string allyname;
    [SerializeField]
    public AllyStatus ally;
    [SerializeField]
    private Animator animator;
    //　キャラクターの速度
    private Vector3 velocity;
    [SerializeField]
    public Rigidbody rigid;
    [MunRPC][SerializeField]
    private Transform leftequip;
    [MunRPC][SerializeField]
    private Transform rightequip;
    [SerializeField]
    public Transform head;
    private int damage;
    [SerializeField]
    public Joystick joystick = null;
    public PlayerDead playerDead;
    public FishingCanvas fishingcanvas;
    public NPCTalkingCanvas nPCTalkingCanvas;
    [TextArea(1, 100)]public string npctalking;
    public bool deadflag = false;
    private double time = 0;
    private double overtime;
    private double chargepower;
    public Transform muzzle;
    public float speed = 1000;
    public string left;
    public string right;
    public Transform laddervec;
    public bool isLadder = false;
    public bool guardstance = false;
    public double StTime = 0;
    public bool levelup = false;
    public double MpTime = 0;
    public RaycastHit fallray;
    [SerializeField]
    private Transform rayPosition;
    private float fallenPosition;
    private float fallenDistance;
    private bool isFall;
    public int fallspeed = -40;
    [SerializeField]
    private Transform slopePosition;
    private RaycastHit sloperay;
    public bool isSlope = false;
    [SerializeField]
    private Transform slopeup1;
    private RaycastHit slopeup1ray;
    [SerializeField]
    private Transform slopeup2;
    [SerializeField]
    private Transform slopeup3;
    private RaycastHit slopeup2ray;
    public float slopeup;
    public int autodirection=0;
    public bool lockon = false;
    public GameObject lockontargetEnemy;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (ally == null)
        {
            ally = ScriptableObject.Instantiate(Resources.Load(allyname)) as AllyStatus;
        }
        SetWait();
        hp = ally.hp;
        maxhp = ally.maxHp;
    }
    // Update is called once per frame
    void Update()
    {
        hp = ally.hp;
        maxhp = ally.maxHp;
        if (mv.isMine)
        {
           
            #region Simple
            if (ally.hp <= 0 && deadflag == false)
            {
                SetDead();
            }
            else if (ally.hp > 0 && deadflag == true)
            {
                deadflag = false;
                animator.SetBool("Damage", false);
            }
            else
            {
                //if (!Mathf.Approximately(Input.GetAxis("Horizontal"), 0f) || !Mathf.Approximately(Input.GetAxis("Vertical"), 0f)
                if (Mathf.Abs(joystick.Horizontal) > 0.1f || Mathf.Abs(joystick.Vertical) > 0.1f)
                {
                    SetNormal();
                }
                else
                {
                    SetWait();
                }
            }
            if (isLadder == true)
            {
                LadderUP();
            }
            else
            {
                LadderOff();
            }
            #endregion

            #region AnimDamageParam 


            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                damageparam = Attack1;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            {
                damageparam = Attack2;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
            {
                damageparam = Attack3;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack4"))
            {
                damageparam = Attack4;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack5"))
            {
                damageparam = Attack5;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack6"))
            {
                damageparam = Attack6;
            }

            var input = new Vector3(joystick.Horizontal * -1, 0f, joystick.Vertical);
            if (Mathf.Clamp01(input.magnitude) > 0.7)
            {
                StTime += Time.deltaTime;
                if (StTime > 0.5)
                {
                    StTime = 0;
                    ally.st -= 1;
                }
            }
            else
            {
                StTime += Time.deltaTime;
                if (StTime > 0.2)
                {
                    StTime = 0;
                    if (ally.st < ally.maxSt)
                    {
                        ally.st += 1;
                    }
                }
            }

            if (ally.st <= 0)
            {
                SetTired();
            }

            if (ally.mp < ally.maxMp)
            {
                MpTime += Time.deltaTime;
                if (MpTime > 0.5)
                {
                    MpTime = 0;
                    ally.mp += 1;
                }
            }


            #endregion

            #region TimeCycle
            if (time <= 4 && animator.GetCurrentAnimatorStateInfo(0).IsTag("Charge"))
            {
                time += Time.deltaTime;
            }

            if (time <= 0.5)
            {
                chargepower = 1;
            }
            else if (time > 0.5 && time <= 1.5)
            {
                chargepower = 1.1;
            }
            else if (time > 1.5 && time <= 2.5)
            {
                chargepower = 1.3;
            }
            else if (time > 2.5 && time <= 3)
            {
                chargepower = 2.0;
            }
            else if (time > 3)
            {
                chargepower = 1;
            }
            #endregion

            #region 落下
            //　落ちている状態
            if (isFall == true)
            {
                fallenPosition = Mathf.Max(fallenPosition, transform.position.y);
                if (Physics.Linecast(transform.position, rayPosition.transform.position, out fallray, LayerMask.GetMask("Environment")) == true)
                {
                    //　落下距離を計算
                    fallenDistance = fallenPosition - transform.position.y;
                    //　落下によるダメージが発生する距離を超える場合ダメージを与える
                    if (fallenDistance >= 3)
                    {
                        ally.hp -= (int)(fallenDistance * 2 - 2);
                    }
                    isFall = false;
                }
            }
            else
            {
                //　地面にレイが届いていなければ落下地点を設定
                if (Physics.Linecast(transform.position, rayPosition.transform.position, out fallray, LayerMask.GetMask("Environment")) == false)
                {
                    //　最初の落下地点を設定
                    fallenPosition = transform.position.y;
                    fallenDistance = 0;
                    isFall = true;
                }
            }

            //傾斜にいるとき重力を上げて滑りやすくする
            if (isSlope == true)
            {
                rigid.AddForce(0, fallspeed, 0);
            }

            /*
            if (Physics.Linecast(transform.position, slopePosition.transform.position, out sloperay, LayerMask.GetMask("Environment")) == true)
            {
                Debug.Log(Vector3.Angle(slopePosition.transform.forward, sloperay.normal));
                float slope = Vector3.Angle(slopePosition.transform.forward, sloperay.normal);
                if (slope >= 135 && slope <= 225)
                {

                }
                else
                {

                    rigid.AddForce(0, fallspeed, 0);
                }
            }
            else
            {
                rigid.AddForce(0, fallspeed, 0);
            }
            */
            #endregion

            #region 段差

            if (Physics.Linecast(transform.position, slopeup1.transform.position, out slopeup1ray, LayerMask.GetMask("Environment")) == true)
            {
                if (Physics.Linecast(slopeup2.transform.position, slopeup3.transform.position, out slopeup2ray, LayerMask.GetMask("Environment")) == false)
                {
                    rigid.velocity = new Vector3(0, slopeup, 0);
                }
            }
            #endregion
        }
    }

    public void SetWait()
    {
        velocity = Vector3.zero;
        animator.SetFloat("Speed", 0f);
    }
    public void SetNormal()
    {
        velocity = Vector3.zero;

        //var input = new Vector3(-Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        var input = new Vector3(joystick.Horizontal * -1, 0f, joystick.Vertical);

        if ((input.magnitude > 0.1f)&&
            animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            //　押した方向をカメラの向きに合わせて変換
            var convertInputToCameraDirection = Quaternion.FromToRotation(input, new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z));
            //　ユニティちゃんの角度をカメラの方向に合わせた角度のY値分だけ回転させる
            transform.rotation = Quaternion.AngleAxis(convertInputToCameraDirection.eulerAngles.y, Vector3.up);
            animator.SetFloat("Speed", Mathf.Clamp01(input.magnitude));
        }

        else
        {
            animator.SetFloat("Speed", 0f);

        }
    }
    public void SetTired()
    {
        animator.SetInteger("AnyType", 0);
        animator.SetBool("Any", true);

    }

    public void SetGuard()
    {
        animator.SetBool("BlockStart", true);
        guardstance = true;

    }
    public void SetNotGuard()
    {
        animator.SetInteger("GuardType", 0);
        animator.SetBool("Guard", true);
        guardstance = false;
    }
    public void Jump()
    {

        animator.SetInteger("GuardType", 2);
        animator.SetBool("Guard", true);//緊急回避
    }
    public void Roling()
    {
        animator.SetInteger("GuardType", 1);
        animator.SetBool("Guard", true);//回避

    }
    public void Counter()
    {
        animator.SetInteger("GuardType", 3);
        animator.SetBool("Guard", true);//回避

    }

    public void ReStart()
    {
        animator.SetInteger("DamageType", 2);
        animator.SetBool("Damage", true);
        deadflag = false;
        velocity = Vector3.zero;
    }

    public void SetDead()
    {
        deadflag = true;
        animator.SetInteger("DamageType", 1);
        animator.SetBool("Damage", true);
        playerDead.IsPlayerDead();
        velocity = Vector3.zero;

    }

    public void ChargeAttack()
    {
        time = 0;
        animator.SetBool("ChargeStart", true);
    }

    public void SetNAttack()
    {
        animator.SetInteger("AttackType", 0);
        animator.SetBool("Attack", true);

        AttackPower();
    }
    public void Tackle()
    {
        animator.SetInteger("AttackType", 2);
        animator.SetBool("Attack", true);

    }
    public void LowAttack()
    {
        animator.SetInteger("AttackType", 3);
        animator.SetBool("Attack", true);

    }

    public void EnableDamaged(double damageparam)
    {
        if (ally.hp > 0)
        {

            velocity = Vector3.zero;
            if (guardstance == false)
            {
                animator.SetInteger("DamageType", 0);
                animator.SetBool("Damage", true);
                animator.SetFloat("Speed", 0f);
                Debug.Log("damaged");
                double a = (double)(damageparam * (double)(100 / (100 + (double)ally.PD)));
                damage = (int)a;

            }
            else
            {
                animator.SetInteger("DamageType", 0);
                animator.SetBool("Damage", true);
                animator.SetFloat("Speed", 0f);
                Debug.Log("damaged");
                double a = (double)(damageparam * (double)((100 / (100 + (double)ally.PD)) * 0.3));
                damage = (int)a;
            }
            if (damage <= 1)
            {
                damage = 1;
            }
            ally.hp -= damage;
            if (ally.hp <= 0)
            {
                ally.hp = 0;
            }

        }
    }
    public void EnableMagicDamaged(double damageparam)
    {
        if (ally.hp > 0)
        {
            velocity = Vector3.zero;
            if (guardstance == false)
            {
                animator.SetInteger("DamageType", 0);
                animator.SetBool("Damage", true);
                animator.SetFloat("Speed", 0f);
                Debug.Log("damaged");
                double a = (double)(damageparam * (double)(100 / (100 + (double)ally.MD)));
                damage = (int)a;
            }
            else
            {
                animator.SetInteger("DamageType", 0);
                animator.SetBool("Damage", true);
                animator.SetFloat("Speed", 0f);
                Debug.Log("damaged");
                double a = (double)(damageparam * (double)((100/(100+ (double)ally.MD)) * 0.3));
                damage = (int)a;
               
            }
            if (damage <= 1)
            {
                damage = 1;
            }
            ally.hp -= damage;
            if (ally.hp <= 0)
            {
                ally.hp = 0;
            }

        }
    }

    public void LadderUP()
    {

        isFall = false;
        transform.rotation = laddervec.rotation;
        animator.SetFloat("LadderSpeed", Mathf.Clamp01(joystick.Vertical));
        animator.SetBool("Ladder", true);

        if (joystick.Horizontal < -0.9f)
        {
            isLadder = false;
        }
    }
    public void LadderOff()
    {

        animator.SetBool("Ladder", false);
    }

    public void AttackPower()
    {
        attackpower = (int)(ally.PA * damageparam * buffpower * chargepower);
        mv.RPC("RPCAttackPower", MonobitTargets.All);
    }
    [MunRPC]
    public void RPCAttackPower()
    {
        leftequip.GetChild(0).gameObject.GetComponent<EnemyHit>().attackpower = attackpower;
        rightequip.GetChild(0).gameObject.GetComponent<EnemyHit>().attackpower = attackpower;
    }

    public void FishingOpen()
    {

        fishingcanvas.PanelOpen();
    }
    public void FishingStart()
    {

        fishingcanvas.FishStart();
        animator.SetInteger("AnyType", 1);
        animator.SetBool("Any", true);
    }
    public void FishingEnd()
    {

        fishingcanvas.Fish();
        animator.SetBool("Any", true);
    }
    public void FishingClose()
    {

        fishingcanvas.PanelClose();
    }
    public void NpcTalk()
    {

        nPCTalkingCanvas.npctalking = npctalking;
        nPCTalkingCanvas.NPCTalkingOn();
    }
    public void NotNpcTalk()
    {

        nPCTalkingCanvas.NPCTalkingOff();
    }
    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
    /// <summary></summary>
    /// <param name="stream">MonobitAnimatorViewの送信データ、または受信データのいずれかを提供するパラメータ</param>
    /// <param name="info">特定のメッセージやRPCの送受信、または更新に関する「送信者、対象オブジェクト、タイムスタンプ」などの情報を保有するパラメータ</param>
    public void OnMonobitSerializeView(MonobitEngine.MonobitStream stream, MonobitEngine.MonobitMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.Enqueue(hp);
            stream.Enqueue(maxhp);
            stream.Enqueue(attackpower);
            stream.Enqueue(left);
            stream.Enqueue(right);
        }
        else
        {
            hp = (int)stream.Dequeue();
            maxhp = (int)stream.Dequeue();
            attackpower = (int)stream.Dequeue();
            left = (string)stream.Dequeue();
            right = (string)stream.Dequeue();
        }
    }
}