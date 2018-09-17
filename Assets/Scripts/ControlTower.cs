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

    public int TeamIndex;
    public float BuildBonus;

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
        }
        else if (teamOneUnits < teamTwoUnits)
        {
            TeamIndex = 1;
            gameObject.GetComponent<Renderer>().material = TeamTwoMaterial;
        }
        else
        {
            TeamIndex = -1;
            gameObject.GetComponent<Renderer>().material = DefaultMaterial;
        }
    }

    void OnDestroy()
    {
        Towers.Remove(this);
    }
}
