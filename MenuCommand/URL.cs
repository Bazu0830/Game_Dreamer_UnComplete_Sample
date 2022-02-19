using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class URL : MonoBehaviour
{
    [SerializeField]
    public string uRL;
    public void onClick()
    {
        Application.OpenURL(uRL);
    }
}