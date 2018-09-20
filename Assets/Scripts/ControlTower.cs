using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTower : MonoBehaviour
{
    [SerializeField]
    private float CaptureRange;
    [SerializeField]
    private Material TeamOneMaterial;
    [SerializeField]
    private Material TeamTwoMaterial;

    private Material DefaultMaterial;
    LineRenderer line;

    public int TeamIndex;
    public float BuildBonus;

    [SerializeField]
    public Transform[] bases;
    public static List<ControlTower> Towers;

	// Use this for initialization
	void Start ()
    {
        if (Towers == null)
        {
            Towers = new List<ControlTower>();
        }

        Towers.Add(this);

        DefaultMaterial = gameObject.GetComponent<Renderer>().material;
        line = GetComponent<LineRenderer>();
        line.enabled = false;

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
            gameObject.GetComponent<Renderer>().material = TeamOneMaterial;
            line.enabled = true;
            line.startColor = line.endColor = Color.blue;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, bases[0].transform.position);
        }
        else if (teamOneUnits < teamTwoUnits)
        {
            TeamIndex = 1;
            gameObject.GetComponent<Renderer>().material = TeamTwoMaterial;
            line.enabled = true;
            line.startColor = line.endColor = Color.red;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, bases[1].transform.position);
        }
        else
        {
            TeamIndex = -1;
            gameObject.GetComponent<Renderer>().material = DefaultMaterial;
            line.enabled = false;

        }
    }

    void OnDestroy()
    {
        Towers.Remove(this);
    }
}
