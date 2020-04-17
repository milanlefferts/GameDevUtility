using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required when using Event data.
using TMPro;

public class Timer : MonoBehaviour
{
    public float MaxTime;
    public float ActiveTime;
    public bool timerActive;

    public Image timerImage;

    public void SetTimer(float totalTime)
    {
        MaxTime = totalTime;
        ActiveTime = 0f;
        timerImage.fillAmount = 1f;
        timerActive = true;
    }

    public void Update()
    {
        if (!timerActive) return;

        ActiveTime += Time.deltaTime;
        var percent = ActiveTime / MaxTime;
        timerImage.fillAmount = Mathf.Lerp(1f, 0f, percent);

        if (ActiveTime > MaxTime && timerActive == true)
        {
            timerActive = false;

            // End Timer
        }


    }

    public void StopTimer()
    {
        timerActive = false;
        ActiveTime = 0f;
    }


}