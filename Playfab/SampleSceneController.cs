using PlayFab;
using PlayFab.ClientModels;
using PlayFab.MultiplayerModels;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SampleSceneController : MonoBehaviour
{
    // 処理中のメッセージは雑に全部これに表示します。
    [SerializeField] Text textBox;

    public void Start()
    {
        textBox.text = "ログイン中...\n";

        // PlayFabにいつも通りログインします。
        var request = new LoginWithCustomIDRequest { CustomId = "MyCustomId", CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnFailure);

        void OnLoginSuccess(LoginResult result)
        {
            textBox.text += "ログインしました！\n\n";

            // ログインできたので続けてマッチングの処理を呼びます。
            Matchmaking();
        }
    }

    private void Matchmaking()
    {
        textBox.text += "マッチメイキングチケットをキューに積みます...\n";

        // マッチングさせるプレイヤーの情報を作ります。
        var matchmakingPlayer = new MatchmakingPlayer
        {
            // Entityは下記のコードで決め打ちで大丈夫です。
            Entity = new PlayFab.MultiplayerModels.EntityKey
            {
                Id = PlayFabSettings.staticPlayer.EntityId,
                Type = PlayFabSettings.staticPlayer.EntityType
            },
            // これ以下を追記
            Attributes = new MatchmakingPlayerAttributes
            {
                // このプレイヤーは Rate 900～1100 のプレイヤーとしかマッチングしない
                DataObject = new { Rate = 1000 }
            }
        };

        var request = new CreateMatchmakingTicketRequest
        {
            // 先程作っておいたプレイヤー情報です。
            Creator = matchmakingPlayer,
            // マッチングできるまで待機する秒数を指定します。最大600秒です。
            GiveUpAfterSeconds = 30,
            // GameManagerで作ったキューの名前を指定します。
            QueueName = "1v1BattleOnRule"
        };

        PlayFabMultiplayerAPI.CreateMatchmakingTicket(request, OnCreateMatchmakingTicketSuccess, OnFailure);

        void OnCreateMatchmakingTicketSuccess(CreateMatchmakingTicketResult result)
        {
            textBox.text += "マッチメイキングチケットをキューに積みました！\n\n";

            // キューに積んだチケットの状態をマッチングするかタイムアウトするまでポーリングします。
            var getMatchmakingTicketRequest = new GetMatchmakingTicketRequest
            {
                TicketId = result.TicketId,
                QueueName = request.QueueName
            };

            StartCoroutine(Polling(getMatchmakingTicketRequest));
        }
    }

    IEnumerator Polling(GetMatchmakingTicketRequest request)
    {
        // ポーリングは1分間に10回まで許可されているので、6秒間隔で実行するのがおすすめです。
        var seconds = 6f;
        var MatchedOrCanceled = false;

        while (true)
        {
            if (MatchedOrCanceled)
            {
                yield break;
            }

            PlayFabMultiplayerAPI.GetMatchmakingTicket(request, OnGetMatchmakingTicketSuccess, OnFailure);
            yield return new WaitForSeconds(seconds);
        }

        void OnGetMatchmakingTicketSuccess(GetMatchmakingTicketResult result)
        {
            switch (result.Status)
            {
                case "Matched":
                    MatchedOrCanceled = true;
                    textBox.text += $"対戦相手が見つかりました！\n\nMatchIDは {result.MatchId} です！";
                    return;

                case "Canceled":
                    MatchedOrCanceled = true;
                    textBox.text += "対戦相手が見つからないのでキャンセルしました...";
                    return;

                default:
                    textBox.text += "対戦相手が見つかるまで待機します...\n";
                    return;
            }
        }
    }

    void OnFailure(PlayFabError error)
    {
        Debug.Log($"{error.ErrorMessage}");
    }
}