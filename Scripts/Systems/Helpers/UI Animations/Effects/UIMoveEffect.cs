using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoveEffect : GenericSingleton<UIMoveEffect>
{
    [Range(0.1f, 5f)]
    [SerializeField] private float duration = 1.0f;


    private bool isLerping;
    public IEnumerator MoveInMoveOut(Vector3 startPosition, Vector3 targetPosition, RectTransform transform)
    {
        // Move the object forward
        yield return StartCoroutine(MoveObjectCoroutine(startPosition, targetPosition, transform));
        transform.localPosition = targetPosition;

        // Move the object backward
        yield return StartCoroutine(MoveObjectCoroutine(targetPosition, startPosition, transform));
        transform.localPosition = startPosition;
    }

    public IEnumerator MoveInOrMoveOut(Vector3 startPosition, Vector3 targetPosition, RectTransform transform)
    {
        yield return StartCoroutine(MoveObjectCoroutine(startPosition, targetPosition, transform));
        transform.localPosition = targetPosition;
    }

    private IEnumerator MoveObjectCoroutine(Vector3 startPosition, Vector3 targetPosition, RectTransform transform)
    {
        isLerping = true;

        float elapsedTime = 0f;
        Vector2 start = new Vector2(startPosition.x, startPosition.y);
        Vector2 end = new Vector2(targetPosition.x, targetPosition.y);

        while (isLerping)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            Vector2 newPosition = Vector2.Lerp(start, end, t);

            transform.localPosition = new Vector3(newPosition.x, newPosition.y, 0);

            if (t >= 1f)
                isLerping = false;

            yield return null;
        }
    }
}