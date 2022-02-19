using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDataScript : MonoBehaviour
{
    //　データ削除メソッド
    public void Delete(string dataName)
    {
        if (PlayerPrefs.HasKey(dataName))
        {
            PlayerPrefs.DeleteKey(dataName);
        }
    }
}