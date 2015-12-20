using UnityEngine;
using System.Collections;

public class ObjectActivator : SequenceElement
{
    public bool isTurningON = true;

    public float activateAfterSec = 0f;
    public GameObject target;

	void Start ()
    {
        Invoke("Activate", activateAfterSec);
	}

    void Activate()
    {
        target.SetActive(isTurningON);
        Debug.Log(target.name + " gets active.");
        isDone = true;
    }
	
}
