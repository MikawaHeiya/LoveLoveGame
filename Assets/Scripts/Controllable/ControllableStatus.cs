using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStatus
{
    Stay, Move, Attack, Ex, Guard, Shock, Win
}

public class ControllableStatus : MonoBehaviour
{
    public PlayerStatus status;

    public void SetStatusStay()
    {
        status = PlayerStatus.Stay;
    }

    public void SetStatusMove()
    {
        status = PlayerStatus.Move;
    }

    public void SetStatusAttack()
    {
        status = PlayerStatus.Attack;
    }

    public void SetStatusEx()
    {
        status = PlayerStatus.Ex;
    }

    public void SetStatusGuard()
    {
        status = PlayerStatus.Guard;
    }

    public void SetStatusShock()
    {
        status = PlayerStatus.Shock;
    }

    public void SetstatusWin()
    {
        status = PlayerStatus.Win;
    }
}
