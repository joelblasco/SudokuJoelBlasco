using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private SquareController[] squareControllers;
    private void Start()
    {
        squareControllers = new SquareController[9];
        for (int i = 0; i < squareControllers.Length; i++)
        {
            squareControllers[i] = transform.GetChild(i).GetComponent<SquareController>();
            squareControllers[i].unitController = this;
        }
    }
    public void PaintUnit(SquareController squareController)
    {
        foreach (SquareController square in squareControllers)
        {
            square.Paint(square.Equals(squareController)?
                GameManager.instance.UiManager.selectedSquareColor : (square.row == squareController.row || square.column == squareController.column)?
                GameManager.instance.UiManager.selectedAxisColor : GameManager.instance.UiManager.selectedUnitColor);
        }
    }
    public void GetColumn(SquareController square)
    {
        square.transform.GetSiblingIndex();
    }
    public void CheckDigit(int digit)
    {
        foreach (SquareController square in squareControllers)
        {
            if (square.digit == digit && square.isGivenDigit) square.Paint(GameManager.instance.UiManager.mistakeColor);
        }
    }
}
