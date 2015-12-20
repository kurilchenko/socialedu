using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShareAnim : MonoBehaviour
{
    [HideInInspector]
    public float parentSpeed = 1f;

    public Ease alphaEase = Ease.Linear;
    public Ease elasticEase = Ease.Linear;
    public Ease lineEase = Ease.Linear;

    public RectTransform self;
    public RectTransform text1;
    public RectTransform text2;
    public RectTransform button;

    defaultTransform selfDefault;
    defaultTransform text1Default;
    defaultTransform text2Default;
    defaultTransform buttonDefault;

    public Sequence startSequence;

    public struct defaultTransform
    {
        public Vector3 d_position;
        public Quaternion d_rotation;
        public Vector3 d_scale;
        public void SetParam(Transform parent)
        {
            d_position = parent.localPosition;
            d_rotation = parent.localRotation;
            d_scale = parent.localScale;
        }
        public void GetParam(Transform parent)
        {
            parent.localPosition = d_position;
            parent.localRotation = d_rotation;
            parent.localScale = d_scale;
        }
    }
    void Start()
     {
        selfDefault.SetParam(self);
        text1Default.SetParam(text1);
        text2Default.SetParam(text2);
        buttonDefault.SetParam(button);

        ResetAnim();
     }
    void ResetAnim()
    {
        startSequence.Kill();
        CanvasGroup buttonAlpha = button.GetComponent<CanvasGroup>();
        CanvasGroup text1Alpha = text1.GetComponent<CanvasGroup>();
        CanvasGroup text2Alpha = text2.GetComponent<CanvasGroup>();

        buttonAlpha.alpha = 0f;
        button.SetLocalPositionZ(300);

        text1Alpha.alpha = 0f;
        text1.SetLocalPositionZ(100);

        text2Alpha.alpha = 0f;
        text2.SetLocalPositionZ(100);
    }
    public void StartAnim()
	{
        ResetAnim();

        CanvasGroup buttonAlpha = button.GetComponent<CanvasGroup>();

        startSequence = DOTween.Sequence();
        startSequence.Append(button.DOLocalMove(buttonDefault.d_position, parentSpeed / 2).SetEase(elasticEase))
        .Insert(parentSpeed / 8, buttonAlpha.DOFade(1f, parentSpeed / 4).SetEase(alphaEase));
    }
    public void ButtonAnim()
    {
        CanvasGroup text1Alpha = text1.GetComponent<CanvasGroup>();
        CanvasGroup text2Alpha = text2.GetComponent<CanvasGroup>();

        startSequence = DOTween.Sequence();
        startSequence.Append(text1.DOLocalMove(text1Default.d_position, parentSpeed / 2).SetEase(elasticEase))
        .Insert(parentSpeed / 8, text1Alpha.DOFade(1f, parentSpeed / 4).SetEase(alphaEase))
        .Insert(parentSpeed / 8, text2.DOLocalMove(text2Default.d_position, parentSpeed / 2).SetEase(elasticEase))
        .Insert(parentSpeed / 6, text2Alpha.DOFade(1f, parentSpeed / 4).SetEase(alphaEase));
    }
    public void FadeAnim()
    {
        CanvasGroup selfAlpha = self.GetComponent<CanvasGroup>();

        startSequence = DOTween.Sequence();
        startSequence.Append(self.DOLocalMoveZ(100, parentSpeed / 4).SetEase(lineEase))
        .Insert(parentSpeed / 4, selfAlpha.DOFade(0f, parentSpeed / 4).SetEase(alphaEase));
    }
}
