using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab.Json;
using TMPro;
using PlayFab;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EasyPlayFabLogin : MonoBehaviour
{
    [SerializeField]
    PlayerData playerdata;
    [SerializeField]
    GameObject Playpanel;
    [SerializeField]
    GameObject Inputpanel;

    public void InputPanel()
    {
        Inputpanel.SetActive(true);
        Playpanel.SetActive(false);
    }
    public void PlayPanel()
    {
        Inputpanel.SetActive(false);
        Playpanel.SetActive(true);
    }

    private void Start()
    {
        PlayPanel();
    }

    public void LoadScenezero()
    {
        if (playerdata.ID != "")
        {
            SceneManager.LoadScene("zero");
        }
        else
        {
            Debug.Log("名前を入力してください");
        }
    }
    [SerializeField] GetPlayerCombinedInfoRequestParams InfoRequestParams;

    public void Login()
    {
        PlayFabAuthService.Instance.InfoRequestParams = InfoRequestParams;
        PlayFabAuthService.OnLoginSuccess += PlayFabAuthService_OnLoginSuccess;
        PlayFabAuthService.Instance.Authenticate(Authtypes.Silent);
    }

    private void PlayFabAuthService_OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login Success!");
        // 新規作成したかどうか
        if (result.NewlyCreated)
        {
            InputPanel();
        }
        else
        {
            Debug.Log(result.PlayFabId);
            Debug.Log(result.InfoResultPayload.PlayerProfile.DisplayName);
            Debug.Log(result.InfoResultPayload.UserData["Exp"].Value);
            Debug.Log(result.InfoResultPayload.UserData["Rank"].Value);
            Debug.Log(result.InfoResultPayload.UserData["Quests"].Value);
            LoadScenezero();
        }
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
    #endregion
    [SerializeField] List<UserQuestData> UserQuestDatas;

    [System.Serializable]
    public class UserQuestData
    {
        public int Id;
        public int Score;
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
            {"Quests", PlayFabSimpleJson.SerializeObject(UserQuestDatas) },
        }
        };

        PlayFabClientAPI.UpdateUserData(request
            , result =>
            {
                Debug.Log("プレイヤーの初期化完了");
                UpdateUserTitleDisplayName();
            }, error => Debug.LogError(error.GenerateErrorReport()));
    }
    #endregion

    // 完了ボタンが押されたときの処理
    public void InputComplete()
    {
        // プレイヤーデータの初期化
        InitPlayer();

        // 表示名の更新
        UpdateUserTitleDisplayName();

        LoadScenezero();
    }

    public void ClearDataOnPlayerPrefs()
    {
        PlayFabAuthService.Instance.ClearRememberMe();
        Debug.Log("ClearPlayerpref");
    }
}
