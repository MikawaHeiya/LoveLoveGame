using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableBehave : MonoBehaviour
{
    public GameObject attackGameObject;
    public GameObject exGameObject;
    public CameraMove mainCamera;

    public event System.Action GPSucceed;

    private Animator animator;
    private ControllableStatus status;
    private ControllableInfo info;

    private int guardFrameCount = 0;

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

    private IEnumerator ResetGPBonus(float basicAttack)
    {
        yield return new WaitForSeconds(info.gpBonusExistTime);
        info.basicAttack = basicAttack;
    }

    public void Move(Vector3 forward)
    {
        status.status = PlayerStatus.Move;
        SetTrigger("Move");
        transform.rotation = Quaternion.Euler(0f, forward.x > 0 ? 0f : 180f, 0f);
        var move = Time.deltaTime * info.moveSpeed * forward;
        transform.position += move;
        if ((forward.x > 0 && (transform.position.x - mainCamera.transform.position.x >= 6f))||
            (forward.x < 0 && (transform.position.x - mainCamera.transform.position.x <= -6f)))
        {
            mainCamera.Move(move);
        }
    }

    public void Attack()
    {
        if (info.Mana >= info.attackManaCost)
        {
            info.Mana -= info.attackManaCost;
            status.status = PlayerStatus.Attack;
            SetTrigger("Attack");

            var atk = Instantiate(attackGameObject, transform).GetComponent<AttackInfo>();
            atk.damage = info.basicAttack * info.attackDamageOffset;
        }
    }

    public void Guard()
    {
        if (info.Mana >= info.guardManaCost)
        {
            info.Mana -= info.guardManaCost;
            status.status = PlayerStatus.Guard;
            SetTrigger("Guard");
        }
    }

    public void Ex()
    {
        if (info.Mana >= info.exManaCost)
        {
            info.Mana -= info.exManaCost;
            status.status = PlayerStatus.Ex;
            SetTrigger("Ex");
        }
    }

    public void Shock()
    {
        status.status = PlayerStatus.Shock;
        SetTrigger("Shock");
    }

    public void ExInstantiate()
    {
        var exAtk = Instantiate(exGameObject, transform).GetComponent<AttackInfo>();
        exAtk.damage = info.basicAttack * info.exDamageOffset;

        status.SetStatusStay();
    }

    public void RecieveAttack(AttackInfo attack)
    {
        if (status.status == PlayerStatus.Shock)
        {
            return;
        }

        if (status.status == PlayerStatus.Guard)
        {
            if (guardFrameCount <= info.gpFrameCount)
            {
                OnGPSucceed();
            }
        }
        else
        {
            info.Health -= attack.damage;
            Shock();

            if (attack.destroyAfterRecieved)
            {
                Destroy(attack.gameObject);
            }
        }
    }

    public void OnGPSucceed()
    {
        StartCoroutine(ResetGPBonus(info.basicAttack));
        info.basicAttack *= info.gpBonus;
        info.Mana += info.guardManaCost;
        GPSucceed?.Invoke();
    }

    private void Start()
    {
        status = GetComponent<ControllableStatus>();
        info = GetComponent<ControllableInfo>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (status.status == PlayerStatus.Guard)
        {
            ++guardFrameCount;
        }
        else
        {
            guardFrameCount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            RecieveAttack(collision.gameObject.GetComponent<AttackInfo>());
        }
        else if (collision.CompareTag("Win"))
        {
            status.status = PlayerStatus.Win;
            SetTrigger("Win");
        }
    }
}
