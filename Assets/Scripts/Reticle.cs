using UnityEngine;
using System.Collections;

public class Reticle : MonoBehaviour {

    public Vector3 originalScale;
    public Vector3 bodyOriginalScale;
    public Circle focus;
    public Transform anchor;
    public LayerMask layerMask;
    public Collider targetCollider;
    public Vector3 facingVector;
    public Transform body;

    float targetBodyScale = 1f;
	float targetBodyOpacity = 1f;
	Vector3 targetPosition;
	Vector3 targetScale;

    public void SetBodyScale(float value)
    {
        if (targetBodyScale == value)
            return;

        targetBodyScale = value;

        var targetScale = new Vector3(bodyOriginalScale.x * value, bodyOriginalScale.y * value, bodyOriginalScale.z);
        LeanTween.cancel(body.gameObject);
        LeanTween.scale(body.gameObject, targetScale, 0.15f);
    }

    // Temp hack.
    public void ResetTargetBodyScale()
    {
        targetBodyScale = 0.999f;
    }

    public void SetBodyOpacity(float alpha)
    {
        if (targetBodyOpacity == alpha)
            return;

        targetBodyOpacity = alpha;

        LeanTween.alpha(body.gameObject, alpha, 0.15f);
    }

    void Start ()   {
		originalScale = transform.localScale;
		bodyOriginalScale = body.localScale;
	}

	void Update() {
		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 25f);
		transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 25f);
	}

	public void SetBody(int index) {
		body = focus.transform;
		body.gameObject.SetActive(true);
	}

	public void SetFocus(Vector3 position, Vector3 scale) {
		targetPosition = position;
		targetScale = scale;
	}

    //public void SetTangents()
}