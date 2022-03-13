using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType
{
    LeftMove, RightMove, UpMove, DownMove, Attack, Guard, Ex, None
}

public static class InputUtil
{
    public static bool GetInputLeftMove()
    {
        return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
    }

    public static bool GetInputRightMove()
    {
        return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
    }

    public static bool GetInputUpMove()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
    }

    public static bool GetInputDownMove()
    {
        return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
    }

    public static bool GetInputAttack()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    public static bool GetInputGuard()
    {
        return Input.GetKeyDown(KeyCode.Mouse1);
    }

    public static bool GetInputEx()
    {
        return Input.GetKeyDown(KeyCode.Q);
    }
}
