using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestUIGaze : MonoBehaviour
{
    public MainAnim mainAnim;

	// Use this for initialization
	void Start () {
        StartCoroutine(CoStartQuestion());
    }

    IEnumerator CoStartQuestion()
    {
        yield return new WaitForSeconds(1f);

        mainAnim.parentSpeed = 3f;
        mainAnim.StartAnim();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

}
