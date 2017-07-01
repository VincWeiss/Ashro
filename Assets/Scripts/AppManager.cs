using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppManager : Singleton<AppManager>
{
    Vector3 GameLocation = new Vector3();
    public string StartingScene = "MainMenu";
    private string filePath;
    private int levelCount=1;
    public Text HighscorePlayerName;
    public Dictionary<string, int> Highscorelist;
    

    enum scenes{
        Level1,
        Level2,
        Level3,
        Level4
    }

	// Use this for initialization
	IEnumerator Start () {
        //Entsprechend Kommentiert!
        //highscore = 200;

            filePath = Path.Combine(Application.persistentDataPath, "Highscore.txt");
            readHighscore();

        yield return StartCoroutine(LoadandSetActive(StartingScene));
    }
	
    public IEnumerator LoadandSetActive(string scenename)
    {
        yield return SceneManager.LoadSceneAsync(scenename, LoadSceneMode.Additive);
        Scene loaded = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(loaded);
    }
    public IEnumerator switchScenes(string scenename)
    {
        //Costs
        if (scenename.Contains("Level")){
            switch (scenename)
            {
                case "Level1":
                    costs = 50.0f;
                    break;
                case "Level2":
                    costs = 75.0f;
                    break;
                case "Level3":
                    costs = 100.0f;
                    break;
                case "Level4":
                    costs = 125.0f;
                    break;
            }
        }
        if (scenename.Equals("MainMenu")&& highscore!=0)
        {
            HighscorePlayerName.enabled = true;

            writeHighscore("Player1",highscore);
        }
        yield return SceneManager.LoadSceneAsync(scenename, LoadSceneMode.Additive);
        Scene loaded = SceneManager.GetSceneByName(scenename);
        Scene current = SceneManager.GetActiveScene();

        yield return null;

        SceneManager.SetActiveScene(loaded);

        yield return null;

        yield return SceneManager.UnloadSceneAsync(current.buildIndex);
    }
    
    public Vector3  PanelLocation { get; set; }

    public Quaternion PanelRotation { get; set; }

    public Vector3 PlaneLocation { get; set; }

    public Quaternion PlaneRotation { get; set; }

    public Vector3 PlaneScale { get; set; }

    public Transform HighscorePanel { get; set; }

    public Transform UpgradePanel { get; set; }

    public float costs { get; set; }

    public static int highscore { get; set; }

    public string Levelname { get; set; }

    public void writeHighscore(string name, int score)
    {
        if (Highscorelist.ContainsKey(name))
        {
            Highscorelist[name] = score;
        }
        else
        {
            Highscorelist.Add(name, score);

        }

        Highscorelist = Highscorelist.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

        string[] lines = new string[Highscorelist.Values.Count];
        int i = 0;
        foreach (var pair in Highscorelist)
        {
            lines[i++] = string.Format("{0}:{1}", pair.Key, pair.Value); 
        }

        System.IO.File.WriteAllLines(filePath, lines);
    }

    public void readHighscore()
    {
        Highscorelist = new Dictionary<string, int>();
        try
        {
            string[] Score = System.IO.File.ReadAllLines(filePath);
            for (int i = 0; i < Score.Length; i++)
            {
                string[] split = Score[i].Split(':');

                Highscorelist.Add(split[0], int.Parse(split[1]));
            }
        }
        catch (IOException e)
        {
        }
    }
}
