using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnBuildProgressEvent : UnityEvent<float> { };
public class UnitProducer : UnitBase {

    public float buildTime;
    private float buildProgress;
    public Transform spawnLocation;
    public GameObject unitPrefab;
    public OnBuildProgressEvent OnBuildProgress;

    protected override void Update()
    {
        base.Update();

        float buildBonus = 1.0f;
        foreach (ControlTower tower in ControlTower.Towers)
        {
            if (tower.TeamIndex == TeamIndex)
            {
                buildBonus *= 10.0f;
            }
        }

        buildProgress += Time.deltaTime / buildTime * buildBonus;
        buildProgress = Mathf.Clamp01(buildProgress);

        if(OnBuildProgress != null)
        {
            OnBuildProgress.Invoke(buildProgress);
        }

        if(buildProgress == 1)
        {
            SpawnUnit();
            buildProgress = 0;
        }
    }

    void SpawnUnit()
    {
        Instantiate(unitPrefab, spawnLocation.position, Quaternion.identity);
    }
}
