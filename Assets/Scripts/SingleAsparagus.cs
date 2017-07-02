using UnityEngine;

public class SingleAsparagus : MonoBehaviour {
    private int countTime = 0;
    Vector3 growPosition = new Vector3(0, 0.002f, 0);
    Vector3 basicPosition;
    Vector3 startPosition;

    void Start() {
        basicPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y-0.06f, gameObject.transform.position.z);
        Debug.Log("BASICPOSITION " + basicPosition);
        startPosition = gameObject.transform.position;
        setActivity();
        InvokeRepeating("GrowAsparagus", 1.0f, 1.0f);
    }

    /*called every second
     the asparagus growing method for the different asparagus status*/
    void GrowAsparagus() {
        if(gameObject.activeSelf==true) {
            if(countTime <= 15) {
                gameObject.transform.position += growPosition;
                countTime++;
                gameObject.name = "growing";
            }
            if(countTime > 15 && countTime <= 30) {
                gameObject.transform.position += growPosition;
                countTime++;
                gameObject.name = "precocious";
            } else if(countTime > 30 && countTime <= 60) {
                gameObject.GetComponent<Renderer>().material.color = Color.green;
                countTime++;
                gameObject.name = "ripe";
            } else if(countTime > 60 && countTime <= 90) {
                gameObject.GetComponent<Renderer>().material.color = new Color32(139, 69, 19, 1);
                gameObject.name = "inedible";
                countTime++;
            } else if(countTime == 91) {
                while(gameObject.transform.position.y >= (AppManager.Instance.PlaneLocation.y - 0.05f)) {
                    gameObject.transform.position -= new Vector3(0, 0.001f, 0);
                }
                countTime++;
            }
             else if(countTime > 91 && countTime <= 105) {
                gameObject.name = "blocked";
                //gameObject.transform.position = basicPosition;
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                countTime++;
            } else if(countTime > 105) {
                gameObject.name = "asparagus";
                gameObject.SetActive(false);
                countTime = 0;
            }
        } else {
            
            secondActivity();
        }
    }

    /*sets the probability if an aspargus is growing or not for the first time*/
    public void setActivity() {
        int probability = Random.Range(0, 101);
        if(probability <= 5) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }

    /*sets the probability if an aspargus is growing or not for the second time*/
    public void secondActivity() {
        int probability = Random.Range(0, 100);
        gameObject.GetComponent<Renderer>().material.color = Color.white;
        if(probability <= 5) {
            countTime = 0;
            //gameObject.transform.position = startPosition;
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }
}