using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnBuildProgressEvent : UnityEvent<float> { };
public class UnitProducer : UnitBase {

    public float buildTime;
    float buildProgress;
    public Transform spawnLocation;
    public GameObject unitPrefab;
    public OnBuildProgressEvent OnBuildProgress;

    protected override void Update()
    {
        base.Update();

        buildProgress += Time.deltaTime / buildTime;
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
