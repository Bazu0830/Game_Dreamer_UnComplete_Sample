using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyEquipPlayerData : MonoBehaviour
{
    [SerializeField]
    GameObject itembutton;
    [SerializeField]
    GameObject iapitembutton;
    [SerializeField]
    public PlayerData playerdata;
    [SerializeField]
    public GameObject playerPrefab;
    [SerializeField]
    public Transform spownpoint;
    private AllyStatus ally;
    private GameObject PlayerIns;
    private string AllyName;
    private AllyStatus allyIns;
    [SerializeField]
    public MenuStart menu;


    //　データをセットする
    public void SetParam(AllyStatus ally)
    {
        this.ally = ally;
    }

    //　装備する
    public void Equip()
    {
        playerdata.ally =ally;
        AllyName = playerdata.ally.characterName;
        playerPrefab = ally.gameObject;
        menu.allyIns = ScriptableObject.Instantiate(Resources.Load(AllyName)) as AllyStatus;
        Destroy(menu.PlayerIns);
        menu.PlayerIns = Instantiate(playerPrefab, spownpoint.position, spownpoint.rotation);
        menu.PlayerIns.tag = "Mine";
    }
}
