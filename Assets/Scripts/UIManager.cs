using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] public Color selectedSquareColor, selectedUnitColor, selectedAxisColor, nonSelectedColor, nonSelectedGivenColor, mistakeColor, normalTextColor, editTextColor;
    [SerializeField] private TMP_Text timeText;
    TimeSpan totalTime;

    public void SetTotalTimeText(float value)
    {
        totalTime = TimeSpan.FromSeconds(value);
        timeText.text = totalTime.ToString("hh':'mm':'ss");
    }

}
