using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IssueOrderLine : MonoBehaviour {

    LineRenderer line;
    UnitProducer prod;
    public float lineWidth = 0.01f;
	// Use this for initialization
	void Start () {
        line = GetComponent<LineRenderer>();
        prod = GetComponent<UnitProducer>();
        if(line == null)
        {
            line = gameObject.AddComponent<LineRenderer>();
            var mat = new Material(Shader.Find("Sprites/Default"));
            mat.color = prod.TeamIndex == 0 ? Color.cyan : Color.red;
            line.material = mat;
            line.startWidth = line.endWidth = lineWidth;
        }
	}
	
	// Update is called once per frame
	void Update () {
        var cursorLoc = CameraUI.instance.cursor.transform.position;
        line.material.color = prod.TeamIndex == 0 ? Color.cyan : Color.red;
        line.enabled = true;

        if (prod.isSelected)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, cursorLoc);
        } else if (prod.standingOrder != null)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, prod.standingOrder.location);
        } else
        {
            line.enabled = false;
        }
	}
}
