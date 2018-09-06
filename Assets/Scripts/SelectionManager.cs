using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour {

    public static SelectionManager instance;
    List<Selectable> currentSelection;

	// Use this for initialization
	void Start () {
        if(instance == null)
        {
            instance = this;
            currentSelection = new List<Selectable>();

        }
        else
        {
            Debug.LogWarning("There are multiple selection managers in the scene, please remove one.");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddSelection(Selectable sel)
    {
        if (sel.OnSelected != null)
        {
            sel.OnSelected.Invoke();
        }
        currentSelection.Add(sel);
    }

    public void SetSelection(Selectable sel)
    {
        ClearSelection();

        if (sel.OnSelected != null)
        {
            sel.OnSelected.Invoke();
        }
        currentSelection.Add(sel);
    }

    public void ClearSelection()
    {
        foreach(var sel in currentSelection)
        {
            if(sel.OnDeselected != null)
            {
                sel.OnDeselected.Invoke();
            }
        }
        currentSelection.Clear();
    }

    public void IssueOrder(Order order)
    {
        foreach(var sel in currentSelection)
        {
            sel.SetOrder(order);
        }
    }

    public void AddOrder(Order order)
    {
        foreach (var sel in currentSelection)
        {
            sel.AddOrder(order);
        }
    }
}
