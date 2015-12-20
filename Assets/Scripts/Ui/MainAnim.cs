using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainAnim : MonoBehaviour
{
    [HideInInspector]
    public float parentSpeed = 1f;

    public Ease alphaEase = Ease.Linear;
    public Ease elasticEase = Ease.Linear;
    public Ease lineEase = Ease.Linear;

    public RectTransform self;

    defaultTransform selfDefault;

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

        ResetAnim();

        StartAnim();
     }
    void ResetAnim()
    {
        startSequence.Kill();
        CanvasGroup selfAlpha = self.GetComponent<CanvasGroup>();

        selfAlpha.alpha = 0f;
        self.SetLocalPositionZ(300);
    }
    public void StartAnim()
	{
        ResetAnim();

        CanvasGroup selfAlpha = self.GetComponent<CanvasGroup>();

        startSequence = DOTween.Sequence();
        startSequence.Append(self.DOLocalMove(selfDefault.d_position, parentSpeed / 2).SetEase(elasticEase))
        .Insert(parentSpeed / 8, selfAlpha.DOFade(1f, parentSpeed / 4).SetEase(alphaEase));
    }
    public void FadeAnim()
    {
        CanvasGroup selfAlpha = self.GetComponent<CanvasGroup>();

        startSequence = DOTween.Sequence();
        startSequence.Append(self.DOLocalMoveZ(100, parentSpeed / 4).SetEase(lineEase))
        .Insert(parentSpeed / 4, selfAlpha.DOFade(0f, parentSpeed / 4).SetEase(alphaEase));
    }
}
