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
            Debug.Log("���O����͂��Ă�������");
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
        // �V�K�쐬�������ǂ���
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
    // �\�����̓��̓R���g���[��
    [SerializeField] TMP_InputField inputName;

    #region �v���C���[�\�����̍X�V
    private void UpdateUserTitleDisplayName()
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = inputName.text
        }, result =>
        {
            Debug.Log("�v���C���[���F" + result.DisplayName);
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }

    // �����{�^��
    [SerializeField] Button inputComp;

    public void InputValueChanged()
    {
        inputComp.interactable = IsValidName();
    }

    private bool IsValidName()
    {
        // �\�����́A�R�����ȏ�Q�T�����ȉ�
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
    #region �v���C���[�̏�����
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
                Debug.Log("�v���C���[�̏���������");
                UpdateUserTitleDisplayName();
            }, error => Debug.LogError(error.GenerateErrorReport()));
    }
    #endregion

    // �����{�^���������ꂽ�Ƃ��̏���
    public void InputComplete()
    {
        // �v���C���[�f�[�^�̏�����
        InitPlayer();

        // �\�����̍X�V
        UpdateUserTitleDisplayName();

        LoadScenezero();
    }

    public void ClearDataOnPlayerPrefs()
    {
        PlayFabAuthService.Instance.ClearRememberMe();
        Debug.Log("ClearPlayerpref");
    }
}
