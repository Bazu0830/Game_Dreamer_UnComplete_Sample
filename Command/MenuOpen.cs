using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuOpen : MonoBehaviour
{
    [SerializeField]
    GameObject menupanel;
    [SerializeField]
    GameObject gamepanel;
    [SerializeField]
    GameObject deadpanel;
    [SerializeField]
    GameObject talkpanel;
    [SerializeField]
    GameObject Clearpanel;
    [SerializeField]
    public Text Text;

    private void Start()
    {
        OpenMenu();
    }
    public void ToGame()
    {
        menupanel.SetActive(false);
        gamepanel.SetActive(true);
        deadpanel.SetActive(false);
        talkpanel.SetActive(false);
        Clearpanel.SetActive(false);
    }
    public void OpenMenu()
    {
        menupanel.SetActive(true);
        gamepanel.SetActive(false);
        deadpanel.SetActive(false);
        talkpanel.SetActive(false);
        Clearpanel.SetActive(false);
    }
    public void DeadPanel()
    {
        menupanel.SetActive(false);
        gamepanel.SetActive(true);
        deadpanel.SetActive(true);
        talkpanel.SetActive(false);
        Clearpanel.SetActive(false);
    }
    public void TalkPanel()
    {
        menupanel.SetActive(false);
        gamepanel.SetActive(true);
        deadpanel.SetActive(false);
        talkpanel.SetActive(true);
        Clearpanel.SetActive(false);
    }
    public void ClearPanel()
    {
        menupanel.SetActive(false);
        gamepanel.SetActive(true);
        deadpanel.SetActive(false);
        talkpanel.SetActive(false);
        Clearpanel.SetActive(true);
    }

    [SerializeField] GameObject ChatPanel;
    public void ChatOpen()
    {
        if (ChatPanel.activeInHierarchy==false)
        {
            ChatPanel.SetActive(true);
        }
        else
        {
            ChatPanel.SetActive(false);
        }
    }
}
