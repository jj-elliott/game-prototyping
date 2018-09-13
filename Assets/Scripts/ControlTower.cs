using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTower : MonoBehaviour
{
    public float CaptureRange;
    public int TeamIndex;

    public static List<ControlTower> Towers;

	// Use this for initialization
	void Start ()
    {
        if (Towers == null)
        {
            Towers = new List<ControlTower>();
        }

        Towers.Add(this);
    }

    // Update is called once per frame
    void Update ()
    {
        int teamOneUnits = 0;
        int teamTwoUnits = 0;

        foreach (UnitBase unit in UnitBase.Units)
        {
            if (unit != null)
            {
                float distance = (unit.transform.position - transform.position).magnitude;
                if (distance < CaptureRange)
                {
                    if (unit.TeamIndex == 0)
                    {
                        teamOneUnits++;
                    }
                    else
                    {
                        teamTwoUnits++;
                    }
                }
            }
        }

        if (teamOneUnits > teamTwoUnits)
        {
            TeamIndex = 0;
        }
        else if (teamOneUnits < teamTwoUnits)
        {
            TeamIndex = 1;
        }
        else
        {
            TeamIndex = -1;
        }
    }

    void OnDestroy()
    {
        Towers.Remove(this);
    }
}
