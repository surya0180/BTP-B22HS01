using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CreateObjects : MonoBehaviour
{
    public GameObject CubePrefab;
    public GameObject objects;
    IEnumerator WaitForNext(int i)
    {
        yield return new WaitForSeconds(1);
        GameObject clone = Instantiate(CubePrefab , transform.position, Quaternion.identity);
        clone.transform.parent = objects.transform;
        clone.name = "Cube"+i;
    }
    public void startGame()
    {
        Debug.Log("Game Started");
        int i = 0;
        while (i < 5)
        {
            StartCoroutine(WaitForNext(i));
            i++;
        }
    }

}
