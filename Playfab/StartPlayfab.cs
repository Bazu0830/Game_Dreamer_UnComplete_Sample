using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using System;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;

public class StartPlayfab : MonoBehaviour
{

   [SerializeField] GetPlayerCombinedInfoRequestParams InfoRequestParams;

    // Start is called before the first frame update
    void Start()
    {

        PlayFabClientAPI.LoginWithCustomID(
            new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true }
        , result => Debug.Log("おめでとうございます！ログイン成功です！")
        , error => Debug.Log("ログイン失敗...(´；ω；｀)"));

        InfoRequestParams.GetUserData = true; // プレイヤーデータを取得する
        InfoRequestParams.GetTitleData = true; // タイトルデータを取得する

        PlayFabAuthService.Instance.InfoRequestParams = InfoRequestParams; // ここを追加!!
        PlayFabAuthService.OnLoginSuccess += PlayFabLogin_OnLoginSuccess;
        PlayFabAuthService.Instance.Authenticate(Authtypes.Silent);
    }
    void OnEnable()
    {
        PlayFabAuthService.OnLoginSuccess += PlayFabLogin_OnLoginSuccess;
    }
    private void PlayFabLogin_OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login Success!");
    }
    private void OnDisable()
    {
        PlayFabAuthService.OnLoginSuccess -= PlayFabLogin_OnLoginSuccess;
    }

    const string PLAYFAB_CUSTOM_ID = "PLAYFAB_CUSTOM_ID";
    public static string CustomId { get => PlayerPrefs.GetString(PLAYFAB_CUSTOM_ID, Guid.NewGuid().ToString()); }

    public void SaveCustomId()
    {
        PlayerPrefs.SetString(PLAYFAB_CUSTOM_ID, CustomId);
        PlayerPrefs.Save();
    }

    public void Login()
    {
        PlayFabAuthService.OnLoginSuccess += PlayFabAuthService_OnLoginSuccess;
        PlayFabAuthService.Instance.Authenticate(Authtypes.Silent);
    }

    private void PlayFabAuthService_OnLoginSuccess(LoginResult success)
    {
        // 新規作成したかどうか
        if (success.NewlyCreated) { }
        // ユーザー名を入力する画面に遷移
        else { }
        // メインメニューに遷移
    }
    // 表示名の入力コントロール
    [SerializeField] TMP_InputField inputName;

    #region プレイヤー表示名の更新
    private void UpdateUserTitleDisplayName()
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = inputName.text
        }, result =>
        {
            Debug.Log("プレイヤー名：" + result.DisplayName);
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }
    #endregion

    // 完了ボタン
    [SerializeField] Button inputComp;

    public void InputValueChanged()
    {
        inputComp.interactable = IsValidName();
    }

    private bool IsValidName()
    {
        // 表示名は、３文字以上２５文字以下
        return !string.IsNullOrWhiteSpace(inputName.text)
            && 3 <= inputName.text.Length
            && inputName.text.Length <= 25;
    }
    #region プレイヤーの初期化
    private void InitPlayer()
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>
        {
            { "Exp", "0" },
            { "Rank", "1" },
        }
        };

        PlayFabClientAPI.UpdateUserData(request
            , result =>
            {
                Debug.Log("プレイヤーの初期化完了");
            // ------------------------------
            // 処理成功時のコールバックで表示名の更新
            // ------------------------------
            UpdateUserTitleDisplayName();
            }, error => Debug.LogError(error.GenerateErrorReport()));
    }
    #endregion
    // 完了ボタンが押されたときの処理
    private void InputComplete()
    {
        // プレイヤーデータの初期化
        InitPlayer();

        // 表示名の更新
        UpdateUserTitleDisplayName();
        //紐付けの解除
        PlayFabAuthService.Instance.UnlinkSilentAuth();
        //ユーザー名パスワード
        PlayFabAuthService.Instance.Authenticate(Authtypes.RegisterPlayFabAccount);
    }
    
}
