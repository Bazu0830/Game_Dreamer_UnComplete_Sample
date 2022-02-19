using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

/// <summary>
/// 仮想通貨のサンプル
/// </summary>
public class CurrenciesSample : MonoBehaviour
{

    //=================================================================================
    //取得
    //=================================================================================

    /// <summary>
    /// インベントリの情報を取得
    /// </summary>
    public void GetUserInventory()
    {
        //GetUserInventoryRequestのインスタンスを生成
        var userInventoryRequest = new GetUserInventoryRequest();

        //インベントリの情報の取得
        Debug.Log($"インベントリの情報の取得開始");
        PlayFabClientAPI.GetUserInventory(userInventoryRequest, OnSuccess, OnError);
    }

    //=================================================================================
    //取得結果
    //=================================================================================

    //インベントリの情報の取得に成功
    private void OnSuccess(GetUserInventoryResult result)
    {
        //result.Inventoryがインベントリの情報
        Debug.Log($"インベントリの情報の取得に成功");

        //所持している仮想通貨の情報をログで表示
        foreach (var virtualCurrency in result.VirtualCurrency)
        {
            Debug.Log($"仮想通貨 {virtualCurrency.Key} : {virtualCurrency.Value}");
        }
    }

    //インベントリの情報の取得に失敗
    private void OnError(PlayFabError error)
    {
        Debug.LogError($"インベントリの情報の取得に失敗\n{error.GenerateErrorReport()}");
    }

    //=================================================================================
    //追加
    //=================================================================================

    /// <summary>
    /// 仮想通貨を追加
    /// </summary>
    public void AddUserVirtualCurrency()
    {
        //AddUserVirtualCurrencyRequestのインスタンスを生成
        var addUserVirtualCurrencyRequest = new AddUserVirtualCurrencyRequest
        {
            Amount = 58,   //追加する金額
            VirtualCurrency = "KM", //仮想通貨のコード
        };

        //仮想通貨の追加
        Debug.Log($"仮想通貨の追加開始");
        PlayFabClientAPI.AddUserVirtualCurrency(addUserVirtualCurrencyRequest, OnSuccess, OnError);
    }

    //=================================================================================
    //追加結果
    //=================================================================================

    //仮想通貨の追加に成功
    private void OnSuccess(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log($"仮想通貨の追加に成功");

        //仮想通貨の情報をログで表示
        Debug.Log($"変更した仮想通貨のコード : {result.VirtualCurrency}");
        Debug.Log($"変更後の残高 : {result.Balance}");
        Debug.Log($"加算額 : {result.BalanceChange}");
    }

    //=================================================================================
    //減額
    //=================================================================================

    /// <summary>
    /// 仮想通貨を減らす
    /// </summary>
    public void SubtractUserVirtualCurrency()
    {
        //AddUserVirtualCurrencyRequestのインスタンスを生成
        var subtractUserVirtualCurrencyRequest = new SubtractUserVirtualCurrencyRequest
        {
            Amount = 58,   //減らす金額
            VirtualCurrency = "KM", //仮想通貨のコード
        };

        //仮想通貨の減額
        Debug.Log($"仮想通貨の減額開始");
        PlayFabClientAPI.SubtractUserVirtualCurrency(subtractUserVirtualCurrencyRequest, OnSuccess, OnError);
    }


}