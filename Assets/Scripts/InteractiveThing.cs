using UnityEngine;
using System.Collections;

public class InteractiveThing : MonoBehaviour
{
    public string eventName;

    public virtual void Interact()
    {
        Debug.Log(eventName);
    }
}
