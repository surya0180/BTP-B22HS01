using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerController : MonoBehaviour
{
    public Animator doorHingeAnimator;
    // Start is called before the first frame update
    void Start()
    {
        doorHingeAnimator = this.GetComponent<Animator>();
    }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    public void openDoor()
    {
        doorHingeAnimator.SetInteger("stateChange", 1);
        print("Hello");
    }
    public void closeDoor()
    {
        doorHingeAnimator.SetInteger("stateChange", 2);
        print("Hello");
    }
}
