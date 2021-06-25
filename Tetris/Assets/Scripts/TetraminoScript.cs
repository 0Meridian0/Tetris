using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetraminoScript : MonoBehaviour
{
    public Vector3 tetraCenterRotation;
    public float fallTime = 0.8f;
    private float prevTime;
    private static int height = 20;
    private static int width = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PersonControl();

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            fallTime = fallTime / 10;
        }

        if (Time.time - prevTime > fallTime)
        {
            transform.position += new Vector3(0, -1, 0);

            if (!CanTetraMove())
            {
                transform.position -= new Vector3(0, -1, 0);
            }
            prevTime = Time.time;
        }
    }

    void PersonControl()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!CanTetraMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!CanTetraMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            transform.RotateAround(transform.TransformPoint(tetraCenterRotation), new Vector3(0, 0, 1), 90);
            if (!CanTetraMove())
            {
                transform.RotateAround(transform.TransformPoint(tetraCenterRotation), new Vector3(0, 0, 1), -90);
            }
        }
    }

    bool CanTetraMove()
    {
        foreach (Transform child in transform)
        {
            int X = Mathf.RoundToInt(child.transform.position.x);
            int Y = Mathf.RoundToInt(child.transform.position.y);

            if (X < 0 || X >= width || Y < 0 || Y >= height)
            {
                return false;
            }
        }
        return true;
    }
}
