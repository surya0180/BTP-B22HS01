using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CreateObjects : MonoBehaviour
{
    // public GameObject CubePrefab;
    public List<GameObject> ObjectShapes;
    public GameObject objects;
    IEnumerator WaitForNext(int i)
    {
        if (i < ObjectShapes.Count)
        {
            yield return new WaitForSeconds(0.3f);
            GameObject clone = Instantiate(ObjectShapes[i], transform.position, Quaternion.identity);
            clone.transform.parent = objects.transform;
            clone.name = ObjectShapes[i].name + "_" + i;
            StartCoroutine(WaitForNext(i + 1));
        }
    }
    public void startGame()
    {
        Debug.Log("Game Started");
        StartCoroutine(WaitForNext(0));
    }
}
