using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoginBonus : MonoBehaviour
{
    DateTime datatime;
    int todayInt = 0;

    // Start is called before the first frame update
    void Start()
    {
        todayInt = datatime.Year * 10000 + datatime.Month * 100 + datatime.Day;
    }

    public void Login()
    {

    } 
}
