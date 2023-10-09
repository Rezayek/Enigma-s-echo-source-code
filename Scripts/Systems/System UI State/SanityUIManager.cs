using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(GameEventListener))]
public class SanityUIManager : StateUIManager
{
    [SerializeField] private int maxValue;
    [SerializeField] private Slider slider;
    [SerializeField] private CanvasGroup sanityLoss;
    [SerializeField] private GameEvent UIEffectCall;
    private AudioEventCaller audioEventCaller;


    // Start is called before the first frame update
    void Start()
    {
        audioEventCaller = AudioEventCaller.Instance;
        audioEventCaller = AudioEventCaller.Instance;
        slider.maxValue = maxValue;
        slider.value = maxValue;
        sanityLoss.alpha = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public override void DecreaseListener(Component sender, List<object> data)
    {
        if (data.Count <= 1)
            return;
        if (data[0] is not PlayerStateEnum && data[1] is not int)
            return;
        PlayerStateEnum castEnum = (PlayerStateEnum)data[0];
        if (castEnum != PlayerStateEnum.DecreaseSanity)
            return;
        slider.value = (int)data[1];
        PlayeImmersiveState();
        audioEventCaller.PlayOnce(SoundType1.LossSanity);
    }

    public override void IncreaseListener(Component sender, List<object> data)
    {
        if (data.Count <= 1)
            return;
        if (data[0] is not PlayerStateEnum && data[1] is not int)
            return;
        PlayerStateEnum castEnum = (PlayerStateEnum)data[0];
        if (castEnum != PlayerStateEnum.IncreaseSanity)
            return;
        slider.value = (int)data[1];
    }

    public override void PlayeImmersiveState()
    {
        List<object> data = new List<object>
        {
            UIAnimations.Fade,
            UIAnimations.FadeInFadeOut,
            sanityLoss,
        };
        UIEffectCall.Raise(this, data);
    }




}
