using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject fishAPrefab;
    public GameObject fishBPrefab;
    public GameObject fishCPrefab;
    public GameObject sharkAPrefab;
    public GameObject sharkBPrefab;
    public GameObject sharkCPrefab;

    GameObject[] fishA;
    GameObject[] fishB;
    GameObject[] fishC;
    GameObject[] sharkA;
    GameObject[] sharkB;
    GameObject[] sharkC;

    GameObject[] targetPool;

    void Awake()
    {
        fishA = new GameObject[30];
        fishB = new GameObject[30];
        fishC = new GameObject[30];
        sharkA = new GameObject[30];
        sharkB = new GameObject[30];
        sharkC = new GameObject[30];

        Generate();
    }

    void Generate()
    {
        for (int i = 0; i < fishA.Length; i++)
        {
            fishA[i] = Instantiate(fishAPrefab);
            fishA[i].SetActive(false);
        }

        for (int i = 0; i < fishB.Length; i++)
        {
            fishB[i] = Instantiate(fishBPrefab);
            fishB[i].SetActive(false);
        }

        for (int i = 0; i < fishC.Length; i++)
        {
            fishC[i] = Instantiate(fishCPrefab);
            fishC[i].SetActive(false);
        }

        for (int i = 0; i < sharkA.Length; i++)
        {
            sharkA[i] = Instantiate(sharkAPrefab);
            sharkA[i].SetActive(false);
        }

        for (int i = 0; i < sharkB.Length; i++)
        {
            sharkB[i] = Instantiate(sharkBPrefab);
            sharkB[i].SetActive(false);
        }

        for (int i = 0; i < sharkC.Length; i++)
        {
            sharkC[i] = Instantiate(sharkCPrefab);
            sharkC[i].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "fishA":
                targetPool = fishA;
                break;
            case "fishB":
                targetPool = fishB;
                break;
            case "fishC":
                targetPool = fishC;
                break;
            case "sharkA":
                targetPool = sharkA;
                break;
            case "sharkB":
                targetPool = sharkB;
                break;
            case "sharkC":
                targetPool = sharkC;
                break;
        }

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }

        return null;
    }

    public GameObject[] GetPool(string type)
    {
        switch(type)
        {
            case "fishA":
                targetPool = fishA;
                break;
            case "fishB":
                targetPool = fishB;
                break;
            case "fishC":
                targetPool = fishC;
                break;
            case "sharkA":
                targetPool = sharkA;
                break;
            case "sharkB":
                targetPool = sharkB;
                break;
            case "sharkC":
                targetPool = sharkC;
                break;
        }

        return targetPool;
    }
}
 