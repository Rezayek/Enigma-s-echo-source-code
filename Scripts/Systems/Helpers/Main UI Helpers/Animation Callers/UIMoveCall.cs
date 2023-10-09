using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoveCall : MonoBehaviour
{
    [SerializeField] private GameEvent UIEffects;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 EndPosition;
    [SerializeField] private RectTransform rectTransform;
    void Start()
    {
        PlayMoveAnimation();

    }


    private void PlayMoveAnimation()
    {
        List<object> data = new List<object>
        {
            UIAnimations.Move,
            UIAnimations.MoveIn,
            startPosition,
            EndPosition,
            rectTransform,
        };
        UIEffects.Raise(this, data);
    }

    public void PlayMoveExposed()
    {
        PlayMoveAnimation();
    }
}
