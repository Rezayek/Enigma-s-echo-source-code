using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NotifierManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Image image;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float secondsToWait = 2f;
    [SerializeField] GameEvent UIEffectsCall;


    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0.0f;
    }

    private void PlayFadeIn()
    {

        List<object> data = new List<object>
            {
                UIAnimations.Fade,
                UIAnimations.FadeIn,
                canvasGroup,
            };
        UIEffectsCall.Raise(this, data);
    }

    private void PlayFadeOut()
    {
        
        List<object> data = new List<object>
            {
                UIAnimations.Fade,
                UIAnimations.FadeOut,
                canvasGroup,
            };
        UIEffectsCall.Raise(this, data);
        
    }

    public void StoreNotification(Component sender, List<object> data)
    {
        if (data.Count != 3)
            return;
        if(data[0] is not NotifierMessageType)
            return;
        NotifierMessageType notifierMessageType = (NotifierMessageType)data[0];
        string name = (string)data[1];
        Sprite sprite = (Sprite)data[2];

        StopAllCoroutines();
        StartCoroutine(WaitBeforeNext(new ItemInfos(name, sprite, notifierMessageType)));
    }

    private void PlayNotifications(ItemInfos item)
    {
        image.sprite = item.sprite;

        switch (item.notifierMessageType)
        {
            case NotifierMessageType.ItemAdd:
                //Debug.Log("<color=blue> Notification: Call Item Add </color>");
                
                textMeshPro.text = item.name + " added to Inventory. ";
                return;
            case NotifierMessageType.ItemRemove:
                //Debug.Log("<color=blue> Notification: Call Item Remove </color>");
                textMeshPro.text = item.name + " removed from Inventory. ";
                return;
            case NotifierMessageType.ItemHealthConsumme:
                //Debug.Log("<color=blue> Notification: Call Item Health Consumme </color>");
                textMeshPro.text = item.name + " is consummed. ";
                return;
            case NotifierMessageType.ItemSanityConsumme:
                //Debug.Log("<color=blue> Notification: Call Item Sanity Consumme </color>");
                textMeshPro.text = item.name + " is consummed. ";
                return;
            case NotifierMessageType.ItemPlace:
                //Debug.Log("<color=blue> Notification: Call Item Place </color>");
                textMeshPro.text = item.name + " is Placed is front of the Alter. ";
                return;
        }
    }
    

    private IEnumerator WaitBeforeNext(ItemInfos item)
    {
        PlayFadeIn();
        PlayNotifications(item);
        yield return new WaitForSeconds(secondsToWait);
        PlayFadeOut();

    }
    private class ItemInfos
    {
        public NotifierMessageType notifierMessageType;
        public string name;
        public Sprite sprite;

        public ItemInfos(string name, Sprite sprite, NotifierMessageType notifierMessageType)
        {
            this.notifierMessageType = notifierMessageType;
            this.name = name;
            this.sprite = sprite;
        }
    }
}
