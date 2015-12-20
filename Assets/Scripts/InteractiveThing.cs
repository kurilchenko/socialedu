using UnityEngine;
using System.Collections;

public class InteractiveThing : MonoBehaviour
{
    public string eventName;
    public float interactionTime = 0.5f;

    public virtual void Interact()
    {
        Debug.Log(eventName);
    }
}
