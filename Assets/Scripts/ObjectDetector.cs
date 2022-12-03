using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    // Start is called before the first frame update
    public Material GreenMaterial;
    public Material RedMaterial;
    public Material WhiteMaterial;
    Dictionary<string, bool> currentObjects;
    Dictionary<string, bool> otherObjects;

    GameObject currentBucket;
    GameObject mainBucket;
    GameObject currentBin;

    Dictionary<string, bool> getMappingFromTag()
    {
        Dictionary<string, bool> map = new Dictionary<string, bool>();
        switch (this.tag)
        {
            case "Cube":
                map["Dice"] = true;
                map["RubiksCube"] = true;
                return map;
            default:
                return map;
        }
    }

    void Start()
    {
        currentObjects = getMappingFromTag();
        otherObjects = new Dictionary<string, bool>();

        currentBucket = new GameObject($"{this.tag}Objects");
        mainBucket = GameObject.Find("MainBucket");

        currentBin = GameObject.Find($"{this.tag}Bin");

        currentBucket.transform.position = currentBin.transform.position;
        currentBucket.transform.parent = currentBin.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Renderer currentRenderer = currentBin.transform.Find("left").GetComponent<Renderer>();
        if (otherObjects.Count != 0)
        {
            currentRenderer.material = RedMaterial;
            return;
        }
        List<string> keys = currentObjects.Keys.ToList();
        foreach (var key in keys)
        {
            if (!currentObjects[key])
            {
                currentRenderer.material = WhiteMaterial;
                return;
            }
        }
        currentRenderer.material = GreenMaterial;
        return;
    }

    public void OnTriggerEnter(Collider other)
    {
        string name = other.gameObject.name;
        if (currentObjects.ContainsKey(other.gameObject.name))
        {
            currentObjects[name] = true;
        }
        else
        {
            otherObjects[name] = true;
        }
        other.gameObject.transform.parent = currentBucket.transform;
    }

    public void OnTriggerExit(Collider other)
    {
        string name = other.gameObject.name;
        if (currentObjects.ContainsKey(other.gameObject.name))
        {
            currentObjects[name] = false;
        }
        else
        {
            otherObjects.Remove(name);
        }
        other.gameObject.transform.parent = mainBucket.transform;
    }
}
