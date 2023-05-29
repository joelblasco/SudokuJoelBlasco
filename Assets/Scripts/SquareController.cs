using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SquareController : MonoBehaviour
{
    [System.NonSerialized] public UnitController unitController;
    private TMP_Text digitText;
    private Image squareBackground;
    public int unitSquareIndex, unitIndex;
    public int digit;
    public int column, row;
    
    public bool isGivenDigit = false;
    public bool isEditDigit = false;

    private void Awake()
    {
        digitText = GetComponentInChildren<TMP_Text>();
        squareBackground = GetComponent<Image>();
    }
    private void Start()
    {
        unitSquareIndex = transform.GetSiblingIndex();
        unitIndex = transform.parent.GetSiblingIndex();
        Vector2 squarePos = Utils.getSquarePosition(this);
        column = (int)squarePos.x;
        row = (int)squarePos.y;
    }
    public void SelectSquare()
    {
        GameManager.instance.ElementManager.SelectSquare(this);
        unitController.PaintUnit(this);
    }
    public void Paint(Color value, float time = 2f)
    {
        LeanTween.cancel(gameObject);
        LeanTween.value(gameObject,LTR_Paint,squareBackground.color, value,time).setEaseOutBack();
    }
    void LTR_Paint(Color value)
    {
        squareBackground.color = value;
    }
    public void SetInitialDigit(int digit)
    {
        this.digit = digit;
        if (digit == 0)
        {
            isGivenDigit = false;
            digitText.text = " ";
        }
        else
        {
            isGivenDigit = true;
            digitText.text = "" + digit;
            Paint(GameManager.instance.UiManager.nonSelectedGivenColor);
        }

    }
    public void SetDefinitiveDigit(int digit)
    {
        this.digit = digit;
        digitText.color = GameManager.instance.UiManager.normalTextColor;
        digitText.enableAutoSizing = false;
        digitText.fontSize = 115;
        digitText.text = "" + (digit == 0 ? "" : digit);
        digitText.transform.localScale = Vector2.zero;
        LeanTween.scale(digitText.gameObject, Vector2.one, 1.25f).setEaseOutElastic();
    }
    public void SetNotesDigit(int digit)
    {
        this.digit = 0;
        digitText.color = GameManager.instance.UiManager.editTextColor;
        digitText.enableAutoSizing = true;
        if (digit == 0)
        {
            digitText.text = "";
        }
        else
        {
            digitText.text += " " + digit;
        }
    }
    
}

