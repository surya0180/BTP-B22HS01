using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CreateObjects : MonoBehaviour
{
    public Dictionary<string, Dictionary<string, bool>> GeometryShapes;
    private List<GameObject> AllObjects;
    public List<GameObject> CubeObjects;
    public List<GameObject> CuboidObjects;
    public List<GameObject> ConeObjects;
    public List<GameObject> CylinderObjects;
    public List<GameObject> SphereObjects;
    public List<GameObject> PyramidObjects;

    private GameObject WelcomePanel;
    private GameObject InGamePanel;
    private GameObject GameOverPanel;

    public GameObject MainBucket;
    public ObjectDetector CubeBucket;
    public ObjectDetector CuboidBucket;
    public ObjectDetector ConeBucket;
    public ObjectDetector CylinderBucket;
    public ObjectDetector SphereBucket;
    public ObjectDetector PyramidBucket;

    private void seed(string name, List<GameObject> list)
    {
        List<GameObject> temp = list;
        System.Random r = new System.Random();
        GeometryShapes.Add(name, new Dictionary<string, bool>());

        int seed_size = 1;

        for (int i = 0; i < seed_size; i++)
        {
            int rand = r.Next(0, temp.Count - 1);
            AllObjects.Add(temp[rand]);
            GeometryShapes[name].Add(temp[rand].name, false);
            temp.RemoveAt(rand);
        }
    }

    public void randomListGenerator()
    {
        seed("Cube", CubeObjects);
        seed("Cuboid", CuboidObjects);
        seed("Cone", ConeObjects);
        seed("Cylinder", CylinderObjects);
        seed("Sphere", SphereObjects);
        seed("Pyramid", PyramidObjects);
    }


    private void Start()
    {
        GeometryShapes = new Dictionary<string, Dictionary<string, bool>>();
        AllObjects = new List<GameObject>();

        WelcomePanel = GameObject.Find("WelcomePanel");
        InGamePanel = GameObject.Find("InGamePanel");
        GameOverPanel = GameObject.Find("GameOverPanel");

        WelcomePanel.SetActive(true);
        InGamePanel.SetActive(false);
        GameOverPanel.SetActive(false);
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

    private bool IsGameOver()
    {
        return isColorGreen("CubeBin") &&
               isColorGreen("CuboidBin") &&
               isColorGreen("ConeBin") &&
               isColorGreen("CylinderBin") &&
               isColorGreen("PyramidBin") &&
               isColorGreen("SphereBin");
    }

    private IEnumerator WaitForNext(int i)
    {
        if (i < AllObjects.Count)
        {
            yield return new WaitForSeconds(0.3f);
            GameObject clone = Instantiate(AllObjects[i], transform.position, Quaternion.identity);
            clone.transform.parent = MainBucket.transform;
            clone.name = AllObjects[i].name;
            StartCoroutine(WaitForNext(i + 1));
        }
    }

    public void startGame()
    {
        randomListGenerator();

        CubeBucket.setCurrentObjects();
        CuboidBucket.setCurrentObjects();
        ConeBucket.setCurrentObjects();
        CylinderBucket.setCurrentObjects();
        SphereBucket.setCurrentObjects();
        PyramidBucket.setCurrentObjects();


        Debug.Log("Game Started");
        StartCoroutine(WaitForNext(0));
        WelcomePanel.SetActive(false);
        InGamePanel.SetActive(true);
        GameOverPanel.SetActive(false);
    }

    private bool isColorGreen(string objName)
    {
        Material status = GameObject.Find(objName).transform.Find("left").GetComponent<Renderer>().material;
        return status.name.Split(" ")[0] == "Green";
    }

    public void RetryGame()
    {
        GeometryShapes.Clear();
        AllObjects.Clear();

        CubeBucket.deleteBucket();
        CuboidBucket.deleteBucket();
        ConeBucket.deleteBucket();
        CylinderBucket.deleteBucket();
        SphereBucket.deleteBucket();
        PyramidBucket.deleteBucket();

        GameObject gameArea = GameObject.Find("GameArea");
        GameObject gameObject = new GameObject("MainBucket");

        gameObject.transform.parent = gameArea.transform;

        WelcomePanel.SetActive(true);
        InGamePanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }
}
