using UnityEngine;
using System.Collections;

public class Recordable : MonoBehaviour
{
    protected bool isRecording;

    public virtual void Record()
    {
        isRecording = true;
    }

    public virtual void StopRecording()
    {
        isRecording = false;
    }

    protected virtual void Start()
    {
        
    }

}
