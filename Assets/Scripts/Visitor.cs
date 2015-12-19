using UnityEngine;
using System.Collections;

public class Visitor : MonoBehaviour
{

    public Transform cameraRig;

    void Start()
    {
        if (UnityEngine.VR.VRSettings.enabled)
        {
            cameraRig.gameObject.GetComponent<MouseCameraControl>().enabled = false;
        }
        else
        {
            cameraRig.gameObject.GetComponent<MouseCameraControl>().enabled = true;
        }
    }

}
