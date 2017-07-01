using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    CarEngine cEngine = new CarEngine();
    public Text LevelTimer;
    private float Countdown=0.0f;
    public string sceneName;
    public GameObject lvlpassed;
    //public GameObject passedparticle;
    public GameObject continueButton;
    public GameObject lvlnotPassed;
    public GameObject restartButton;
    private bool GamePaused;
    public float costs;
    public bool isFinished = false;
    
    LvlManager lvl = new LvlManager();
    bool initCosts = false;

    // Use this for initialization
    void Start () {
        //switch lvl
        Time.timeScale = 1.0f;
        Scene scene = SceneManager.GetActiveScene();
        
            Countdown = 5.0f;
        costs = AppManager.Instance.costs;
        Debug.Log("Timer Costs " + costs);


        InvokeRepeating("calcScore", 2.0f, 1.0f);
    }
 
    public void FixedUpdate()
        {
        //Dostuff
        if (Countdown <= 0.0f)
            {
            //Time.timeScale = 0;
            //Pause Game with Time.timescale=0; stuff
            //Play Game with time.timescale=1; stuff

        }
    }
    void Update()
    {
        if (!isFinished)
        {
            Countdown -= Time.deltaTime;
            LevelTimer.text =  ""+Mathf.Round(Countdown);
        }
        if(Countdown <= 0.0f)
        {
            isFinished = true;
            cEngine.maxMotorTorque = 0.0f;
            if (cEngine.ScoreVal > 0)
            {
            lvlpassed.SetActive(true);
            continueButton.SetActive(true);
            }else
            {
                lvlnotPassed.SetActive(true);
                restartButton.SetActive(true);
            }
            LevelTimer.text = "";
        }
        
        
        
    }

    void calcScore() 
        {
            float Score = cEngine.ScoreVal;
            if (Countdown <=  120.0f && initCosts == false)
            {
                
                Score -= costs;
                initCosts = true;
            cEngine.ScoreVal = (int)Score;
        }
            else if (Countdown <= 120)
            {
                Score -= (costs / 60);
                Score = Mathf.Round(Score);
            cEngine.ScoreVal=(int)Score;
        }
        } 
    
}
