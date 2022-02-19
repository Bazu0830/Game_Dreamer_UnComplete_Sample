using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateAllyStatus : MonoBehaviour
{
    [SerializeField]
    private AllyStatus ally;
    [SerializeField]
    private AllyStatus allyIns;

    public void CreateIns()
    {
        allyIns = ScriptableObject.Instantiate(Resources.Load(ally.characterName)) as AllyStatus;
    }
}
