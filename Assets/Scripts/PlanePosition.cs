using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePosition : MonoBehaviour {
    public GameObject planeToPlace;
    public Transform HighscorePanel;
    public Transform UpgradePanel;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeScene(string sceneName)
    {
        Debug.Log("Change Scene method");
        Debug.Log(planeToPlace.transform.position);
        AppManager.Instance.PlaneLocation = planeToPlace.transform.position;
        AppManager.Instance.PlaneRotation = planeToPlace.transform.rotation;
        AppManager.Instance.PlaneScale = planeToPlace.transform.localScale;

        AppManager.Instance.UpgradePanel = UpgradePanel;
        AppManager.Instance.HighscorePanel = HighscorePanel;


        StartCoroutine(AppManager.Instance.switchScenes(sceneName));

    }
}
