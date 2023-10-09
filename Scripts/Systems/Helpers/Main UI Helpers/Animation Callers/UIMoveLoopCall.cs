using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoveLoopCall : MonoBehaviour
{
    [SerializeField] private GameEvent UIEffects;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 EndPosition;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float duration = 2;

    private bool rePlay;
    private void Start()
    {
        rePlay = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (rePlay)
            StartCoroutine(PlayMoveAnimation());
            
    }

    private IEnumerator PlayMoveAnimation()
    {
        rePlay = false;
        List<object> data = new List<object>
        {
            UIAnimations.Move,
            UIAnimations.MoveInOut,
            startPosition,
            EndPosition,
            rectTransform,
        };
        UIEffects.Raise(this, data);
        yield return new WaitForSeconds(duration);
        rePlay = true;

    }
}
