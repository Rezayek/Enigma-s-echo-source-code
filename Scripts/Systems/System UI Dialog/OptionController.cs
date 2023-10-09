using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(GameEventListener))]
public class OptionController : MonoBehaviour
{
    [Header("Option data")]
    [SerializeField] private int index;
    [SerializeField] private GameObject optionHolder;
    [SerializeField] private TextMeshProUGUI optionText;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 startScale;
    [SerializeField] private Vector2 endScale;
    [SerializeField] private GameEvent UIEffectCall;
    public void OptionOn(Component sender, List<object> data)
    {
        if (data.Count != 3)
            return;

        if (data[0] is not int )
            return;
        if (data[1] is not UIDisplay)
            return;
        if (data[2] is not string)
            return;

        int i = (int)data[0];
        
        UIDisplay display = (UIDisplay)data[1];

        if (i != index)
            return;

        if (display != UIDisplay.On)
            return;

        if (optionHolder.activeSelf)
            return;

        optionHolder.SetActive(true);
        UIEffectCall.Raise(this, new List<object> { UIAnimations.Scale, UIAnimations.ScaleIn, startScale, endScale, rectTransform });
        optionText.text = (string)data[2];
    }

    public void OptionOff(Component sender, List<object> data)
    {
        if (data.Count != 2)
            return;
        if (data[0] is not int)
            return;
        if (data[1] is not UIDisplay)
            return;

        int i = (int)data[0];
        UIDisplay display = (UIDisplay)data[1];
        if (i != index)
            return;

        if (display != UIDisplay.Off)
            return;

        if (!optionHolder.activeSelf)
            return;

        optionHolder.SetActive(false);
    }

}
