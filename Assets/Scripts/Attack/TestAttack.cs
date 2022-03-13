using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 forward;

    private void FixedUpdate()
    {
        transform.position += moveSpeed * Time.deltaTime * forward;
    }
}
