using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LvlManager : MonoBehaviour
{
    public Button nextLvl;
    CarEngine cEngine = new CarEngine();

    void Start()
    {
        Button btn = nextLvl.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {

        Debug.Log("You have clicked the button!");
    }
}