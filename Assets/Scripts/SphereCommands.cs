using System.Collections.Generic;
using UnityEngine;

public class SphereCommands : MonoBehaviour {

    public List<GameObject> userNodes = new List<GameObject>();
    public Transform path;
    private GameObject userWPPrefab;

    // Use this for initialization
    void Start() {
        userWPPrefab = GameObject.Find("Waypoints");
    }

    /*called when user taps the barn. Sets the nodes for the truck rotation in the barn*/
    void OnBarnSelect() {
        GameObject ANode = GameObject.Find("ANode");
        GameObject BRotate = GameObject.Find("BRotate");
        GameObject BNode = GameObject.Find("BNode");
        userNodes.Add(ANode);
        userNodes.Add(BRotate);
        userNodes.Add(BNode);
    }

    /*gets the object that was tapped position from the raycast. Loads the position to a list*/
    void OnSelect() {
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hit;
        if(Physics.Raycast(headPosition, gazeDirection, out hit)) {
            GameObject wpToSet = GameObject.CreatePrimitive(PrimitiveType.Quad);
            wpToSet.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
            wpToSet.GetComponent<Renderer>().material.color = Color.blue;
            wpToSet.transform.Rotate(90, 0, 0);
            wpToSet.transform.parent = userWPPrefab.transform;
            wpToSet.name = "Node";
            float boardYLoc = AppManager.Instance.PlaneLocation.y;
            wpToSet.transform.position = new Vector3(hit.point.x, boardYLoc += 0.002f, hit.point.z);
            userNodes.Add(wpToSet);
        }
    }
}