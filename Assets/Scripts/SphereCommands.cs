using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SphereCommands : MonoBehaviour
{

    public List<GameObject> userNodes = new List<GameObject>();
    public Transform path;
    private GameObject userWPPrefab;
    // Use this for initialization
    void Start()
    {
        userWPPrefab = GameObject.Find("Waypoints");
    }
    void OnBarnSelect()
    {
        
        GameObject ANode = GameObject.Find("ANode");
        GameObject BRotate = GameObject.Find("BRotate");
        GameObject BNode = GameObject.Find("BNode");
        userNodes.Add(ANode);
        userNodes.Add(BRotate);
        userNodes.Add(BNode);
       

    }

        void OnSelect()
    {
       
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hit;
            if (Physics.Raycast(headPosition, gazeDirection, out hit))
            {
                // Casts the ray and get the first game object hit

                //Creates empty WP object
                GameObject wpToSet = GameObject.CreatePrimitive(PrimitiveType.Quad);

                //Debug.Log("Pressed right click.");
                wpToSet.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                wpToSet.GetComponent<Renderer>().material.color = Color.blue;
                wpToSet.transform.Rotate(90, 0, 0);
                wpToSet.transform.parent = userWPPrefab.transform;


                wpToSet.name = "Node";


                float boardYLoc = AppManager.Instance.PlaneLocation.y;
                //Assign Raycast value to wp;
                wpToSet.transform.position = new Vector3(hit.point.x, boardYLoc += 0.002f, hit.point.z);




                    userNodes.Add(wpToSet);

            }
       
        /*
        Debug.Log("OnSelected Called");
        // If the sphere has no Rigidbody component, add one to enable physics.
        // if (!this.GetComponent<Rigidbody>())
        //{

        // Do a raycast into the world based on the user's
        // head position and orientation.
       
            // If the raycast hit a hologram, use that as the focused object.


            GameObject placeholder = GameObject.CreatePrimitive(PrimitiveType.Quad);
            Debug.Log("Pressed right click.");

            placeholder.name = "Node";
            // Casts the ray and get the first game object hit
            Logtxt.text = "This hit at " + hitInfo.point.ToString();
            placeholder.transform.position = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
            placeholder.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            placeholder.GetComponent<Renderer>().material.color = Color.red;
            placeholder.transform.Rotate(90, 0, 0);
            GameObject PathHolder = GameObject.Find("Waypoints");
            //
            if (hitInfo.collider.name == "Barn")
            {
                placeholder.transform.position = new Vector3(-1.1f, 0.02f, 1.0f);
                cEngine.wpCounter++;

                placeholder.transform.parent = PathHolder.transform;
                //cEngine.updateWP(placeholder);
                BoxCollider boxColliderBarn1 = placeholder.AddComponent<BoxCollider>();
                boxColliderBarn1.size = new Vector3(0.1f, 0.1f, 0.1f);
                boxColliderBarn1.isTrigger = true;

                //second GameObject to barn
                GameObject barnWp = GameObject.CreatePrimitive(PrimitiveType.Quad);
                barnWp.name = "Barn";
                barnWp.transform.position = new Vector3(-2.5f, 0.02f, 1.0f);
                barnWp.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                barnWp.GetComponent<Renderer>().material.color = Color.red;
                barnWp.transform.Rotate(90, 0, 0);
                cEngine.wpCounter++;
                barnWp.transform.parent = PathHolder.transform;
                //cEngine.updateWP(barnWp);
                BoxCollider boxColliderBarn2 = barnWp.AddComponent<BoxCollider>();
                boxColliderBarn2.size = new Vector3(0.1f, 0.1f, 0.1f);
                boxColliderBarn2.isTrigger = true;
            }
            else
            {


                placeholder.transform.parent = PathHolder.transform;
                BoxCollider boxCollider = placeholder.AddComponent<BoxCollider>();
                boxCollider.isTrigger = true;
                boxCollider.size = new Vector3(0.1f, 0.1f, 0.1f);
                cEngine.wpCounter++;
                //cEngine.updateWP(placeholder);
            }

            /*
            var rigidbody = this.gameObject.AddComponent<Rigidbody>();
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }*/
        
    }
}