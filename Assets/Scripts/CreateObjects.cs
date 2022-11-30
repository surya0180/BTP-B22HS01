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

    }
}
