
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameEventListener))]
public class InteractorManager : MonoBehaviour
{

    [SerializeField] private GameObject interactorPlace;
    [SerializeField] private GameObject interactorLoot;
    [SerializeField] private GameObject interactorInspect;
    [SerializeField] private GameObject interactorTalk;

    private void Start()
    {
        interactorLoot.SetActive(false);
        interactorPlace.SetActive(false);
        interactorInspect.SetActive(false);
        interactorTalk.SetActive(false);
    }

    public void LootContorller(Component sender, List<object> data)
    {
        if (data.Count <= 1)
            return;
        if (data[0] is not InteractorActions && data[1] is not UIDisplay)
            return;
        InteractorActions castEnum1 = (InteractorActions)data[0];
        if (castEnum1 != InteractorActions.Loot)
            return;
        UIDisplay castEnum2 = (UIDisplay)data[1];
        if(castEnum2 == UIDisplay.On && !interactorLoot.activeSelf)
        {
            interactorLoot.SetActive(true);
        }
        else if (castEnum2 == UIDisplay.Off && interactorLoot.activeSelf)
        {
            interactorLoot.SetActive(false);
        }
    }

    public void PlaceContorller(Component sender, List<object> data)
    {
        if (data.Count <= 1)
            return;
        if (data[0] is not InteractorActions && data[1] is not UIDisplay)
            return;
        InteractorActions castEnum1 = (InteractorActions)data[0];
        if (castEnum1 != InteractorActions.Place)
            return;
        UIDisplay castEnum2 = (UIDisplay)data[1];
        if (castEnum2 == UIDisplay.On && !interactorPlace.activeSelf)
        {
            interactorPlace.SetActive(true);
        }
        else if (castEnum2 == UIDisplay.Off && interactorPlace.activeSelf)
        {
            interactorPlace.SetActive(false);
        }
    }

    public void InspectContorller(Component sender, List<object> data)
    {
        if (data.Count <= 1)
            return;
        if (data[0] is not InteractorActions && data[1] is not UIDisplay)
            return;
        InteractorActions castEnum1 = (InteractorActions)data[0];
        if (castEnum1 != InteractorActions.Inspect)
            return;
        UIDisplay castEnum2 = (UIDisplay)data[1];
        if (castEnum2 == UIDisplay.On && !interactorInspect.activeSelf)
        {
            interactorInspect.SetActive(true);
        }
        else if (castEnum2 == UIDisplay.Off && interactorInspect.activeSelf)
        {
            interactorInspect.SetActive(false);
        }
    }

    public void TalkController(Component sender, List<object> data)
    {
        if (data.Count <= 1)
            return;
        if (data[0] is not InteractorActions && data[1] is not UIDisplay)
            return;
        InteractorActions castEnum1 = (InteractorActions)data[0];
        if (castEnum1 != InteractorActions.Talk)
            return;
        UIDisplay castEnum2 = (UIDisplay)data[1];
        
        if (castEnum2 == UIDisplay.On && !interactorTalk.activeSelf)
        {
            interactorTalk.SetActive(true);
        }
        else if (castEnum2 == UIDisplay.Off && interactorTalk.activeSelf)
        {
            interactorTalk.SetActive(false);
        }

    }
}
