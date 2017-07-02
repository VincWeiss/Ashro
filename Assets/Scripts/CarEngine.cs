using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

public class CarEngine : MonoBehaviour {
    public GameObject Game;
    public Transform path;
    public float maxSteerAngle = 45f;
    public WheelCollider wheelFL, wheelFR, wheelRL, wheelRR;
    public float maxBrakeTorque = 150f, maxMotorTorque = 15f;
    public float currentSpeed;
    public float maxSpeed = 100f;
    public Vector3 centerOfMass;
    public float mass;
    public int amountNodes = 2;
    public float MaxCapacity = 10;
    private float tempCapacity = 0;
    public Slider Capacity;
    public GameObject Waypoint;
    public GameObject Collector;
    public int count = 0;
    public int ScoreVal = 100;
    public Text Score;
    public Text HighScore;
    public Text SoldScore;
    public int SoldScoreCount = 0;
    protected int userNodeCounter = 4;
    public Vector3 temp2;
    public SphereCommands patrol = new SphereCommands();
    public int currectNode = 0;
    public int wpCounter = 0;
    public Color rayColor = Color.yellow;
    private int userNodesCount = 0;
    public TextMesh capFollow;
    public Text topHighscore;
    private string filePath;
    public Transform UpgradePanel;
    public Transform HighscorePanel;
    public List<Transform> nodes = new List<Transform>();
    public float BuyUpgradeScore;
    public GameObject MotorButton;
    public GameObject CapacityButton;
    public Text textMotor;
    public int UpgradeMotorCount = 0;
    public Text textCapacity;
    public int UpgradeCapacityCount = 0;

    /*initilasation of dynamicvariables and transformation of preset waypoints in a list*/
    private void Start() {
        Debug.Log("Start");
        nodes = new List<Transform>();
        Transform [] pathTransforms = path.GetComponentsInChildren<Transform>();
        for(int i = 0; i < pathTransforms.Length; i++) {
            if(pathTransforms [i] != path.transform) {
                nodes.Add(pathTransforms [i]);
            }
        }
        currectNode = 0;
        readTophighscore();
        changeGameLocation();
        changePanelLocation();
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
        count = 0;
        incrementScore();
        InvokeRepeating("updateScore", 2.0f, 1.0f);
    }

    /*called at the beginning of every frame*/
    private void Update() {
        checkForUpgrade();
    }

    /*called at the end of every frame*/
    private void FixedUpdate() {
        ApplySteer();
        Drive();
        CheckWaypointDistance();
    }

    /*checks the steering ankle of the steering wheelcollider and 
    sets them in the direction of the waypoints */
    private void ApplySteer() {
        if(patrol.userNodes.Count > 0) {
            Vector3 relativeVector = transform.InverseTransformPoint(patrol.userNodes [0].transform.position);
            float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
            wheelFL.steerAngle = newSteer;
            wheelFR.steerAngle = newSteer;
        } else {
            Vector3 relativeVector = transform.InverseTransformPoint(nodes [currectNode].position);
            float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
            wheelFL.steerAngle = newSteer;
            wheelFR.steerAngle = newSteer;
        }
    }

    /*checks the speed of the wheelcollider and 
    sets the speed if under limit*/
    private void Drive() {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;
        if(currentSpeed < maxSpeed) {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        } else {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
    }

    /*checks the distance to the next waypoint. If distance small enough the waypoint is reached and 
     the next waypoint is set as current.
     If car reaches the barn it gets roated by 180 degrees*/
    private void CheckWaypointDistance() {
        if(patrol.userNodes.Count > 0) {
            if((Vector3.Distance(transform.position, patrol.userNodes [0].transform.position) < 0.015f)) {
                if(patrol.userNodes [0].name.Contains("Rotate")) {
                    setParametersAfterRotation();
                }
                if(patrol.userNodes [0].name == "Node") {
                    Destroy(patrol.userNodes [0]);
                }
                patrol.userNodes.RemoveAt(0);
            }
        } else {
            if((Vector3.Distance(transform.position, nodes [currectNode].position) < 0.015f)) {
                if(nodes [currectNode].name.Contains("Rotate")) {
                    setParametersAfterRotation();
                }
                if(currectNode == nodes.Count - 1) {
                    currectNode = 0;
                } else {
                    currectNode++;
                }
            }
        }
    }

    //Parameters after rotation
    private void setParametersAfterRotation() {
        wheelFL.motorTorque = 0;
        wheelFR.motorTorque = 0;
        gameObject.transform.Rotate(0, 180, 0);
        currectNode = 0;
        SoldScoreCount += count;
        ScoreVal += count;
        count = 0;
        Score.text = "Score: " + ScoreVal.ToString();
        tempCapacity = 0;
        SoldScoreCount = 0;
        Capacity.value = tempCapacity;
        BuyUpgradeScore = ScoreVal;
    }

    /*@Param Collider boxcollider
     Handles the car collecting asparagus event*/
    void OnTriggerEnter(Collider boxCollider) {
        if(tempCapacity < MaxCapacity) {
            if(boxCollider.gameObject.name == ("ripe")) {
                setAsparagusScorePoints(15, boxCollider);
                AppManager.highscore += 15;
            }
            if(boxCollider.gameObject.name == ("precocious")) {
                setAsparagusScorePoints(5, boxCollider);
                AppManager.highscore += 5;
            }
            if(boxCollider.gameObject.name == ("inedible")) {
                setAsparagusScorePoints(-20, boxCollider);
            }
            incrementScore();
        } else if(boxCollider.gameObject.name == "inedible" || boxCollider.gameObject.name == "precocious" || boxCollider.gameObject.name == "ripe") {
            boxCollider.gameObject.SetActive(false);
            while(boxCollider.gameObject.transform.position.y >= (Game.transform.position.y - 0.05f)){

            boxCollider.gameObject.transform.position -= new Vector3(0,0.001f,0);
            }
            //boxCollider.gameObject.transform.position += temp2;
        }
        Capacity.value = tempCapacity;
    }

    //adding the score points to the global count
    private void setAsparagusScorePoints(int value, Collider boxCollider) {
        count = count + value;
        boxCollider.gameObject.SetActive(false);
        while(boxCollider.gameObject.transform.position.y >= (Game.transform.position.y - 0.05f)) {

            boxCollider.gameObject.transform.position -= new Vector3(0, 0.001f, 0);
        }
        tempCapacity++;
    }

    /*called every time an asparagus is collected.
    Displays the highscore to the global panel*/
    void incrementScore() {
        HighScore.text = "Highscore: " + AppManager.highscore.ToString();
    }

    void updateScore() {
        Score.text = "Score: " + ScoreVal.ToString();
    }

    /*Gets the game position from AppManager to place the game field in the level*/
    public void changeGameLocation() {
        Game.transform.position = AppManager.Instance.PlaneLocation;
        Game.transform.rotation = AppManager.Instance.PlaneRotation;
        Game.transform.localScale = AppManager.Instance.PlaneScale;
    }

    /*Gets the panel position from AppManager to place the panel in the level*/
    public void changePanelLocation() {
        UpgradePanel.transform.position = AppManager.Instance.UpgradePanel.transform.position;
        UpgradePanel.transform.rotation = AppManager.Instance.UpgradePanel.transform.rotation;
        UpgradePanel.transform.localScale = AppManager.Instance.UpgradePanel.transform.localScale;

        HighscorePanel.transform.position = AppManager.Instance.HighscorePanel.transform.position;
        HighscorePanel.transform.rotation = AppManager.Instance.HighscorePanel.transform.rotation;
        HighscorePanel.transform.localScale = AppManager.Instance.HighscorePanel.transform.localScale;
    }

    //reads the TXT with the global Highscore and implements to the highscore panel (UI)
    public void readTophighscore() {
        filePath = Path.Combine(Application.persistentDataPath, "Highscore.txt");
        string [] Score = System.IO.File.ReadAllLines(filePath);
        topHighscore.text = Score [0];
    }

    /*called every frame.
    Checks if the user has enough money to buy an upgrade.
    Is set inactive if the score is low*/
    public void checkForUpgrade() {
        if(ScoreVal >= 50.0f) {
            MotorButton.SetActive(true);
            CapacityButton.SetActive(true);
        } else {
            MotorButton.SetActive(false);
            CapacityButton.SetActive(false);
        }
    }

    //called if user wants to upgrade his truck
    //coast for upgrade 50 points
    //called max 5 times
    public void MotorUpgrade() {
        if(UpgradeMotorCount <= 5) {
            count -= 50;
            ScoreVal -= 50;
            Score.text = "Score: " + ScoreVal.ToString();
            maxMotorTorque += 2;
            maxSpeed += 2;
            UpgradeMotorCount++;
            textMotor.text = "Motor: " + UpgradeMotorCount + "/5";
        }

    }

    //called if user wants to upgrade capacity of his truck
    //coast for upgrade 50 points
    //called max 5 times
    public void CapacityUpgrade() {
        if(UpgradeCapacityCount <= 5) {
            ScoreVal -= 50;
            count -= 50;
            MaxCapacity += 3;
            Score.text = "Score: " + ScoreVal.ToString();
            UpgradeCapacityCount++;
            Capacity.maxValue = MaxCapacity;
            textCapacity.text = "Capacity: " + UpgradeCapacityCount + "/5";
        }
    }
}