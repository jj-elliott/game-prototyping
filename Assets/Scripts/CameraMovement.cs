using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float Speed = 1.0f;
	// Use this for initialization
	void Start () {
		
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
            transform.Translate(Speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-Speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(0, Speed, 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(0, -Speed, 0);
        }
    }
}
