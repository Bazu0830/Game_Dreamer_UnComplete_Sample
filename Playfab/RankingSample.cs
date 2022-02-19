using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ランキングのサンプル
/// </summary>
public class RankingSample : MonoBehaviour
{

    [SerializeField]
    private Text _nameText = default;

    //=================================================================================
    //ユーザ名
    //=================================================================================

    /// <summary>
    /// ユーザ名を更新する
    /// </summary>
    public void UpdateUserName()
    {
        //ユーザ名を指定して、UpdateUserTitleDisplayNameRequestのインスタンスを生成
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = _nameText.text
        };

        //ユーザ名の更新
        Debug.Log($"ユーザ名の更新開始");
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateUserNameSuccess, OnUpdateUserNameFailure);
    }

    //ユーザ名の更新成功
    private void OnUpdateUserNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        //result.DisplayNameに更新した後のユーザ名が入ってる
        Debug.Log($"ユーザ名の更新が成功しました : {result.DisplayName}");
    }

    //ユーザ名の更新失敗
    private void OnUpdateUserNameFailure(PlayFabError error)
    {
        Debug.LogError($"ユーザ名の更新に失敗しました\n{error.GenerateErrorReport()}");
    }


    private void Update()
    {
        var statistics = new List<StatisticUpdate> { new StatisticUpdate { StatisticName = "Rank", Value = 1 } };
        var request = new UpdatePlayerStatisticsRequest { Statistics = statistics };

        PlayFabClientAPI.UpdatePlayerStatistics(
            request,
            response => Debug.Log("成功したときの処理"),
            error => Debug.LogError("失敗したときの処理"));
    }








    [SerializeField]
    private Text _scoreText = default;

    //=================================================================================
    //スコア
    //=================================================================================

    /// <summary>
    /// スコア(統計情報)を更新する
    /// </summary>
    public void UpdatePlayerStatistics()
    {
        //UpdatePlayerStatisticsRequestのインスタンスを生成
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>{
        new StatisticUpdate{
          StatisticName = "ランキングサンプル",   //ランキング名(統計情報名)
          Value = int.Parse( _scoreText.text), //スコア(int)
        }
      }
        };

        //ユーザ名の更新
        Debug.Log($"スコア(統計情報)の更新開始");
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdatePlayerStatisticsSuccess, OnUpdatePlayerStatisticsFailure);
    }

    //スコア(統計情報)の更新成功
    private void OnUpdatePlayerStatisticsSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log($"スコア(統計情報)の更新が成功しました");
    }

    //スコア(統計情報)の更新失敗
    private void OnUpdatePlayerStatisticsFailure(PlayFabError error)
    {
        Debug.LogError($"スコア(統計情報)更新に失敗しました\n{error.GenerateErrorReport()}");
    }



    [SerializeField]
    private Text _rankingText = default;

    //=================================================================================
    //ランキング取得
    //=================================================================================

    /// <summary>
    /// ランキング(リーダーボード)を取得
    /// </summary>
    public void GetLeaderboard()
    {
        //GetLeaderboardRequestのインスタンスを生成
        var request = new GetLeaderboardRequest
        {
            StatisticName = "ランキングサンプル", //ランキング名(統計情報名)
            StartPosition = 0,                 //何位以降のランキングを取得するか
            MaxResultsCount = 3                  //ランキングデータを何件取得するか(最大100)
        };

        //ランキング(リーダーボード)を取得
        Debug.Log($"ランキング(リーダーボード)の取得開始");
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, OnGetLeaderboardFailure);
    }

    //ランキング(リーダーボード)の取得成功
    private void OnGetLeaderboardSuccess(GetLeaderboardResult result)
    {
        Debug.Log($"ランキング(リーダーボード)の取得に成功しました");

        //result.Leaderboardに各順位の情報(PlayerLeaderboardEntry)が入っている
        _rankingText.text = "";
        foreach (var entry in result.Leaderboard)
        {
            _rankingText.text += $"\n順位 : {entry.Position}, スコア : {entry.StatValue}, 名前 : {entry.DisplayName}, ID : {entry.PlayFabId}";
        }
    }

    //ランキング(リーダーボード)の取得失敗
    private void OnGetLeaderboardFailure(PlayFabError error)
    {
        Debug.LogError($"ランキング(リーダーボード)の取得に失敗しました\n{error.GenerateErrorReport()}");
    }

    //=================================================================================
    //ランキング取得
    //=================================================================================

    /// <summary>
    /// 自分の順位周辺のランキング(リーダーボード)を取得
    /// </summary>
    public void GetLeaderboardAroundPlayer()
    {
        //GetLeaderboardAroundPlayerRequestのインスタンスを生成
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "ランキングサンプル", //ランキング名(統計情報名)
            MaxResultsCount = 3                  //自分を含め前後何件取得するか
        };

        //自分の順位周辺のランキング(リーダーボード)を取得
        Debug.Log($"自分の順位周辺のランキング(リーダーボード)の取得開始");
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetLeaderboardAroundPlayerSuccess, OnGetLeaderboardAroundPlayerFailure);
    }

    //自分の順位周辺のランキング(リーダーボード)の取得成功
    private void OnGetLeaderboardAroundPlayerSuccess(GetLeaderboardAroundPlayerResult result)
    {
        Debug.Log($"自分の順位周辺のランキング(リーダーボード)の取得に成功しました");

        //result.Leaderboardに各順位の情報(PlayerLeaderboardEntry)が入っている
        _rankingText.text = "";
        foreach (var entry in result.Leaderboard)
        {
            _rankingText.text += $"\n順位 : {entry.Position}, スコア : {entry.StatValue}, 名前 : {entry.DisplayName}, ID : {entry.PlayFabId}";
        }
    }

    //自分の順位周辺のランキング(リーダーボード)の取得失敗
    private void OnGetLeaderboardAroundPlayerFailure(PlayFabError error)
    {
        Debug.LogError($"自分の順位周辺のランキング(リーダーボード)の取得に失敗しました\n{error.GenerateErrorReport()}");
    }

}