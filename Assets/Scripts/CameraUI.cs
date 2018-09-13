using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUI : MonoBehaviour {

    Camera camera;

	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();	
	}
	
    void OnLeftClick()
    {
        // The general idea here is to preform a raycast from the camera and see if we hit anything we can select

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        // Perform the raycast, and store info about the result in the hit
        Physics.Raycast(ray, out hit);


        if (hit.transform != null) // We hit something
        {
            Selectable sel = hit.transform.GetComponent<Selectable>(); // Ask unity if this thing has a selectable component on it

            if (sel != null && sel.TeamIndex == SelectionManager.instance.TeamIndex)
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    SelectionManager.instance.AddSelection(sel);
                }
                else
                {
                    SelectionManager.instance.SetSelection(sel);
                }
            }
        }
    }

    void OnRightClick()
    {
        // The general idea here is to preform a raycast from the camera and see if we hit anything we can select

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        // Perform the raycast, and store info about the result in the hit
        Physics.Raycast(ray, out hit);


        if (hit.transform != null)
        {
            UnitBase unit = hit.transform.gameObject.GetComponent<UnitBase>();

            if (hit.transform.CompareTag("Terrain")) // We hit somewhere we can move to
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    SelectionManager.instance.AddOrder(new MoveOrder(hit.point));
                }
                else
                {
                    SelectionManager.instance.IssueOrder(new MoveOrder(hit.point));
                }
            } else if(unit != null && unit.TeamIndex != SelectionManager.instance.TeamIndex)
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    SelectionManager.instance.AddOrder(new AttackOrder(hit.transform));
                }
                else
                {
                    SelectionManager.instance.IssueOrder(new AttackOrder(hit.transform));
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0)) // Did the player just click down the left mouse?
        {
            OnLeftClick();
        }

        if (Input.GetMouseButtonDown(1)) // Did the player just click down the right mouse?
        {
            OnRightClick();
        }
    }
}
