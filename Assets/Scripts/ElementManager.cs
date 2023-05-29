using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElementManager : MonoBehaviour
{
    private SquareController selectedSquare;
    private Sudoku playingSudoku;
    [SerializeField] private List<Utils.InputAction> inputActionsHistory = new List<Utils.InputAction>();
    [SerializeField] private SquareController[] squareControllers;
    private bool notesModeEnabled = false;
    
    
    private void LoadSudoku(int level)
    {
        string levelFile = "sudoku_" + level;
        TextAsset sudokuFile = (TextAsset)Resources.Load(levelFile);
        Debug.Log(sudokuFile.text);
        playingSudoku = JsonUtility.FromJson<Sudoku>(sudokuFile.text);
        for (int i = 0; i < 81; i++)
        {
            squareControllers[i].SetInitialDigit(playingSudoku.sudokuPuzzle[i]);
        }

    }
    public void Initialize()
    {
        inputActionsHistory.Clear();
        foreach (SquareController square in squareControllers)
        {
            square.Paint(GameManager.instance.UiManager.nonSelectedColor);
        }
        LoadSudoku(UnityEngine.Random.Range(1,3));
    }
    public void SelectSquare(SquareController squareController)
    {
        if (!GameManager.instance.isPlaying()) return;
        if (squareController == null)
        {
            foreach (SquareController square in squareControllers)
            {
                square.Paint(square.isGivenDigit ? GameManager.instance.UiManager.nonSelectedGivenColor : GameManager.instance.UiManager.nonSelectedColor, 0.5f);
            }
        }
        else
        {
            selectedSquare = squareController;

            foreach (SquareController square in squareControllers)
            {
                if (square.row == selectedSquare.row || square.column == selectedSquare.column) square.Paint(GameManager.instance.UiManager.selectedAxisColor);
                else { square.Paint(square.isGivenDigit ? GameManager.instance.UiManager.nonSelectedGivenColor : GameManager.instance.UiManager.nonSelectedColor, 0.5f); }
            }
        }
    }
    public void InputDigit(int digit)
    {
        if (!GameManager.instance.isPlaying() || selectedSquare.digit == digit || selectedSquare == null) return;
        if (notesModeEnabled)
        {
            selectedSquare.SetNotesDigit(digit);
        }
        else
        {
            inputActionsHistory.Add(new Utils.InputAction(selectedSquare, selectedSquare.digit));
            selectedSquare.SetDefinitiveDigit(digit);
            selectedSquare.unitController.CheckDigit(digit);
            foreach (SquareController square in squareControllers)
            {
                if ((square.row == selectedSquare.row || square.column == selectedSquare.column) && square.digit == digit && !square.Equals(selectedSquare)) square.Paint(GameManager.instance.UiManager.mistakeColor);
            }
        }
    }
    public void UndoInputs()
    {
        if (!GameManager.instance.isPlaying()) return;
        if (inputActionsHistory.Count > 0)
        {
            Debug.Log(inputActionsHistory.Count);
            selectedSquare = inputActionsHistory[inputActionsHistory.Count - 1].squareController;
            selectedSquare.SetDefinitiveDigit(inputActionsHistory[inputActionsHistory.Count - 1].previousDigit);
            inputActionsHistory.RemoveAt(inputActionsHistory.Count - 1);
            SelectSquare(selectedSquare);
        }
    }
    public void DeleteInput()
    {
        if (selectedSquare == null || selectedSquare.isGivenDigit || !GameManager.instance.isPlaying()) return;
        inputActionsHistory.Add(new Utils.InputAction(selectedSquare, selectedSquare.digit));
        selectedSquare.SetDefinitiveDigit(0);
    }
    public void ChangeNotesMode()
    {
        notesModeEnabled = !notesModeEnabled;
    }
}
