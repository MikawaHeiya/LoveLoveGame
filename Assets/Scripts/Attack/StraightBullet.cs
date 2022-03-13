using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 forward;

    private void FixedUpdate()
    {
        transform.position += Time.deltaTime * moveSpeed * (transform.rotation * forward);
    }
}
