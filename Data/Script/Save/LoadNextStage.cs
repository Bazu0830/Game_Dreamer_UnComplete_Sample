using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextStage : MonoBehaviour
{
    public void OnTriggerEnter(Collider player)
    {
        SceneManager.sceneLoaded += LoadSceneLoaded;
        SceneManager.LoadScene("");
    }

    public void LoadSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // イベントから削除
        SceneManager.sceneLoaded -= LoadSceneLoaded;
    }
}
