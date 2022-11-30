using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeDetector : MonoBehaviour
{
    // Start is called before the first frame update
    public Material GreenMaterial;
    public Material RedMaterial;
    public Material WhiteMaterial;
    Dictionary<string, bool> cubes;
    Dictionary<string, bool> others;
    void Start()
    {
        cubes = new Dictionary<string, bool>();
        others = new Dictionary<string, bool>();

        cubes["Dice"] = false;
        cubes["RubiksCube"] = false;
    }

    // Update is called once per frame
    void Update()
    {
        Renderer CubeRend = GameObject.Find("CubeBin").transform.Find("left").GetComponent<Renderer>();
        if (others.Count != 0)
        {
            CubeRend.material = RedMaterial;
            return;
        }
        List<string> keys = cubes.Keys.ToList();
        foreach (var key in keys)
        {
            if (!cubes[key])
            {
                CubeRend.material = WhiteMaterial;
                return;
            }
        }
        CubeRend.material = GreenMaterial;
        return;
    }

    public void OnTriggerEnter(Collider other)
    {
        string name = other.gameObject.name;
        if (cubes.ContainsKey(other.gameObject.name))
        {
            cubes[name] = true;
        }
        else
        {
            others[name] = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        string name = other.gameObject.name;
        if (cubes.ContainsKey(other.gameObject.name))
        {
            cubes[name] = false;
        }
        else
        {
            others.Remove(name);
        }
    }
}
