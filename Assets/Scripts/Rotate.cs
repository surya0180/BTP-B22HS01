using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    float speed = 50.0f;

    private void Start() {
        
    }
    private void Update() {
        transform.Rotate(Vector3.left * speed * Time.deltaTime);
    }

}
