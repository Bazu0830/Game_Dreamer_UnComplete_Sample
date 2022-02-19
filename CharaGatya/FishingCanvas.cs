using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingCanvas : MonoBehaviour
{
    [SerializeField]
    GameObject FishingButton;
    [SerializeField]
    GameObject FishingPanel;
    private bool ismine = false;

    public void PanelOpen()
    {
        ismine = true;
        FishingButton.SetActive(true);
    }
    
    public void PanelClose()
    {
        ismine = false;
        FishingButton.SetActive(false);
        FishingPanel.SetActive(false);
    }

    [SerializeField] Slider green;
    [SerializeField] Slider yellow;
    [SerializeField] Slider red;
    [SerializeField] Slider black;
    [SerializeField] Slider white;
    public float fishtime = 0f;
    public int fishcount = 0;

    private void Start()
    {
        SliderOn();
    }
    private void LateUpdate()
    {
        if (ismine == false)
        {
            return;
        }

        white.value += 0.01f;
        if (white.value >= 1f)
        {
            white.value = 0f;
        }
    }

    public void Fish()
    {
        fishtime = white.value;
        if (fishtime > green.value)
        {
            Debug.Log("Miss");
        }
        else if (fishtime > yellow.value)
        {
            Debug.Log("Great");
        }
        else if (fishtime > red.value)
        {
            Debug.Log("Good");
        }
        else if (fishtime > black.value)
        {
            Debug.Log("Bad");
        }
        else
        {
            Debug.Log("Miss");
        }
        FishingPanel.SetActive(false);
        FishingButton.SetActive(true);
    }

    void SliderOn()
    {
        float i = UnityEngine.Random.Range(0.5f, 1.0f);
        green.value = i;
        yellow.value = i - 0.05f;
        red.value = i - 0.2f;
        black.value = i - 0.5f;
        white.value = 0f;
    }

    public void FishStart()
    {
        FishingButton.SetActive(false);
        StartCoroutine(DelayMethod(1.0f, () =>
        {
            FishingPanel.SetActive(true);
            SliderOn();
        }));
    }
    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}
