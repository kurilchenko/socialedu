using UnityEngine;
using System.Collections;

public class Visitor : MonoBehaviour
{
    public bool isInLocalControl;
    public bool isRecording;
    public float interactionTime = 0.5f;
    public Transform cameraRig;
    [HideInInspector]
    public Sight sight;
    public GameObject avatar;

    public GameObject lastTarget;
    float elapesdTimeOnTarget;
    float targetInteractionTime;

    void Start()
    {
        //sight = GetComponent<Sight>();

        if (isInLocalControl)
        {
            StartLocalControl();
        }
        else
        {
            StartRemoteControl();
        }

        if (isRecording && isInLocalControl)
        {
            SetupRecording();
        }
    }

    void StartLocalControl()
    {
        if (UnityEngine.VR.VRSettings.enabled)
        {
            cameraRig.gameObject.GetComponent<MouseCameraControl>().enabled = false;
        }
        else
        {
            cameraRig.gameObject.GetComponent<MouseCameraControl>().enabled = true;
        }

        if (avatar != null)
        {
            avatar.SetActive(false);
        }
    }

    void StartRemoteControl()
    {
        cameraRig.gameObject.SetActive(false);
    }

    void Update()
    {
        /*
        if (lastTarget != sight.target)
        {
            elapesdTimeOnTarget = 0;
            CancelInvoke("InteractWithTarget");
            //sight.reticleInteraction.focus.completenes = 0;

            if (sight.target != null)
            {
                Invoke("InteractWithTarget", interactionTime);

                var thing = sight.target.GetComponent<InteractiveThing>();

                Debug.Log(sight.target.name + " " + thing);

                if (thing != null)
                {
                    targetInteractionTime = thing.interactionTime;
                }
            }
        }
        else if (sight.target != null)
        {
            elapesdTimeOnTarget += Time.deltaTime;

            var portion = elapesdTimeOnTarget / interactionTime;

            portion = portion > 1f ? 1f : portion;

            //sight.reticleInteraction.focus.completenes = portion;
        }

        lastTarget = sight.target;
        */

        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F12))
        {
            var currentAntiAliasing = QualitySettings.antiAliasing;
            QualitySettings.antiAliasing = 16;
            Application.CaptureScreenshot(Time.time.ToString()+".png", 4);
            QualitySettings.antiAliasing = currentAntiAliasing;
        }
        #endif
    }

    /*
    void InteractWithTarget()
    {
        if (sight.target == null)
            return;

        var thing = sight.target.GetComponent<InteractiveThing>();

        if (thing == null)
            return;

        thing.Interact();
    }
    */

    void SetupRecording()
    {
        var recordables = GetComponentsInChildren<Recordable>();

        foreach (var rec in recordables)
        {
            rec.Record();
        }

        Invoke("StopAllRecordings", FindObjectOfType<Awespace.Sequence>().Duration);
    }

    void StopAllRecordings()
    {
        var recordables = GetComponentsInChildren<Recordable>();

        foreach (var rec in recordables)
        {
            rec.StopRecording();
        }
    }

}
