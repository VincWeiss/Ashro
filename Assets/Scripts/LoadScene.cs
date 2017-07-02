using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    [HideInInspector]
    public string sceneName;

    public void ChangeScene(string sceneName) {
        StartCoroutine(AppManager.Instance.switchScenes(sceneName));
    }

    public void ReloadScene() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
