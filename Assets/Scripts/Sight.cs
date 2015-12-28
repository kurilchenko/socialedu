using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sight : MonoBehaviour
{
    public Transform anchor;
    public Reticle reticle;
    public Reticle reticleInteraction;
    public float minAngleForVisible = 15f;
    public GameObject target;
    public LayerMask layerMask = 1;

    public RaycastHit hitInfo;

    public Vector3 facingVector
    {
        get
        {
            return anchor.forward;
        }
    }

	void Start ()
    {
        reticle.SetBodyOpacity(0.3f);
        reticleInteraction.focus.completenes = 0;  	
	}
	
	void Update ()
    {
        Physics.Raycast(anchor.transform.position, anchor.forward, out hitInfo);

        UpdateReticle(hitInfo);
        UpdateTarget();
    }

    void UpdateReticle(RaycastHit hitInfo)
    {
        reticle.transform.LookAt(anchor.transform.position);
        var reticleDistance = hitInfo.collider != null ? hitInfo.distance : 1f;
        Vector3 targetPosition = hitInfo.collider != null ? hitInfo.point : anchor.position + anchor.forward * 1f;

        reticle.SetFocus(targetPosition, reticle.originalScale * reticleDistance);

        reticleInteraction.transform.LookAt(anchor.transform.position);
        reticleInteraction.SetFocus(targetPosition, reticle.originalScale * reticleDistance);
    }

    void UpdateTarget()
    {
        target = hitInfo.collider != null ? hitInfo.collider.gameObject : null;
    }

    void UpdateIndicator()
    {

    }

    float GetAngleBetweenFocusPointAndPosition(Vector3 position)
    {
        var anchorPosition = anchor.position;
        var targetDir = position - anchorPosition;
        return Vector3.Angle(targetDir, anchor.forward);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
    }


}
