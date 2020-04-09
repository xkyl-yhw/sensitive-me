using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladderSetPos : MonoBehaviour
{
     public Vector2 downPos;
     public Vector2 upPos;

    private void Start()
    {
        setPos();
    }

    void setPos()
    {
        downPos = transform.position - new Vector3(0, transform.localScale.y / 2 - 0.5f);
        upPos = transform.position + new Vector3(0, transform.localScale.y / 2 + 0.5f);
    }
}
