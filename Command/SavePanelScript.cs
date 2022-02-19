using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePanelScript : MonoBehaviour
{
    [SerializeField]
    GameObject savepanel;
    [SerializeField]
    GameObject loadpanel;
    [SerializeField]
    GameObject returnzero;

    public void OnSavePanel()
    {
        savepanel.SetActive(true);
        loadpanel.SetActive(false);
        returnzero.SetActive(false);
    }
    public void OnLoadPanel()
    {
        savepanel.SetActive(false);
        loadpanel.SetActive(true);
        returnzero.SetActive(false);
    }
    public void OnReturnZero()
    {
        savepanel.SetActive(false);
        loadpanel.SetActive(false);
        returnzero.SetActive(true);
    }
}
