using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAsparagus : MonoBehaviour
{
    Asparagus asparagus = new Asparagus();
    int countTime = 0;
    Vector3 growPosition;
    Vector3 basicPosition;
    // Use this for initialization
    void Start()
    {
        growPosition = new Vector3(0, 0.01f * AppManager.Instance.PlaneScale.y, 0);
        basicPosition = new Vector3(0, AppManager.Instance.PlaneLocation.y - 0.2f , 0);
        setActivity();

        InvokeRepeating("GrowAsparagus", 1.0f, 1.0f);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void GrowAsparagus()
    {
        if (gameObject.activeSelf)
        {
            if (countTime <= 15)
            {
                gameObject.transform.position += growPosition;
                countTime++;
                gameObject.name = "growing";
            }
            if (countTime > 15 && countTime <= 30)
            {

                gameObject.transform.position += growPosition;
                countTime++;
                gameObject.name = "precocious";
            }
            else if (countTime > 30 && countTime <= 60)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.green;

                countTime++;
                gameObject.name = "ripe";

            }
            else if (countTime > 60 && countTime <= 90)
            {
                gameObject.GetComponent<Renderer>().material.color = new Color32(139, 69, 19, 1);
                gameObject.name = "inedible";
                countTime++;

            }
            else if (countTime > 90 && countTime <= 120)
            {
                gameObject.name = "blocked";
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                gameObject.transform.position = basicPosition;

                countTime++;

            }
            else if (countTime > 120)
            {
                gameObject.name = "aspargus";
                gameObject.SetActive(false);
                countTime = 0;

            }
        }
        else
        {
            secondActivity();
        }
    }


    public void setActivity()
    {
        int probability = Random.Range(0, 101);

        if (probability <= 5)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
    public void secondActivity()
    {
        int probability = Random.Range(0, 1000);
        gameObject.GetComponent<Renderer>().material.color = Color.white;
        if (probability <= 1)
        {
            gameObject.SetActive(true);
            countTime = 0;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}