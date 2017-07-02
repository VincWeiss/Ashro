using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public CarEngine cEngine;
    public Text LevelTimer;
    private float Countdown = 0.0f;
    public string sceneName;
    public GameObject lvlpassed;
    public GameObject continueButton;
    public GameObject lvlnotPassed;
    public GameObject restartButton;
    private bool GamePaused;
    public float costs;
    public bool isFinished = false;
    bool initCosts = false;

    // Use this method for initialization
    void Start() {
        Time.timeScale = 1.0f;
        Scene scene = SceneManager.GetActiveScene();
        Countdown = 10.0f;
        costs = AppManager.Instance.costs;
        Debug.Log("Timer Costs " + costs);
        InvokeRepeating("calcScore", 2.0f, 1.0f);
    }

    void Update() {
        if(!isFinished) {
            Countdown -= Time.deltaTime;
            LevelTimer.text = "" + Mathf.Round(Countdown);
        }
        if(Countdown <= 0.0f) {
            isFinished = true;
            cEngine.maxMotorTorque = 0.0f;
            //if(cEngine.ScoreVal > 0) {
                lvlpassed.SetActive(true);
                continueButton.SetActive(true);
           /* } else {
                lvlnotPassed.SetActive(true);
                restartButton.SetActive(true);
            }*/
            LevelTimer.text = "";
        }
    }

    void calcScore() {
        float Score = cEngine.ScoreVal;
        if(Countdown <= 120.0f && initCosts == false) {

            Score -= costs;
            initCosts = true;
            cEngine.ScoreVal = (int)Score;
        } else if(Countdown <= 120 && Countdown >= 0) {
            Score -= (costs / 60);
            Score = Mathf.Round(Score);
            cEngine.ScoreVal = (int)Score;
        }
    }
}
