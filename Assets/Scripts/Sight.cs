using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sight : MonoBehaviour
{
    public Transform anchor;
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

	}
	
	void Update ()
    {
        Physics.Raycast(anchor.transform.position, anchor.forward, out hitInfo);

        UpdateTarget();
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
