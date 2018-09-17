using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnBuildProgressEvent : UnityEvent<float> { };
public class UnitProducer : UnitBase {

    [SerializeField]
    public float[] buildTime;
    private float buildProgress;
    public Transform spawnLocation;
    [SerializeField]
    public GameObject[] unitPrefab;
    public OnBuildProgressEvent OnBuildProgress;
    public int activeUnit = 0;

    protected override void Update()
    {
        base.Update();

        float buildBonus = 1.0f;
        foreach (ControlTower tower in ControlTower.Towers)
        {
            if (tower.TeamIndex == TeamIndex)
            {
                buildBonus *= tower.BuildBonus;
            }
        }

        buildProgress += Time.deltaTime / buildTime[activeUnit] * buildBonus;
        buildProgress = Mathf.Clamp01(buildProgress);

        if(OnBuildProgress != null)
        {
            OnBuildProgress.Invoke(buildProgress);
        }

        if(buildProgress == 1)
        {
            buildProgress = 0;
            SpawnUnit();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            activeUnit = 0;
            buildProgress = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            activeUnit = 1;
            buildProgress = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            activeUnit = 2;
            buildProgress = 0;
        }


    }

    void SpawnUnit()
    {
        Selectable spawned = Instantiate(unitPrefab[activeUnit], spawnLocation.position, Quaternion.identity).GetComponent<Selectable>();
        spawned.Start();
        spawned.SetOrder(new MoveOrder(this.meshAgent.destination));
    }
}
