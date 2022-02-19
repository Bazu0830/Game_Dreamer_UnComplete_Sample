using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoOpen : MonoBehaviour
{
    [SerializeField] GameObject SkillCheckPanel;
    [SerializeField] GameObject CommandPanel;
    [SerializeField] GameObject ItemSelectPanel;
    [SerializeField] GameObject SavePanel;
    [SerializeField] GameObject GameStartPanel;

    // Start is called before the first frame update
    void Start()
    {
        GameStartWait();
    }
    public void SkillCheckPanelON()
    {
        SkillCheckPanel.SetActive(true);
        CommandPanel.SetActive(true);
        ItemSelectPanel.SetActive(false);
        SavePanel.SetActive(false);
        GameStartPanel.SetActive(false);
    }
    public void ItemSelectPanelON()
    {
        SkillCheckPanel.SetActive(false);
        CommandPanel.SetActive(true);
        ItemSelectPanel.SetActive(true);
        SavePanel.SetActive(false);
        GameStartPanel.SetActive(false);
    }
    public void SavePanelON()
    {
        SkillCheckPanel.SetActive(false);
        CommandPanel.SetActive(true);
        ItemSelectPanel.SetActive(false);
        SavePanel.SetActive(true);
        GameStartPanel.SetActive(false);
    }
    public void GameStartWait()
    {
        SkillCheckPanel.SetActive(false);
        CommandPanel.SetActive(false);
        ItemSelectPanel.SetActive(false);
        SavePanel.SetActive(false);
        GameStartPanel.SetActive(true);
    }
}
