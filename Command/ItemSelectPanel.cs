using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectPanel : MonoBehaviour
{
    [SerializeField]
    GameObject itemallpanel1;


    // Start is called before the first frame update
    void Start()
    {
        ItemAllPanel0();
    }
    public void ItemAllPanel0()
    {
        itemallpanel1.SetActive(false);
    }
    public void ItemAllPanel1()
    {
        itemallpanel1.SetActive(true);
    }
}
