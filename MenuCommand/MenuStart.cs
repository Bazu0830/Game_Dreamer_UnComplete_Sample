using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class MenuStart : MonoBehaviour
{
    void Start()
    {
        StartPanelOpen();
        OnlinePanelClose();
        StatusMysetPanel();
        GatyaPanel();
        SettingPanelClose();
        SetHomeChara();
    }
    private void OnGUI()
    {
        PlayerDataOpen();
        MedalHave();
    }

    #region Main
    [SerializeField]
    GameObject StartPanel;
    [SerializeField]
    GameObject OnlinePanel;
    [SerializeField]
    GameObject ShopPanel;
    [SerializeField]
    GameObject StatusPanel;
    [SerializeField]
    GameObject SettingsPanel;

    public void StartPanelOpen()
    {
        StartPanel.SetActive(true);
        OnlinePanel.SetActive(false);
        ShopPanel.SetActive(false);
        StatusPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }
    public void SelectOnline()
    {
        StartPanel.SetActive(false);
        OnlinePanel.SetActive(true);
        ShopPanel.SetActive(false);
        StatusPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }
    public void SelectShop()
    {
        StartPanel.SetActive(false);
        OnlinePanel.SetActive(false);
        ShopPanel.SetActive(true);
        StatusPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }
    public void SelectStatus()
    {
        StartPanel.SetActive(false);
        OnlinePanel.SetActive(false);
        ShopPanel.SetActive(false);
        StatusPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }
    public void SelectSettingsPanel()
    {
        StartPanel.SetActive(false);
        OnlinePanel.SetActive(false);
        ShopPanel.SetActive(false);
        StatusPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }
    #endregion

    #region OnlinePanel
    [SerializeField]
    GameObject MakeRoomPanel;
    [SerializeField]
    GameObject FindRoomPanel;
    [SerializeField]
    GameObject RandomRoomPanel;

    public void OnlinePanelClose()
    {
        MakeRoomPanel.SetActive(false);
        FindRoomPanel.SetActive(false);
        RandomRoomPanel.SetActive(false);
    }
    public void MakeRoomPanelOpen()
    {
        MakeRoomPanel.SetActive(true);
        FindRoomPanel.SetActive(false);
        RandomRoomPanel.SetActive(false);
    }
    public void FindRoomPanelOpen()
    {
        MakeRoomPanel.SetActive(false);
        FindRoomPanel.SetActive(true);
        RandomRoomPanel.SetActive(false);
    }
    public void RadomRoomPanelOpen()
    {
        MakeRoomPanel.SetActive(false);
        FindRoomPanel.SetActive(false);
        RandomRoomPanel.SetActive(true);
    }
    #endregion

    #region StatusPanel
    [SerializeField]
    GameObject mysetPanel;
    [SerializeField]
    GameObject CharaPanel;

    public void StatusMysetPanel()
    {
        mysetPanel.SetActive(true);
        CharaPanel.SetActive(false);
       
    }
    public void StatusCharaPanel()
    {
        mysetPanel.SetActive(false);
        CharaPanel.SetActive(true);
        
    }
    
    #endregion

    #region 設定パネル
    [SerializeField]
    GameObject SettingPanel;

    public void SettingPanelClose()
    {
        SettingPanel.SetActive(true);
    }
    #endregion

    #region 課金
    [SerializeField]
    GameObject gatyaPanel;
    [SerializeField]
    GameObject buyPanel;
    [SerializeField]
    GameObject TradePanel;

    public void GatyaPanel()
    {
        gatyaPanel.SetActive(true);
        buyPanel.SetActive(false);
        TradePanel.SetActive(false);
    }
    public void OpenBuyPanel()
    {
        gatyaPanel.SetActive(false);
        buyPanel.SetActive(true);
        TradePanel.SetActive(false);
    }
    public void TradePanelOpen()
    {
        gatyaPanel.SetActive(false);
        buyPanel.SetActive(false);
        TradePanel.SetActive(true);
    }
    #endregion

    #region 個人情報表示
    [SerializeField]
    AllDictionaryData alldata;
    [SerializeField]
    public PlayerData playerdata;
    [SerializeField]
    public Text nametext;
    [SerializeField]
    public Text ranktext;
    [SerializeField]
    public Text kakintext;

    public void PlayerDataOpen()
    {
        nametext.text = playerdata.Name;
        ranktext.text = "ランク："+playerdata.rank;
        kakintext.text = "魔書の断片：" + playerdata.gatya;
    }

    #endregion

    #region 総合値計算

    [SerializeField] GameObject AllyPanel1;
    [SerializeField] GameObject AllyPanel2;
    [SerializeField] GameObject AllyPanel3;
    [SerializeField] GameObject AllyPanel4;
    [SerializeField] Image AllyImage1;
    [SerializeField] Image AllyImage2;
    [SerializeField] Image AllyImage3;
    [SerializeField] Image AllyImage4;

    public void OpenAllyImage()
    {
        if(playerdata.ally == null) { AllyPanel1.SetActive(true); } else { AllyPanel1.SetActive(false); AllyImage1.sprite = playerdata.ally.Sprite; }
        if (playerdata.ally2 == null) { AllyPanel2.SetActive(true); } else { AllyPanel2.SetActive(false); AllyImage2.sprite = playerdata.ally2.Sprite; }
        if (playerdata.ally3 == null) { AllyPanel3.SetActive(true); } else { AllyPanel3.SetActive(false); AllyImage3.sprite = playerdata.ally3.Sprite; }
        if (playerdata.ally4 == null) { AllyPanel4.SetActive(true); } else { AllyPanel4.SetActive(false); AllyImage4.sprite = playerdata.ally4.Sprite; }
    }
    #endregion

    #region 記念メダル
    [SerializeField]
    Text medaltext;

    public void MedalHave()
    {
        medaltext.text = "記念メダル："+playerdata.medal+"枚";
    }
    #endregion

    #region ホーム画面キャラ
    [SerializeField]
    public Transform spownpoint;
    public GameObject PlayerIns;
    private string AllyName;
    public AllyStatus allyIns;
    private GameObject playerPrefab;
    [SerializeField]
    AllyStatus defaultchara;
    [SerializeField]
    Camera maincamera;

    public void SetHomeChara()
    {
        Destroy(PlayerIns);
        if (playerdata.ally == null)
        {
            AllyName = defaultchara.characterName;
            playerPrefab = defaultchara.gameObject;
            allyIns = ScriptableObject.Instantiate(Resources.Load(AllyName)) as AllyStatus;
            PlayerIns = Instantiate(playerPrefab, spownpoint.position, spownpoint.rotation);
            PlayerIns.tag = "Mine";
            PlayerIns.GetComponent<UnityChanScript>().enabled = false;
            maincamera.GetComponent<ZeroCamera>().targetObject = PlayerIns;
        }
        else
        {
            AllyName = playerdata.ally.characterName;
            playerPrefab = playerdata.ally.gameObject;
            allyIns = ScriptableObject.Instantiate(Resources.Load(AllyName)) as AllyStatus;
            PlayerIns = Instantiate(playerPrefab, spownpoint.position, spownpoint.rotation);
            PlayerIns.tag = "Mine";
            PlayerIns.GetComponent<UnityChanScript>().enabled = false;
            maincamera.GetComponent<ZeroCamera>().targetObject = PlayerIns;
        }
    }
        
    #endregion

    #region 各種設定
    public InputField ChangeNameInputField;

    public void ChangeName()
    {
        if (ChangeNameInputField.text != "")
        {
            playerdata.Name=ChangeNameInputField.text;
        }
    }

    public InputField SecretKeyInputField;

    public void SecretKey()
    {
        if (SecretKeyInputField.text == "T321Jrc6")
        {
            playerdata.gatya += 1000;
        }
    }
    #endregion

    #region オンラインパネル総合値

    [SerializeField] GameObject Panel1;
    [SerializeField] GameObject Panel2;
    [SerializeField] GameObject Panel3;
    [SerializeField] GameObject Panel4;
    [SerializeField] Image Image1;
    [SerializeField] Image Image2;
    [SerializeField] Image Image3;
    [SerializeField] Image Image4;

    public void OpenImage()
    {
        if (playerdata.ally == null) { Panel1.SetActive(true); } else { Panel1.SetActive(false); Image1.sprite = playerdata.ally.Sprite; }
        if (playerdata.ally2 == null) { Panel2.SetActive(true); } else { Panel2.SetActive(false); Image2.sprite = playerdata.ally2.Sprite; }
        if (playerdata.ally3 == null) { Panel3.SetActive(true); } else { Panel3.SetActive(false); Image3.sprite = playerdata.ally3.Sprite; }
        if (playerdata.ally4 == null) { Panel4.SetActive(true); } else { Panel4.SetActive(false); Image4.sprite = playerdata.ally4.Sprite; }
    }
    #endregion
}
