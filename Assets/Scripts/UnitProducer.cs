using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class OnBuildProgressEvent : UnityEvent<float> { };

public class UnitProducer : UnitBase
{
    [SerializeField]
    public GameObject[] unitPrefab;
    [SerializeField]
    public float[] buildTime;
    public Transform spawnLocation;
    public OnBuildProgressEvent OnBuildProgress;
    public Text productionText;
    public int activeUnit = 0;
    public float minRallyDistance = 5;
    protected float buildProgress;

    public override void Start()
    {
        base.Start();
        bool wasEnabled = meshAgent.enabled;
        meshAgent.enabled = true;
        this.meshAgent.destination = transform.position;
        meshAgent.enabled = wasEnabled;
    }

    protected override void Update()
    {
        base.Update();

        if (TeamIndex >= 0)
        {
            productionText.text = "Building: " + unitPrefab[activeUnit].name;

            buildProgress += Time.deltaTime / buildTime[activeUnit];
            buildProgress = Mathf.Clamp01(buildProgress);

            if (OnBuildProgress != null)
            {
                OnBuildProgress.Invoke(buildProgress);
            }

            if (buildProgress >= 1.0f)
            {
                SpawnUnit();
                buildProgress = 0.0f;
            }
        }
        else
        {
            productionText.text = "";
        }
    }

    public void UpdateBuildProgress(float addVal)
    {
        buildProgress += addVal;
    }

    protected virtual void SpawnUnit()
    {
        if (activeUnit < 0 && activeUnit >= unitPrefab.Length)
            return;
        Selectable spawned = Instantiate(unitPrefab[activeUnit], spawnLocation.position, Quaternion.identity).GetComponent<Selectable>();
        spawned.Start();

        //if(Vector3.Distance(this.meshAgent.destination, transform.position) > minRallyDistance)
        //    spawned.SetOrder(new MoveOrder(this.meshAgent.destination));
    }
}
