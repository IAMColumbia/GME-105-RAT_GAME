using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMover : MonoBehaviour
{

    public float moveSpeed = 5f;

    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= moveSpeed * Time.deltaTime;
        }

        transform.position = pos;
    }
}
