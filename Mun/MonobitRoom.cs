using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MonobitEngine;
using System.Collections.Generic;
using System.Linq;

public class MonobitRoom : MonobitEngine.MunMonoBehaviour
{
    //誰かがログインする度に生成するプレイヤーPrefab
                     public string playerPrefab;
                     public Transform spownpoint;
    [SerializeField] public Transform spownpoint1;
    [SerializeField] public Transform spownpoint2;
    [SerializeField] public Transform spownpoint3;
    [SerializeField] public Transform spownpoint4;
    [SerializeField] public PlayerData playerdata;
    [SerializeField] public TouchController Camera;
    [SerializeField] public GameObject Canvas;
    [SerializeField] public Joystick Joystick;
                     public GameObject PlayerIns=null;
                     public string AllyName;
                     public AllyStatus allyIns;
                     public int PlayerNum;
    [SerializeField] public Text RoomIDText;
    [SerializeField] public SpownMap spownMap;
    [SerializeField] AllDictionaryData alldata;
    [SerializeField] FancyScrollView.Example038.Example038 example;

    [SerializeField] GameObject NowLoadingPanel;
    [SerializeField] Slider NowLoadingSlider;
    private AsyncOperation async;

    private void Start()
    {
        if (!MonobitNetwork.isConnect || !MonobitNetwork.inRoom)
        {
            LeaveRoom();
            Debug.Log("接続に失敗しました");
            return;
        }
        if (MonobitNetwork.room.maxPlayers == 1)
        {
            MonobitNetwork.offline = true;
            Debug.Log("最大人数が１人に設定されているため、オフラインモードに移行しました。");
        }
        spownMap.MapInfo = MonobitNetwork.room.customParameters["stage"].ToString();
        RoomIDText.text = "部屋ID:" + MonobitNetwork.room.name;
        PlayerNum = MonobitNetwork.playerList.Length;
        if (PlayerNum == 1)
        {
            spownpoint = spownpoint1;
        }
        else if (PlayerNum == 2)
        {
            spownpoint = spownpoint2;
        }
        else if (PlayerNum == 3)
        {
            spownpoint = spownpoint3;
        }
        else
        {
            spownpoint = spownpoint4;
        }

        playerPrefab = playerdata.ally.ObjectName;
        AllyName = playerdata.ally.characterName;
        allyIns = ScriptableObject.Instantiate(Resources.Load(AllyName)) as AllyStatus;
        PlayerIns = MonobitNetwork.Instantiate(playerPrefab, spownpoint.position, spownpoint.rotation, 0);
        PlayerIns.tag = "Mine";
        PlayerIns.GetComponent<UnityChanScript>().playerdata = playerdata;
        PlayerIns.GetComponent<UnityChanScript>().joystick = Joystick;
        PlayerIns.GetComponent<UnityChanScript>().playerDead = Canvas.GetComponent<PlayerDead>();
        PlayerIns.GetComponent<UnityChanScript>().fishingcanvas = Canvas.GetComponent<FishingCanvas>();
        PlayerIns.GetComponent<UnityChanScript>().nPCTalkingCanvas = Canvas.GetComponent<NPCTalkingCanvas>();
        PlayerIns.GetComponent<UnityChanScript>().ally = allyIns;
        Camera.player = PlayerIns.GetComponent<UnityChanScript>().head.gameObject;
        Canvas.GetComponent<PlayerButtonController>().player = PlayerIns;
        Canvas.GetComponent<DisplayHPMP>().player = PlayerIns;
        Canvas.GetComponent<DisplayHPMP>().allystatus = PlayerIns.GetComponent<UnityChanScript>().ally;
        example.ally = allyIns;
        StartCoroutine(DelayMethod(2.0f, () =>
        {
            Canvas.GetComponent<MenuOpen>().ToGame();
        }));
    }

    private float time = 0f;
    private void OnGUI()
    {
        time += Time.deltaTime;
    }

    public void LeaveRoom()
    {
        // 退室
        MonobitNetwork.LeaveRoom();
        StartCoroutine("LoadData");
    }

    private int deathcount=0; 

    public void ReStart()
    {
        allyIns.hp = allyIns.maxHp;
        PlayerIns.GetComponent<UnityChanScript>().ReStart();
        Canvas.GetComponent<MenuOpen>().ToGame();
        deathcount++;
    }

    public void QuestClear()
    {
        Canvas.GetComponent<MenuOpen>().ClearPanel();
        Text text = Canvas.GetComponent<MenuOpen>().Text;
        int score = (int)(10000 - time - (deathcount * 100));
        if (score < 0)
        {
            score = 0;
        }
        text.text = "目標達成\n" + "クリアタイム  " + (int)(time / 60) + "m" + (int)(time % 60) + "s"
            + "\n" + "復活回数  " + deathcount + "回" 
            + "\n" + "スコア\n" + score;

    }


    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

    IEnumerator LoadData()
    {

        async = SceneManager.LoadSceneAsync("zero");

        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            NowLoadingSlider.value = progressVal;
            yield return null;
        }
    }
}