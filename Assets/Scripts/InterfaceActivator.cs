using UnityEngine;
using System.Collections;

public class InterfaceActivator : SequenceElement
{
    public MainAnim ui;
    public GameObject oui; 
    public float delay;
    public float duration;


	void OnEnable()
    {
        //ui.gameObject.SetActive(true);
        //ui.StartAnim();

        oui.SetActive(true);

        Invoke("Hide", duration);
    }

    void Hide()
    {
        //ui.FadeAnim();

        oui.gameObject.SetActive(false);

        isDone = true;
    }
}
