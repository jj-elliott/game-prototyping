using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private float Speed = 1.0f;
    private int ScreenWidth;
    private int ScreenHeight;
    private int Limit = 10;

	// Use this for initialization
	void Start () 
    {
        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, Speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -Speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(0, Speed, 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(0, -Speed, 0);
        }
        if (Input.mousePosition.x > ScreenWidth - Limit)
        {
            transform.Translate(Speed, 0, 0);
        }
        if (Input.mousePosition.x < 0 + Limit)
        {
            transform.Translate(-Speed, 0, 0);
        }
        if (Input.mousePosition.y > ScreenHeight - Limit)
        {
            transform.Translate(0, Speed, 0);
        }
        if (Input.mousePosition.y < 0 + Limit)
        {
            transform.Translate(0, -Speed, 0);
        }
    }
}
