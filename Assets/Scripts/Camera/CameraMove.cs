using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Vector3 positionLeftBorder;
    public Vector3 positionRightBorder;

    public void Move(Vector3 forward)
    {
        var move = new Vector3(forward.x, 0f, 0f);
        if (transform.position.x + move.x >= positionLeftBorder.x &&
            transform.position.x + move.x <= positionRightBorder.x)
        {
            transform.position += move;
        }
    }
}
