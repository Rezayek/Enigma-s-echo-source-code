using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFadeEffect : GenericSingleton<UIFadeEffect>
{
    [SerializeField] private float fadeInDuration = 1.0f;
    [SerializeField] private float fadeOutDuration = 0.3f;

    [SerializeField] private float delayDuration = 0.5f;

    public IEnumerator FadeInFadeOut(CanvasGroup canvasGroup)
    {
        yield return FadeIn(canvasGroup);
        yield return new WaitForSeconds(delayDuration);
        yield return FadeOut(canvasGroup);
    }

    public IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0.0f, 1.0f, elapsedTime / fadeInDuration);
            canvasGroup.alpha = alpha;
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1.0f;
    }

    public IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1.0f, 0.0f, elapsedTime / fadeOutDuration);
            canvasGroup.alpha = alpha;
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0.0f;
    }
}
