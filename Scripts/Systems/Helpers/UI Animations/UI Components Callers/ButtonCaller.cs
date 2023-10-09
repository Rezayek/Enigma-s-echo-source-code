using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonCaller : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameEvent UIEffects;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 EndPosition;
    [SerializeField] private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(delegate { PlayMoveAnimation(); });
    }

    public void PlayMoveAnimation()
    {
        List<object> data = new List<object>
        {
            UIAnimations.Move,
            UIAnimations.MoveInOut,
            startPosition,
            EndPosition,
            rectTransform,
        };
        UIEffects.Raise(this, data);
    }
}
