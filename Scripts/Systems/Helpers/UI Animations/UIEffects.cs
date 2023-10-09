using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GameEventListener))]
[RequireComponent(typeof(UIFadeEffect))]
[RequireComponent(typeof(UIMoveEffect))]
[RequireComponent(typeof(UIScaleEffect))]
public class UIEffects : MonoBehaviour
{

    private UIFadeEffect fadeEffect;
    private UIMoveEffect moveEffect;
    private UIScaleEffect scaleEffect;

    // Start is called before the first frame update
    void Awake()
    {
        fadeEffect = UIFadeEffect.Instance;
        moveEffect = UIMoveEffect.Instance;
        scaleEffect = UIScaleEffect.Instance;
    }

    public void PlayFade(Component sender, List<object> data)
    {
        if (data.Count != 3)
            return;
        if (data[0] is not UIAnimations.Fade)
            return;
        UIAnimations fadeType = (UIAnimations)data[1];
        CanvasGroup canvasGroup = (CanvasGroup)data[2];
        if (fadeType == UIAnimations.FadeIn)
            StartCoroutine(fadeEffect.FadeIn(canvasGroup));
        else if (fadeType == UIAnimations.FadeOut)
            StartCoroutine(fadeEffect.FadeOut(canvasGroup));
        else if (fadeType == UIAnimations.FadeInFadeOut)
            StartCoroutine(fadeEffect.FadeInFadeOut(canvasGroup));
    }

    public void PlayMove(Component sender, List<object> data)
    {
        if (data.Count != 5)
            return;
        if (data[0] is not UIAnimations.Move)
            return;

        UIAnimations moveType = (UIAnimations)data[1];
        Vector3 startPoint = (Vector3)data[2];
        Vector3 endPoint = (Vector3)data[3];
        RectTransform transform = (RectTransform)data[4];
        if (moveType == UIAnimations.MoveIn)
            StartCoroutine(moveEffect.MoveInOrMoveOut(startPoint, endPoint, transform));
        if (moveType == UIAnimations.MoveOut)
            StartCoroutine(moveEffect.MoveInOrMoveOut(endPoint, startPoint, transform));
        if (moveType == UIAnimations.MoveInOut)
            StartCoroutine(moveEffect.MoveInMoveOut(startPoint, endPoint, transform));
    }

    public void PlayScale(Component sender, List<object> data)
    {
        if (data.Count != 5)
            return;
        if (data[0] is not UIAnimations.Scale)
            return;
        UIAnimations scaleType = (UIAnimations)data[1];
        Vector2 currentScale = (Vector2)data[2];
        Vector2 targetScale = (Vector2)data[3];
        RectTransform transform = (RectTransform)data[4];
        if (scaleType == UIAnimations.ScaleIn)
            StartCoroutine(scaleEffect.ScaleFromZero(currentScale, targetScale,  transform));
        if(scaleType == UIAnimations.ScaleOut)
            StartCoroutine(scaleEffect.ScaleToZero(targetScale, currentScale, transform));

    }
}
