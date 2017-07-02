using UnityEngine;

public class PlanePosition : MonoBehaviour {
    public GameObject planeToPlace;
    public GameObject localHighscorePanel;
    public GameObject localUpgradePanel;

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


        AppManager.Instance.UpgradePanelLocation = localUpgradePanel.transform.position;
        AppManager.Instance.UpgradePanelRotation = localUpgradePanel.transform.rotation;
        AppManager.Instance.UpgradePanelScale = localUpgradePanel.transform.localScale;

        AppManager.Instance.HighscorePanelLocation = localHighscorePanel.transform.position;
        AppManager.Instance.HighscorePanelRotation = localHighscorePanel.transform.rotation;
        AppManager.Instance.HighscorePanelScale = localHighscorePanel.transform.localScale;
        StartCoroutine(AppManager.Instance.switchScenes(sceneName));
    }
}
