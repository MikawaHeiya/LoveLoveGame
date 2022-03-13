using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float attackDeltaTime;
    public GameObject attack;
    public Vector3 attackPosition;
    public Vector3 attackRotation;
    public bool turnToPlayer;

    private bool playerEntered = false;
    private GameObject player;

    private void Start()
    {
        StartCoroutine(Attack());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
            playerEntered = false;
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackDeltaTime);

            if (playerEntered)
            {
                var atk = Instantiate(attack, attackPosition, 
                    transform.rotation * (turnToPlayer ? 
                    Quaternion.Euler(new Vector3(0, 0, -1f * Vector3.Angle(new Vector3(1f, 0, 0), player.transform.position - transform.position))) : 
                    Quaternion.Euler(attackRotation)));
            }
        }
    }
}