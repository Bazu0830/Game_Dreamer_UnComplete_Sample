using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayFabLogin : MonoBehaviour
{
    //アカウントを作成するか
    private bool _shouldCreateAccount;

    //ログイン時に使うID
    private string _customID;

    public void Start()
    {
        _customID = LoadCustomID();
        var request = new LoginWithCustomIDRequest { CustomId = _customID, CreateAccount = _shouldCreateAccount };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        PlayFabClientAPI.LoginWithCustomID(request, (result =>
        {
            GetTitleDataToVersion();
        }), error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }

    void GetTitleDataToVersion()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), result =>
        {
#if UNITY_ANDROID
            if (result.Data.ContainsKey("AndroidVersion"))
            {
                // タイトルデータのバージョン
                string serverVersion = result.Data["AndroidVersion"].ToString();
                // ローカルのバージョン
                string lovalVersion = Application.version;
                // 文字列の大小を比較
                int compareResult = serverVersion.CompareTo(lovalVersion);
                // サーバのバージョンの方が大きかったらストアに飛ばす
                if (compareResult == 1)
                {
                    Debug.Log("アプリの更新が必要です");
                    Application.OpenURL("アプリのストアページURL");
                }
                else
                {
                    Debug.Log("最新バージョンです");
                }

            }
#else
             Debug.Log("iOSも同じように書く");
#endif
        },
            error => {
                Debug.Log(error.GenerateErrorReport());
            }
        );
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");

        GetTitleData(); // ここを追加
                        //アカウントを作成しようとしたのに、IDが既に使われていて、出来なかった場合
        if (_shouldCreateAccount && !result.NewlyCreated)
        {
            Debug.LogWarning($"CustomId : {_customID} は既に使われています。");
            Start();//ログインしなおし
            return;
        }

        //アカウント作成時にIDを保存
        if (result.NewlyCreated)
        {
            SaveCustomID();
        }
        Debug.Log($"PlayFabのログインに成功\nPlayFabId : {result.PlayFabId}, CustomId : {_customID}\nアカウントを作成したか : {result.NewlyCreated}");

    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
        Debug.LogError($"PlayFabのログインに失敗\n{error.GenerateErrorReport()}");
    }

    public static void GetUserData()
    {
        var request = new GetUserDataRequest();
        PlayFabClientAPI.GetUserData(request, OnSuccess, OnError);

        void OnSuccess(GetUserDataResult result)
        {
            Debug.Log("GetUserData: Success!");
            Debug.Log($"My name is {result.Data["Name"].Value}");
        }

        void OnError(PlayFabError error)
        {
            Debug.Log("GetUserData: Fail...");
            Debug.Log(error.GenerateErrorReport());
        }
    }
    public static void UpdateUserData()
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>
            {
                { "Name", "Minami" },
                { "Job", "TeaAdvisor" }  // Job を新しく追加してみる
            }
            /*      Data = new Dictionary<string, string>
                  {
                      { "Name", "Minami" },
                      { "Job", null}  // Job はなかったことに
                  }
             */
        };

        PlayFabClientAPI.UpdateUserData(request, OnSuccess, OnError);

        void OnSuccess(UpdateUserDataResult result)
        {
            Debug.Log("UpdateUserData: Success!");
            GetUserData();
        }

        void OnError(PlayFabError error)
        {
            Debug.Log("UpdateUserData: Fail...");
            Debug.Log(error.GenerateErrorReport());
        }

    }
    public static void GetTitleData()
    {
        var request = new GetTitleDataRequest();
        PlayFabClientAPI.GetTitleData(request, OnSuccess, OnError);

        void OnSuccess(GetTitleDataResult result)
        {
            Debug.Log("GetTitleData: Success!");

            var loginMessage = result.Data["LoginMessage"];
            Debug.Log(loginMessage);

            var gachaMaster = Utf8Json.JsonSerializer.Deserialize<GachaMaster[]>(result.Data["GachaMaster"]);
            foreach (var master in gachaMaster)
            {
                Debug.Log(master.Name);
            }
        }

        void OnError(PlayFabError error)
        {
            Debug.Log("GetTitleData: Fail...");
            Debug.Log(error.GenerateErrorReport());
        }
    }

    public class GachaMaster
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public int Rate { get; set; }
    }


    //=================================================================================
    //カスタムIDの取得
    //=================================================================================

    //IDを保存する時のKEY
    private static readonly string CUSTOM_ID_SAVE_KEY = "CUSTOM_ID_SAVE_KEY";

    //IDを取得
    private string LoadCustomID()
    {
        //IDを取得
        string id = PlayerPrefs.GetString(CUSTOM_ID_SAVE_KEY);

        //保存されていなければ新規生成
        _shouldCreateAccount = string.IsNullOrEmpty(id);
        return _shouldCreateAccount ? GenerateCustomID() : id;
    }

    //IDの保存
    private void SaveCustomID()
    {
        PlayerPrefs.SetString(CUSTOM_ID_SAVE_KEY, _customID);
    }

    //=================================================================================
    //カスタムIDの生成
    //=================================================================================

    //IDに使用する文字
    private static readonly string ID_CHARACTERS = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    //IDを生成する
    private string GenerateCustomID()
    {
        int idLength = 32;//IDの長さ
        StringBuilder stringBuilder = new StringBuilder(idLength);
        var random = new System.Random();

        //ランダムにIDを生成
        for (int i = 0; i < idLength; i++)
        {
            stringBuilder.Append(ID_CHARACTERS[random.Next(ID_CHARACTERS.Length)]);
        }

        return stringBuilder.ToString();
    }

}