using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class OnPlayerCaptureProgressEvent : UnityEvent<float> { };

[System.Serializable]
public class OnEnemyCaptureProgressEvent : UnityEvent<float> { };

public class CaptureableUnitProducer : UnitProducer
{
    public float captureRadius;
    public float captureTime;
    public OnPlayerCaptureProgressEvent OnPlayerCaptureProgress;
    public OnEnemyCaptureProgressEvent OnEnemyCaptureProgress;
    public Text captureText;
    [SerializeField]
    public Material playerUnitMaterial;
    [SerializeField]
    public Material enemyUnitMaterial;
    [SerializeField]
    public Material neutralUnitMaterial;
    protected float playerCaptureProgress = 0.0f;
    protected float enemyCaptureProgress = 0.0f;
    [SerializeField]
    public GameObject enemyPrefab;

	// Use this for initialization
	public override void Start ()
    {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();

        int numPlayerUnits = 0;
        List<Selectable> playerUnits = SelectionManager.instance.GetUnits(0);
        foreach (Selectable unit in playerUnits)
        {
            if (unit != null && Vector3.Distance(transform.position, unit.transform.position) < captureRadius)
            {
               ++numPlayerUnits;
            }
        }

        int numEnemyUnits = 0;
        List<Selectable> enemyUnits = SelectionManager.instance.GetUnits(1);
        foreach (Selectable unit in enemyUnits)
        {
            if (unit != null && Vector3.Distance(transform.position, unit.transform.position) < captureRadius)
            {
                ++numEnemyUnits;
            }
        }

        if (numPlayerUnits > numEnemyUnits)
        {
            float difference = (float)(numPlayerUnits - numEnemyUnits);
            if (enemyCaptureProgress > 0.0f)
            {
                captureText.text = "Player is capturing factory";

                enemyCaptureProgress -= Time.deltaTime / captureTime * difference;
                enemyCaptureProgress = Mathf.Clamp01(enemyCaptureProgress);

                if (OnEnemyCaptureProgress != null)
                {
                    OnEnemyCaptureProgress.Invoke(enemyCaptureProgress);
                }
            }
            else if (playerCaptureProgress < 1.0f)
            {
                captureText.text = "Player is capturing factory";

                playerCaptureProgress += Time.deltaTime / captureTime * difference;
                playerCaptureProgress = Mathf.Clamp01(playerCaptureProgress);

                if (OnPlayerCaptureProgress != null)
                {
                    OnPlayerCaptureProgress.Invoke(playerCaptureProgress);
                }
            }
            else
            {
                captureText.text = "";
            }
        }
        else if (numPlayerUnits < numEnemyUnits)
        {
            float difference = (float)(numEnemyUnits - numPlayerUnits);
            if (playerCaptureProgress > 0.0f)
            {
                captureText.text = "Enemy is capturing factory";

                playerCaptureProgress -= Time.deltaTime / captureTime * difference;
                playerCaptureProgress = Mathf.Clamp01(playerCaptureProgress);

                if (OnPlayerCaptureProgress != null)
                {
                    OnPlayerCaptureProgress.Invoke(playerCaptureProgress);
                }
            }
            else if (enemyCaptureProgress < 1.0f)
            {
                captureText.text = "Enemy is capturing factory";

                enemyCaptureProgress += Time.deltaTime / captureTime * difference;
                enemyCaptureProgress = Mathf.Clamp01(enemyCaptureProgress);

                if (OnEnemyCaptureProgress != null)
                {
                    OnEnemyCaptureProgress.Invoke(enemyCaptureProgress);
                }
            }
            else
            {
                captureText.text = "";
            }
        }
        else
        {
            captureText.text = "";
        }

        if (playerCaptureProgress == 1.0f)
        {
            TeamIndex = 0;
            gameObject.GetComponent<Renderer>().material = playerUnitMaterial;

        }
        else if (playerCaptureProgress == 1.0f)
        {
            TeamIndex = 1;
            gameObject.GetComponent<Renderer>().material = enemyUnitMaterial;
        }
        else
        {
            TeamIndex = -1;
            gameObject.GetComponent<Renderer>().material = neutralUnitMaterial;
        }
    }

    protected override void SpawnUnit()
    {
        if (TeamIndex == 0)
        {
            Selectable spawned = Instantiate(unitPrefab, spawnLocation.position, Quaternion.identity).GetComponent<Selectable>();
            spawned.Start();
            ((UnitBase)spawned).homeBase = this;
            if (standingOrder != null)
                spawned.SetOrder(standingOrder);

        }
        else if (TeamIndex == 1)
        {
            Selectable spawned = Instantiate(enemyPrefab, spawnLocation.position, Quaternion.identity).GetComponent<Selectable>();
            spawned.Start();
            ((UnitBase)spawned).homeBase = this;
            if (standingOrder != null)
                spawned.SetOrder(standingOrder);

        }
    }

    public void UpdatePlayerCaptureProgress(float captureValue)
    {
        if (enemyCaptureProgress > 0.0f)
        {
            enemyCaptureProgress -= captureValue;
            enemyCaptureProgress = Mathf.Clamp01(enemyCaptureProgress);
        }
        else
        {
            playerCaptureProgress += captureValue;
            playerCaptureProgress = Mathf.Clamp01(playerCaptureProgress);
        }
    }

    public void UpdateEnemyCaptureProgress(float captureValue)
    {
        if (playerCaptureProgress > 0.0f)
        {
            playerCaptureProgress -= captureValue;
            playerCaptureProgress = Mathf.Clamp01(playerCaptureProgress);
        }
        else
        {
            enemyCaptureProgress += captureValue;
            enemyCaptureProgress = Mathf.Clamp01(enemyCaptureProgress);
        }
    }
}
