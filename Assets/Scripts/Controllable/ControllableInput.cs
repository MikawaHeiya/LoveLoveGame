using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableInput : MonoBehaviour
{
    private ControllableStatus status;
    private ControllableBehave behave;
    private Animator animator;

    private InputType preInput = InputType.None;

    private void Start()
    {
        status = GetComponent<ControllableStatus>();
        behave = GetComponent<ControllableBehave>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (status.status != PlayerStatus.Ex && InputUtil.GetInputEx())
        {
            preInput = InputType.None;
            Ex();
            return;
        }

        if (status.status == PlayerStatus.Stay || status.status == PlayerStatus.Move)
        {
            if (preInput != InputType.None)
            {
                switch (preInput)
                {
                    case InputType.Attack:
                        preInput = InputType.None;
                        Attack();
                        return;
                    case InputType.Guard:
                        preInput = InputType.None;
                        Guard();
                        return;
                    default:
                        preInput = InputType.None;
                        break;
                }
            }

            if (InputUtil.GetInputAttack())
            {
                Attack();
            }
            else if (InputUtil.GetInputGuard())
            {
                Guard();
            }
            else
            {
                var forward = Vector3.zero;
                if (InputUtil.GetInputUpMove())
                {
                    forward.y += 1;
                }
                if (InputUtil.GetInputDownMove())
                {
                    forward.y -= 1;
                }
                if (InputUtil.GetInputLeftMove())
                {
                    forward.x -= 1;
                }
                if (InputUtil.GetInputRightMove())
                {
                    forward.x += 1;
                }

                if (forward != Vector3.zero)
                {
                    Move(forward);
                }
                else
                {
                    status.status = PlayerStatus.Stay;
                    SetTrigger("Stay");
                }
            }
        }
        else
        {
            if (InputUtil.GetInputAttack())
            {
                preInput = InputType.Attack;
            }
            else if (InputUtil.GetInputGuard())
            {
                preInput = InputType.Guard;
            }
        }
    }

    private void SetTrigger(string name)
    {
        animator.ResetTrigger("Stay");
        animator.ResetTrigger("Move");
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Ex");
        animator.ResetTrigger("Guard");
        animator.ResetTrigger("Shock");
        animator.ResetTrigger("Win");

        animator.SetTrigger(name);
    }

    private void Move(Vector3 forward)
    {
        behave.Move(forward);
    }

    private void Attack()
    {
        behave.Attack();
    }

    private void Guard()
    {
        behave.Guard();
    }

    private void Ex()
    {
        behave.Ex();
    }
}
