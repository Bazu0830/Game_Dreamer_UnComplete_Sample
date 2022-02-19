using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    [SerializeField]
    private MenuOpen menuopen;

    public void IsPlayerDead()
    {
        menuopen.DeadPanel();
    }
    public void ToGame()
    {
        menuopen.ToGame();
    }
}
