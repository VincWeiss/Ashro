using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.IO;

public class CarEngine : MonoBehaviour
{
    public GameObject Game;
    public Transform path;
    public float maxSteerAngle = 45f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;
    public float maxBrakeTorque = 150f;
    public float maxMotorTorque = 15f;
    
    public float currentSpeed;
    public float maxSpeed = 100f;
    public Vector3 centerOfMass;
    public float mass;
    public bool isBraking = false;
    public int amountNodes = 2;
    public float MaxCapacity = 10;
    private float tempCapacity = 0;
    public Slider Capacity;
    GameObject Waypoint;
    public GameObject Collector;
    private int count=0;
    //private static int highscore;
    public int ScoreVal=0;
    public Text Score;
    public Text textWin;
    public Text HighScore;
    public Text SoldScore;
    int SoldScoreCount = 0;
    protected int userNodeCounter = 4;
    [Header("Sensors")]
    public float sensorLength = 0.1f;
    public Vector3 frontSensorPosition = new Vector3(0f, 0.2f, 0.5f);
    Vector3 temp2 = new Vector3(0, -0.55f, 0);
    public float frontSideSensorPosition = 0.2f;
    public float frontSensorAngle = 30f;
    //public Patrol patrol = new Patrol();
    public SphereCommands patrol = new SphereCommands();
    public int currectNode = 0;
    public int wpCounter = 0;
    public Color rayColor = Color.yellow;
    private int userNodesCount = 0;
    public TextMesh capFollow;

    public Text topHighscore;
    private string filePath;

    //panel
    public GameObject UpgradePanel;
    public GameObject HighscorePanel;

    public List<Transform> nodes = new List<Transform>();
    private void Start()
    {
        readTophighscore();
        changeGameLocation();
        changePanelLocation();
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
        count = 0;
        incrementScore();
        InvokeRepeating("updateScore", 2.0f, 1.0f);

        nodes = new List<Transform>();
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();


        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }

    }

    private void Update()
    {


        
       
        
    }
    private void FixedUpdate()
    {
        
           
            ApplySteer();
            Drive();
            CheckWaypointDistance();
            //Braking();

       
    }
    
 
    
    private void ApplySteer()
    {
        if (patrol.userNodes.Count > 0){
            Vector3 relativeVector = transform.InverseTransformPoint(patrol.userNodes[0].transform.position);
            float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
            wheelFL.steerAngle = newSteer;
            wheelFR.steerAngle = newSteer;
        }else
        {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currectNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
        }
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

        if (currentSpeed < maxSpeed && !isBraking)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
    }

    private void CheckWaypointDistance()
    {
        if (patrol.userNodes.Count > 0)
        {
            if ((Vector3.Distance(transform.position, patrol.userNodes[0].transform.position) < 0.015f))
            {
                if (patrol.userNodes[0].name.Contains("Rotate"))
                {
                    wheelFL.motorTorque = 0;
                    wheelFR.motorTorque = 0;
                    gameObject.transform.Rotate(0, 180, 0);
                    currectNode = 0;
                    SoldScoreCount += count;

                    SoldScore.text = "Sold asparagus: " + SoldScoreCount.ToString();
                    ScoreVal += count;
                    count = 0;
                    Score.text = "Score: " + ScoreVal.ToString();
                    tempCapacity = 0;
                    SoldScoreCount = 0;
                    Capacity.value = tempCapacity;
                }
                if(patrol.userNodes[0].name == "Node")
                {
                Destroy(patrol.userNodes[0]);
                }
                patrol.userNodes.RemoveAt(0);
            }

        }
        else
        {
            if ((Vector3.Distance(transform.position, nodes[currectNode].position) < 0.015f))
            {
                if (nodes[currectNode].name.Contains("Rotate"))
                {
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

                    /*Debug.Log("Reached WP is " + nodes[currectNode].name);
                    Debug.Log("Currect Node is " + currectNode);
                    Debug.Log("Current WPCount is " + wpCounter);
                    Debug.Log("Nodes.Count is " + nodes.Count);*/
                    /*Debug.Log(currectNode);
                    if (nodes[currectNode].name.Equals("Node")){
                        nodes.RemoveAt(currectNode);

                        currectNode++;
                    }*/
                }




                if (currectNode == nodes.Count - 1)
            {
                currectNode = 0;
                //nodes = new List<Transform>();
            }
            else
            {
                
                currectNode++;
            }
            //Debug.Log("New currect Node is " + currectNode);
        }

        }

    }

    private void Braking()
    {
        if (isBraking)
        {

            wheelRL.brakeTorque = maxBrakeTorque;
            wheelRR.brakeTorque = maxBrakeTorque;
        }
        else
        {

            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
    }

    void OnTriggerEnter(Collider boxCollider)
    {
        
        //Debug.Log("Collision");
        
        /*
        if (boxCollider.gameObject.name == ("Barn_station"))
        {
            SoldScoreCount += count;
            SoldScore.text = "Sold asparagus: " + SoldScoreCount.ToString();
            count = 0;
            textCount.text = "Score: " + count.ToString();
        }*/
        if (tempCapacity < MaxCapacity)
        {
            if (boxCollider.gameObject.name == ("ripe"))
            {
                count += 15;
                AppManager.highscore += 15;
                
                //SoldScore.text = count.ToString();
                boxCollider.gameObject.SetActive(false);
                //Debug.Log(count);
                boxCollider.gameObject.transform.position = temp2;
                tempCapacity++;
            }
            if (boxCollider.gameObject.name == ("precocious"))
            {
                count += 5;
                AppManager.highscore += 5;
                boxCollider.gameObject.SetActive(false);
                boxCollider.gameObject.transform.position = temp2;
                tempCapacity++;
            }
            if (boxCollider.gameObject.name == ("inedible"))
            {
                count -= 20;
                boxCollider.gameObject.SetActive(false);
                boxCollider.gameObject.transform.position = temp2;
                tempCapacity++;
            }
            incrementScore();
        }else if(boxCollider.gameObject.name == "inedible" || boxCollider.gameObject.name == "precocious" || boxCollider.gameObject.name == "ripe")
        {
            boxCollider.gameObject.SetActive(false);
            boxCollider.gameObject.transform.position = temp2;
        }

        Capacity.value = tempCapacity;
    }

    void incrementScore()
    {
        HighScore.text = "Highscore: " + AppManager.highscore.ToString();
        //Score.text = "Score: " + count.ToString();
        if (count >= 300)
        {
            textWin.text = "Winner";
        }
    }


    void updateScore()
    {
        Score.text = "Score: " + ScoreVal.ToString();
    }

    public void changeGameLocation()
    {
        Game.transform.position = AppManager.Instance.PlaneLocation;
        Game.transform.rotation = AppManager.Instance.PlaneRotation;
        Game.transform.localScale = AppManager.Instance.PlaneScale;

    }

    public void changePanelLocation()
    {
        UpgradePanel.transform.position = AppManager.Instance.UpgradePanel.transform.position;
        UpgradePanel.transform.rotation = AppManager.Instance.UpgradePanel.transform.rotation;
        UpgradePanel.transform.localScale = AppManager.Instance.UpgradePanel.transform.localScale;

        HighscorePanel.transform.position = AppManager.Instance.HighscorePanel.transform.position;
        HighscorePanel.transform.rotation = AppManager.Instance.HighscorePanel.transform.rotation;
        HighscorePanel.transform.localScale = AppManager.Instance.HighscorePanel.transform.localScale;
    }

    public void readTophighscore()
    {
        filePath = Path.Combine(Application.persistentDataPath, "Highscore.txt");
        string[] Score = System.IO.File.ReadAllLines(filePath);
        topHighscore.text = Score[0];
    }
}