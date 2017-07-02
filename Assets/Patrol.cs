using System;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

    public List<GameObject> userNodes = new List<GameObject>();
    public Transform path;
    public GameObject userWPPrefab;

    // Use this for initialization
    void Start() {
        userWPPrefab = GameObject.Find("Waypoints");
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(1)) {
            try {
                // Casts the ray and get the first game object hit
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                //Creates empty WP object
                GameObject wpToSet = GameObject.CreatePrimitive(PrimitiveType.Quad);
                wpToSet.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                wpToSet.GetComponent<Renderer>().material.color = Color.red;
                wpToSet.transform.Rotate(90, 0, 0);
                wpToSet.transform.parent = userWPPrefab.transform;
                wpToSet.name = "Node";
                wpToSet.transform.position = new Vector3(hit.point.x, 0.0002f, hit.point.z);

                if(wpToSet.transform.position.x <= -1.7) {
                    wpToSet.name = "Node";
                    wpToSet.transform.position = new Vector3(-1.1f, 0.0002f, 1.0f);
                    userNodes.Add(wpToSet);
                    //ADD new WP to userNode list
                    GameObject BarnwpToSet = new GameObject();
                    BarnwpToSet.name = "Rotate";
                    BarnwpToSet.transform.position = new Vector3(-2.5f, 0.0002f, 1.0f);
                    userNodes.Add(BarnwpToSet);

                    GameObject SecondBarnwpToSet = new GameObject();
                    SecondBarnwpToSet.name = "Node";
                    SecondBarnwpToSet.transform.position = new Vector3(-1.1f, 0.0002f, 1.0f);
                    userNodes.Add(SecondBarnwpToSet);
                } else {
                    userNodes.Add(wpToSet);
                }

            } catch(Exception e) {
                Debug.Log(e);
            }
        }
    }
}
