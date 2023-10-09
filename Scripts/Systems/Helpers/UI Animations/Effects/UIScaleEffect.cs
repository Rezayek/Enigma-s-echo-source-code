using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaleEffect : GenericSingleton<UIScaleEffect>
{
    [Range(0.1f, 5f)]
    [SerializeField] private float duration = 1.0f;

    private bool isScaling;

    public IEnumerator ScaleFromZero(Vector2 currentScale , Vector2 targetScale, RectTransform targetTransform)
    {
        targetTransform.sizeDelta = currentScale;
        yield return StartCoroutine(ScaleObjectCoroutine(currentScale, targetScale, targetTransform));
        targetTransform.sizeDelta = targetScale;
    }

    public IEnumerator ScaleToZero(Vector2 currentScale , Vector2 targetScale, RectTransform targetTransform)
    {

        yield return StartCoroutine(ScaleObjectCoroutine(currentScale, targetScale, targetTransform));
        targetTransform.sizeDelta = targetScale;
    }

    private IEnumerator ScaleObjectCoroutine(Vector2 startScale, Vector2 endScale, RectTransform targetTransform)
    {
        isScaling = true;

        float elapsedTime = 0f;

        while (isScaling)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            Vector2 newScale = Vector2.Lerp(startScale, endScale, t);

            targetTransform.sizeDelta = newScale;

            if (t >= 1f)
                isScaling = false;

            yield return null;
        }
    }
}
