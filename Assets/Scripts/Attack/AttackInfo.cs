using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInfo : MonoBehaviour
{
    public float existTime;
    public float damage;
    public GameObject sender;
    public bool destroyAfterRecieved;

    private void Start()
    {
        sender = gameObject;
        StartCoroutine(SelfDestroy());
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(existTime);
        Destroy(gameObject);
    }
}
