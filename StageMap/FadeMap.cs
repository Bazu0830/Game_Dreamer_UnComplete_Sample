using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMap : MonoBehaviour
{

    private void Start()
    {
        FadeIn(this.gameObject,1,1);
    }

    public void FadeIn(GameObject _obj, float _fadeTime, float _delayTime)
    {
        iTween.FadeTo(_obj, iTween.Hash("alpha", 1, "time", _fadeTime, "delay", _delayTime));
    }
}
