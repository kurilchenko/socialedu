using UnityEngine;
using System.Collections;

public class Visitor : MonoBehaviour
{
    public bool isInControl;
    public float interactionTime = 0.5f;
    public Transform cameraRig;
    [HideInInspector]
    public Sight sight;

    public GameObject lastTarget;
    float elapesdTimeOnTarget;
    float targetInteractionTime;

    void Start()
    {
        sight = GetComponent<Sight>();

        if (UnityEngine.VR.VRSettings.enabled)
        {
            cameraRig.gameObject.GetComponent<MouseCameraControl>().enabled = false;
        }
        else
        {
            cameraRig.gameObject.GetComponent<MouseCameraControl>().enabled = true;
        }
    }

    void Update()
    {
        if (lastTarget != sight.target)
        {
            elapesdTimeOnTarget = 0;
            CancelInvoke("InteractWithTarget");
            sight.reticleInteraction.focus.completenes = 0;

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

            sight.reticleInteraction.focus.completenes = portion;
        }

        lastTarget = sight.target;
    }

    void InteractWithTarget()
    {
        if (sight.target == null)
            return;

        var thing = sight.target.GetComponent<InteractiveThing>();

        if (thing == null)
            return;

        thing.Interact();
    }

}
