using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectArea : MonoBehaviour
{
    public Vector2 dimensions = new Vector2(1f, 1f);
    public Transform reticlePrefab;
    public GameObject body;

    Transform reticle;
    new BoxCollider collider;
    Sight sight;
    GraphicRaycaster raycaster;

    void Start()
    {
        collider = gameObject.AddComponent<BoxCollider>();

        GetComponent<BoxCollider>().size = new Vector3(dimensions.x, 0.001f, dimensions.y);
        
        reticle = Instantiate(reticlePrefab) as Transform;
        reticle.transform.position = transform.position;
        reticle.transform.parent = transform;
        reticle.rotation = transform.rotation;

        foreach (var visitor in FindObjectsOfType<Visitor>())
        {
            if (visitor.isInLocalControl)
            {
                sight = visitor.GetComponent<Sight>();
            }
        }

        raycaster = transform.root.GetComponent<GraphicRaycaster>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        body.SetActive(false);
    }

    public void Open()
    {
        body.SetActive(true);
        reticle.gameObject.SetActive(true);
    }

    public void Close()
    {
        body.SetActive(false);
        reticle.gameObject.SetActive(false);

        FindObjectOfType<Awespace.Sequence>().Play();
    }

    void OnDrawGizmos()
    {
        //Gizmos.matrix = transform.localToWorldMatrix;
        //Gizmos.DrawWireCube(transform.position, new Vector3(dimensions.x, 0.001f, dimensions.y));
    }

    public float TimeSinceFocusedOnArea { get; set; }
    public float ElapsedTimeSinceLastFocus
    {
        get
        {
            return TimeSinceFocusedOnArea != 0f ? Time.timeSinceLevelLoad - TimeSinceFocusedOnArea : float.MaxValue;
        }
    }
    public float TimeSinceMovedReticle { get; private set; }
    public float ElapsedTimeSinceMovedReticle
    {
        get
        {
            return TimeSinceMovedReticle != 0f ? Time.timeSinceLevelLoad - TimeSinceMovedReticle : float.MaxValue;
        }
    }

    void OnEnable()
    {
        TimeSinceFocusedOnArea = Time.timeSinceLevelLoad;
        TimeSinceMovedReticle = Time.timeSinceLevelLoad;
    }

    void Update()
    {
        if (sight == null)
            return;

        var relativePointAtDistance = sight.hitInfo.collider != null ? sight.hitInfo.point : collider.transform.position;
        var closestPositionToBounds = sight.anchor.position + sight.facingVector * Vector3.Distance(sight.anchor.position, relativePointAtDistance);
        var closestPointOnBounds = collider.ClosestPointOnBounds(closestPositionToBounds);

        if (Vector3.Distance(closestPointOnBounds, closestPositionToBounds) <= 0.01f)
        {
            TimeSinceFocusedOnArea = Time.timeSinceLevelLoad;
        }

        #if UNITY_ANDROID
		//reticle.transform.position = closestPointOnBounds;	
		reticle.transform.position = Vector3.Lerp(reticle.transform.position, closestPointOnBounds, 30f);
        #endif

        #if UNITY_STANDALONE

        // If VR isn't enabled and Alt key isn't pressed--don't move the cursor by the mouse.
        if (!UnityEngine.VR.VRSettings.enabled && !Input.GetKey(KeyCode.LeftAlt))
            return;

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            TimeSinceMovedReticle = Time.timeSinceLevelLoad;
        }

        if (Input.GetAxis("Mouse X") != 0)
        {
            var localRight = reticle.transform.InverseTransformDirection(reticle.transform.right);

            reticle.transform.localPosition = reticle.transform.localPosition + localRight * Input.GetAxis("Mouse X") * 0.01f;
        }
        if (Input.GetAxis("Mouse Y") != 0)
        {
            var localForward = reticle.transform.InverseTransformDirection(reticle.transform.forward);
            reticle.transform.localPosition = reticle.transform.localPosition + localForward * Input.GetAxis("Mouse Y") * 0.01f;
        }

        reticle.transform.position = collider.bounds.ClosestPoint(reticle.transform.position);

        if (Input.GetMouseButtonDown(0))
        {
            /*
            RaycastHit hitInfo;
            Physics.Raycast(sight.anchor.position, reticle.transform.position - sight.anchor.position, out hitInfo, sight.layerMask);

            if (hitInfo.collider != null)
            {
                var target = hitInfo.rigidbody != null ? hitInfo.rigidbody.gameObject : hitInfo.collider.gameObject;

                if (target != null)
                {
                    target.SendMessage("OnClick", hitInfo.point, SendMessageOptions.DontRequireReceiver);
                }
            }
            */

            var screenPoint = sight.anchor.GetComponent<Camera>().WorldToScreenPoint(reticle.transform.position);
            var ped = new PointerEventData(null);
            var results = new List<RaycastResult>();

            transform.root.GetComponent<EventSystem>().RaycastAll(ped, results);

            //Debug.Log();

            //raycaster.Raycast(ped, results);

            Close();
        }
        #endif

    }

}
