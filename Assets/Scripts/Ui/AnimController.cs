using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimController : MonoBehaviour
{
    public MainAnim start = null;
    public MainAnim task1 = null;

    public MainAnim question1 = null;
    public MainAnim answer1 = null;

    public ShareAnim share = null;

    public float sequenceSpeed = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
           StartAnim();
        }
    }
    public void StartAnim()
    {
        start.parentSpeed = sequenceSpeed;
        task1.parentSpeed = sequenceSpeed;
        question1.parentSpeed = sequenceSpeed;
        answer1.parentSpeed = sequenceSpeed;
        share.parentSpeed = sequenceSpeed;

        start.StartAnim();
        task1.StartAnim();
        question1.StartAnim();
        answer1.StartAnim();
        share.StartAnim();
    }

}
