using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

/// <summary>
/// インベントリのサンプル
/// </summary>
public class InventorySample : MonoBehaviour
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
        Debug.Log($"インベントリの情報の取得に成功 : インベントリに入ってるアイテム数 {result.Inventory.Count}個");

        //インベントリに入ってる各アイテムの情報をログで表示
        foreach (ItemInstance item in result.Inventory)
        {
            Debug.Log($"ID : {item.ItemId}, Name : {item.DisplayName}, ItemInstanceId : {item.ItemInstanceId}");
        }
    }

    //インベントリの情報の取得に失敗
    private void OnError(PlayFabError error)
    {
        Debug.LogError($"インベントリの情報の取得に失敗\n{error.GenerateErrorReport()}");
    }
    //=================================================================================
    //消費
    //=================================================================================

    /// <summary>
    /// インベントリのアイテムを消費
    /// </summary>
    public void ConsumeItem()
    {
        //ConsumeItemRequestのインスタンスを生成
        var consumeItemRequest = new ConsumeItemRequest
        {
            ItemInstanceId = "77761CE0432DFCA0", //消費したいアイテムのインスタンスID
            ConsumeCount = 1                   //消費数
        };

        //インベントリのアイテムを消費
        Debug.Log($"インベントリのアイテムを消費開始");
        PlayFabClientAPI.ConsumeItem(consumeItemRequest, OnSuccess, OnError);
    }

    //=================================================================================
    //消費結果
    //=================================================================================

    //インベントリのアイテムの消費に成功
    private void OnSuccess(ConsumeItemResult result)
    {
        Debug.Log($"インベントリのアイテム({result.ItemInstanceId})の消費に成功");
    }

}