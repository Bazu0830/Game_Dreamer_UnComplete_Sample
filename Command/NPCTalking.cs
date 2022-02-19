using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class NPCTalking : MonoBehaviour
{/*
    [SerializeField]
    private Image image1;
    [SerializeField]
    private Image image2;
    [SerializeField]
    private Sprite name1;
    [SerializeField]
    private Sprite name2;
    [SerializeField]
    private string nameString1 = "<1>";
    [SerializeField]
    private string nameString2 = "<2>";
    */

    [SerializeField]
    MenuOpen menuopen;
    [TextArea(1,100)]
    public string talkall;
    [SerializeField]
    private Text text;
    [SerializeField]
    private string splitString = "<>";
    private string[] splitMessage;
    private int messageNum;
    [SerializeField]
    private float textSpeed = 0.05f;
    private float elapsedTime = 0f;
    private int nowTextNum = 0;
    [SerializeField]
    private Image clickIcon;
    [SerializeField]
    private float clickFlashTime = 0.2f;
    private bool isOneMessage = false;
    private bool isEndMessage = true;

    void Update()
    {
        //　メッセージが終わっているか、メッセージがない場合はこれ以降何もしない
        if (isEndMessage || talkall == null)
        {
            return;
        }

        //　1回に表示するメッセージを表示していない	
        if (!isOneMessage)
        {
            //　テキスト表示時間を経過したらメッセージを追加
            if (elapsedTime >= textSpeed)
            {
                text.text += splitMessage[messageNum][nowTextNum];

                nowTextNum++;
                elapsedTime = 0f;

                //　メッセージを全部表示、または行数が最大数表示された
                if (nowTextNum >= splitMessage[messageNum].Length)
                {
                    isOneMessage = true;
                }
            }
            elapsedTime += Time.deltaTime;

            //　メッセージ表示中にマウスの左ボタンを押したら一括表示
            if (Input.GetMouseButtonDown(0))
            {
                //　ここまでに表示しているテキストに残りのメッセージを足す
                text.text += splitMessage[messageNum].Substring(nowTextNum);
                isOneMessage = true;
            }
            //　1回に表示するメッセージを表示した
        }
        else
        {

            elapsedTime += Time.deltaTime;

            //　クリックアイコンを点滅する時間を超えた時、反転させる
            if (elapsedTime >= clickFlashTime)
            {
                clickIcon.enabled = !clickIcon.enabled;
                elapsedTime = 0f;
            }

            //　マウスクリックされたら次の文字表示処理
            if (Input.GetMouseButtonDown(0))
            {
                nowTextNum = 0;
                messageNum++;
                text.text = "";
                clickIcon.enabled = false;
                elapsedTime = 0f;
                isOneMessage = false;

                //　メッセージが全部表示されていたらゲームオブジェクト自体の削除
                if (messageNum >= splitMessage.Length)
                {
                    isEndMessage = true;
                    menuopen.ToGame();
                }
            }
        }
    }
    //　新しいメッセージを設定
    public void SetMessage()
    { 
        //　分割文字列で一回に表示するメッセージを分割する
        splitMessage = Regex.Split(talkall, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        nowTextNum = 0;
        messageNum = 0;
        isOneMessage = false;
        isEndMessage = false;
    }
   
}
