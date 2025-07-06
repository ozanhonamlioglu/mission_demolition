using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    private static MissionDemolition S;

    [SerializeField] private TextMeshProUGUI uitLevel;
    [SerializeField] private TextMeshProUGUI uitShots;
    [SerializeField] private Button uitButton;
    [SerializeField] private Vector3 castlePos;
    [SerializeField] private GameObject[] castles;


    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        S = this; // Define the Singleton
        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    private void Update()
    {
        UpdateGUI();
        if (mode == GameMode.playing && Goal.goalMet)
        {
            mode = GameMode.levelEnd;
            SwitchView("Show Both");
            Invoke("NextLevel", 2f);
        }
    }

    private void StartLevel()
    {
        if (castle) Destroy(castle);

        var gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (var pTemp in gos) Destroy(pTemp);

        castle = Instantiate(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;

        SwitchView("Show Slingshot");
        ProjectileLine.S.Clear();

        Goal.goalMet = false;
        UpdateGUI();
        mode = GameMode.playing;
    }

    private void UpdateGUI()
    {
        uitLevel.text = "Level: " + (level + 1) + "of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
    }

    private void NextLevel()
    {
        level++;
        if (level == levelMax) level = 0;
        StartLevel();
    }

    public void SwitchView(string eView = "")
    {
        var tmpText = uitButton.GetComponentInChildren<TMP_Text>();
        if (eView == "") eView = tmpText.text;
        showing = eView;
        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                tmpText.text = "Show Castle";
                break;
            case "Show Castle":
                FollowCam.POI = S.castle;
                tmpText.text = "Show Both";
                break;
            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                tmpText.text = "Show Slingshot";
                break;
        }
    }

    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}