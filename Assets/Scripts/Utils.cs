using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    private static int GetColumn(int index)
    {
        return Mathf.FloorToInt(index / 3);
    }
    public static Vector2 getSquarePosition(SquareController squareController)
    {
        Vector2 v = new Vector2();
        v.x = GetColumn(squareController.unitSquareIndex)+(GetColumn(squareController.unitIndex)*3);
        v.y = (squareController.unitSquareIndex - 3 * v.x)+(squareController.unitIndex * 3);
        return v;

    }
    [System.Serializable]
    public class InputAction
    {
        public SquareController squareController;
        public int previousDigit;
        public InputAction(SquareController squareController, int previousDigit)
        {
            this.squareController = squareController;
            this.previousDigit = previousDigit;
        }
    }
}
