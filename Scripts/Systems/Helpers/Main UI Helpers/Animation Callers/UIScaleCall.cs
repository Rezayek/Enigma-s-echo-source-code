using System.Collections.Generic;
using UnityEngine;

public class UIScaleCall : MonoBehaviour
{
    [SerializeField] private GameObject objectToAnimate;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 startScale;
    [SerializeField] private Vector2 endScale;
    [SerializeField] private GameEvent UIEffectCall;

    private void Start()
    {
        PlayScale();
    }

    private void PlayScale()
    {
        objectToAnimate.SetActive(true);
        UIEffectCall.Raise(this, new List<object> { UIAnimations.Scale, UIAnimations.ScaleIn, startScale, endScale, rectTransform });
    }
}
