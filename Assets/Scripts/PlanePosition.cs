using UnityEngine;

public class PlanePosition : MonoBehaviour {
    public GameObject planeToPlace;
    public Transform HighscorePanel;
    public Transform UpgradePanel;

    /*onClick method for the mapping scene.
    Called when user taps the "tapToConfirm" button.
    Sets the plane and the panels to the AppManager class.
    The method is loading the new scene*/
    public void ChangeScene(string sceneName) {
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
