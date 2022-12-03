using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CreateObjects : MonoBehaviour
{
    // public GameObject CubePrefab;
    public List<GameObject> ObjectShapes;
    public GameObject objects;
    private GameObject WelcomePanel;
    private GameObject InGamePanel;
    private GameObject GameOverPanel;

    private void Start()
    {
        WelcomePanel = GameObject.Find("WelcomePanel");
        InGamePanel = GameObject.Find("InGamePanel");
        GameOverPanel = GameObject.Find("GameOverPanel");

        WelcomePanel.SetActive(true);
        InGamePanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    private bool isColorGreen(string objName)
    {
        Material status = GameObject.Find(objName).transform.Find("left").GetComponent<Renderer>().material;
        return status.name.Split(" ")[0] == "Green";
    }

    private bool IsGameOver()
    {

        return isColorGreen("CubeBin") &&
               isColorGreen("CuboidBin") &&
               isColorGreen("ConeBin") &&
               isColorGreen("CylinderBin") &&
               isColorGreen("PyramidBin") &&
               isColorGreen("SphereBin");
    }

    private void Update()
    {
        if (IsGameOver())
        {
            WelcomePanel.SetActive(false);
            InGamePanel.SetActive(false);
            GameOverPanel.SetActive(true);
        }
    }

    IEnumerator WaitForNext(int i)
    {
        if (i < ObjectShapes.Count)
        {
            yield return new WaitForSeconds(0.3f);
            GameObject clone = Instantiate(ObjectShapes[i], transform.position, Quaternion.identity);
            clone.transform.parent = objects.transform;
            clone.name = ObjectShapes[i].name;
            StartCoroutine(WaitForNext(i + 1));
        }
    }

    public void startGame()
    {
        Debug.Log("Game Started");
        StartCoroutine(WaitForNext(0));
        WelcomePanel.SetActive(false);
        InGamePanel.SetActive(true);
        GameOverPanel.SetActive(false);
    }

    public void RetryGame()
    {
        GameObject mainBucket = GameObject.Find("MainBucket");
        GameObject cubeBucket = GameObject.Find("CubeBucket");
        GameObject cuboidBucket = GameObject.Find("CuboidBucket");
        GameObject coneBucket = GameObject.Find("ConeBucket");
        GameObject cylinderBucket = GameObject.Find("CylinderBucket");
        GameObject sphereBucket = GameObject.Find("SphereBucket");
        GameObject pyramidBucket = GameObject.Find("PyramidBucket");

        Destroy(mainBucket, 2.0f);
        Destroy(cubeBucket, 2.0f);
        Destroy(cuboidBucket, 2.0f);
        Destroy(coneBucket, 2.0f);
        Destroy(cylinderBucket, 2.0f);
        Destroy(sphereBucket, 2.0f);
        Destroy(pyramidBucket, 2.0f);

        GameObject gameArea = GameObject.Find("GameArea");
        GameObject gameObject = new GameObject("MainBucket");

        gameObject.transform.parent = gameArea.transform;

        WelcomePanel.SetActive(true);
        InGamePanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }
}
