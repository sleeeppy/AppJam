using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int stage;
    public Animator stageAnim;
    public Animator clearAnim;
    public Animator fadeAnim;
    public Transform playerPos;

    public string[] fishObjs;
    public Transform[] spawnPoints;

    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public TextMeshProUGUI scoreText;
    public Image[] lifeImage;
    public GameObject gameOverSet;
    public ObjectManager objectManager;

    public List<Spawn> spawnList;
    public int spawni;
    public bool spawnEnd;


    void Awake()
    {
        spawnList = new List<Spawn>();
        fishObjs = new string[] { "fishA", "fishB", "fishC", "fishE", "sharkA", "sharkB", "sharkC" };
        StageStart();
    }

    public void StageStart()
    {
        Time.timeScale = 1f;
        stageAnim.SetTrigger("On");
        stageAnim.GetComponent<TextMeshProUGUI>().text = "Stage " + stage + "\nStart";
        clearAnim.GetComponent<TextMeshProUGUI>().text = "Stage " + stage + "\nClear!";

        ReadSpawnFile();
        fadeAnim.SetTrigger("In");
        if(stage == 3)
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.speed = 6;
        }
    }

    public void StageEnd()
    {
        clearAnim.SetTrigger("On");
        fadeAnim.SetTrigger("Out");

        player.transform.position = playerPos.position;

        stage++;
        if (stage < 2)
            Invoke("GameOver", 6);
        else
            Invoke("StageStart", 5);
    }

    void ReadSpawnFile()
    {
        spawnList.Clear();
        spawni = 0;
        spawnEnd = false;

        TextAsset textFile = Resources.Load("Stage " + stage) as TextAsset;
        StringReader stringreader = new StringReader(textFile.text);

        while (stringreader != null)
        {
            string line = stringreader.ReadLine();
            Debug.Log(line);

            if (line == null)
                break;
            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        stringreader.Close();
        nextSpawnDelay = spawnList[0].delay;

    }
    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnFish();
            curSpawnDelay = 0;
        }

        Player playerLogic = player.GetComponent<Player>();

        // Formatting
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }

    void SpawnFish()
    {
        int fishi = 0;
        switch (spawnList[spawni].type)
        {
            case "fishA":
                fishi = 0;
                break;
            case "fishB":
                fishi = 1;
                break;
            case "fishC":
                fishi = 2;
                break;
            case "fishE":
                fishi = 3;
                break;
            case "sharkA":
                fishi = 4;
                break;
            case "sharkB":
                fishi = 5;
                break;
            case "sharkC":
                fishi = 6;
                break;
        }

        // Spawning Enemies
        int fishPoint = spawnList[spawni].point;
        GameObject fish = objectManager.MakeObj(fishObjs[fishi]);
        fish.transform.position = spawnPoints[fishPoint].position;

        Rigidbody rigid = fish.GetComponent<Rigidbody>();
        Fish fishLogic = fish.GetComponent<Fish>();
        fishLogic.player = player;
        fishLogic.gameManager = this;
        fishLogic.objectManager = objectManager;

        if (fishPoint == 0 || fishPoint == 1 || fishPoint == 2)
        {
            fish.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector3(2, 0, fishLogic.speed * (-1));
        }
        else if (fishPoint == 10 || fishPoint == 11 || fishPoint == 12)
        {
            fish.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector3(-2, 0, fishLogic.speed * (-1));
        }
        else
        {
            rigid.velocity = new Vector3(0, 0, fishLogic.speed * (-1));
        }

        spawni++;
        if (spawni == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        nextSpawnDelay = spawnList[spawni].delay;

    }
    public void UpdateLifeIcon(int life)
    {
        // Life icon set
        for (int i = 0; i < 3; i++)
        {
            lifeImage[i].color = new Color(1, 1, 1, 0);
        }

        for (int i = 0; i < life; i++)
        {
            lifeImage[i].color = new Color(1, 1, 1, 1);
        }
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(1);
    }

    public void ToGame()
    {
        SceneManager.LoadScene(1);
    }

    public void FinishGame()
    {
        Application.Quit();
    }


}
