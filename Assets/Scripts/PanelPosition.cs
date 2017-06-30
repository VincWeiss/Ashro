using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPosition : MonoBehaviour {
    public GameObject panelToPlace;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeScene(string sceneName)
    {
        Debug.Log("Change Scene method");
        Debug.Log(panelToPlace.transform.position);
        AppManager.Instance.PanelLocation = panelToPlace.transform.position;
        AppManager.Instance.PanelRotation = panelToPlace.transform.rotation;

    

    }
}
