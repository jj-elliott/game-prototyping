using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//[System.Serializable]
//public class OnPlayerCaptureProgressEvent : UnityEvent<float> { };

//[System.Serializable]
//public class OnEnemyCaptureProgressEvent : UnityEvent<float> { };
 
public class FaceLazerCharger : UnitProducer
{
    public OnPlayerCaptureProgressEvent OnPlayerCaptureProgress;
    public OnEnemyCaptureProgressEvent OnEnemyCaptureProgress;
    public Text captureText;
    protected float playerCaptureProgress = 0.0f;
    protected float enemyCaptureProgress = 0.0f;
    protected bool lazerReady = false;
    new float buildTime = 60f;
    public GameObject playerCaptureBar, enemyCaptureBar;
	// Use this for initialization
	public override void Start ()
    {
        base.Start();
        convertable = true;
        TeamIndex = 0;
        gameObject.GetComponent<Renderer>().material.color = SelectionManager.instance.teamColors[TeamIndex];
    }


    private void setLazerReady()
    {
        lazerReady = true;
        productionText.text = "FaceLazer ready";
        //need to update GUI to indicate lazer ready
    }

    public void UseLazer()
    {
        lazerReady = false;
        buildProgress = 0;
    }

    // Update is called once per frame
    protected override void Update ()
    {
        //base.Update();
        if (TeamIndex >= 0)
        {
            productionText.text = "Charging FaceLazer";

            buildProgress += Time.deltaTime / buildTime;
            buildProgress = Mathf.Clamp01(buildProgress);

            if (OnBuildProgress != null)
            {
                OnBuildProgress.Invoke(buildProgress);
            }

            if (buildProgress >= 1.0f)
            {
                setLazerReady();
            }
        }
    }

    public void UpdatePlayerCaptureProgress(float captureValue)
    {
        if (enemyCaptureProgress > 0.0f)
        {
            enemyCaptureProgress -= captureValue;
            enemyCaptureProgress = Mathf.Clamp01(enemyCaptureProgress);
            captureText.text = "Player is capturing FaceLazer";
            playerCaptureBar.SetActive(false);
            enemyCaptureBar.SetActive(true);

        }
        else if (playerCaptureProgress < 1.0f)
        {
            playerCaptureProgress += captureValue;
            playerCaptureProgress = Mathf.Clamp01(playerCaptureProgress);
            captureText.text = "Player is capturing FaceLazer";
            playerCaptureBar.SetActive(true);
            enemyCaptureBar.SetActive(false);
        }
    }

    public void UpdateEnemyCaptureProgress(float captureValue)
    {
        if (playerCaptureProgress > 0.0f)
        {
            playerCaptureProgress -= captureValue;
            playerCaptureProgress = Mathf.Clamp01(playerCaptureProgress);
            captureText.text = "Enemy is capturing FaceLazer";
            playerCaptureBar.SetActive(true);
            enemyCaptureBar.SetActive(false);
        }
        else if (enemyCaptureProgress < 1.0f)
        {
            enemyCaptureProgress += captureValue;
            enemyCaptureProgress = Mathf.Clamp01(enemyCaptureProgress);
            captureText.text = "Enemy is capturing FaceLazer";
            playerCaptureBar.SetActive(false);
            enemyCaptureBar.SetActive(true);
        }
    }

    public override void SetOrder(Order order)
    {
        if(lazerReady)
        {
            CameraUI.instance.FireLasers();
        }
    }
}
