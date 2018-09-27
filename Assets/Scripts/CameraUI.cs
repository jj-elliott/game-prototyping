using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraUI : MonoBehaviour {

    Camera camera;
    Vector2 clickStartPos;
    public float dragThreshold = 5;
    UnityEngine.XR.WSA.Input.GestureRecognizer recognizer;
    // Use this for initialization
    void Start () {
        camera = GetComponent<Camera>();
        recognizer = new UnityEngine.XR.WSA.Input.GestureRecognizer();
        recognizer.SetRecognizableGestures(UnityEngine.XR.WSA.Input.GestureSettings.Tap);
        recognizer.TappedEvent += OnAirTap;
        recognizer.StartCapturingGestures();
    }
	
    void OnLeftClick(Ray ray)
    {
        // The general idea here is to preform a raycast from the camera and see if we hit anything we can select

        RaycastHit hit;
        
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
            } else
            {
                SelectionManager.instance.ClearSelection();
            }
        }
    }

    void OnAirTap(UnityEngine.XR.WSA.Input.InteractionSourceKind source, int tapCount, Ray headRay)
    {
        print("Detected tap! " + tapCount);
        if(tapCount == 1)
        {
            OnLeftClick(headRay);
        } else
        {
            OnRightClick(headRay);
        }
    }

    void OnBoxSelect(Vector2 startPos , Vector2 endPos)
    {
        SelectionManager.instance.ClearSelection();

        foreach(var unit in SelectionManager.instance.GetUnits(SelectionManager.instance.TeamIndex))
        {
            if(unit == null)
            {
                continue;
            }
            Vector3 screenPos = camera.WorldToScreenPoint(unit.transform.position);

            if(screenPos.z > 0 && screenPos.x > Mathf.Min(startPos.x , endPos.x) && screenPos.x < Mathf.Max(startPos.x, endPos.x) &&
                screenPos.y > Mathf.Min(startPos.y, endPos.y) && screenPos.y < Mathf.Max(startPos.y, endPos.y))
            {
                SelectionManager.instance.AddSelection(unit);
            }
        }
    }

    void OnRightClick(Ray ray)
    {
        // The general idea here is to preform a raycast from the camera and see if we hit anything we can select

        RaycastHit hit;

        // Perform the raycast, and store info about the result in the hit
        Physics.Raycast(ray, out hit);


        if (hit.transform != null)
        {
            UnitBase unit = hit.transform.gameObject.GetComponent<UnitBase>();
            UnitProducer prod = hit.transform.gameObject.GetComponent<UnitProducer>();
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
            }

            else if (prod != null && prod.TeamIndex == SelectionManager.instance.TeamIndex)
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    SelectionManager.instance.AddOrder(new DonateOrder(prod));
                }
                else
                {
                    SelectionManager.instance.IssueOrder(new DonateOrder(prod));
                }
            }

            else if(unit != null && unit.TeamIndex != SelectionManager.instance.TeamIndex)
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
            clickStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)) // Did the player just click down the left mouse?
        {
            if(Vector2.Distance(clickStartPos , Input.mousePosition) > dragThreshold)
            {
                OnBoxSelect(clickStartPos, Input.mousePosition);
            } else
            {
                OnLeftClick(camera.ScreenPointToRay(Input.mousePosition));
            }
        }

        

        if (Input.GetMouseButtonDown(1)) // Did the player just click down the right mouse?
        {
            OnRightClick(camera.ScreenPointToRay(Input.mousePosition));
        }
    }

    private void OnGUI()
    {
        if (Input.GetMouseButton(0) && Vector2.Distance(clickStartPos, Input.mousePosition) > dragThreshold)
        {
            Rect r = new Rect();
            r.xMin = Mathf.Min(clickStartPos.x, Input.mousePosition.x);
            r.xMax = Mathf.Max(clickStartPos.x, Input.mousePosition.x);
            r.yMin = Screen.height - Mathf.Min(clickStartPos.y, Input.mousePosition.y);
            r.yMax = Screen.height - Mathf.Max(clickStartPos.y, Input.mousePosition.y);

            Utils.DrawScreenRect(r, Color.green - new Color(0, 0, 0, 0.75f));
        }
    }
}
