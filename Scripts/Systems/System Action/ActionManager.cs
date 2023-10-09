using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ActionInputManager))]
public class ActionManager : MonoBehaviour
{
    [Range(0.1f, 5f)]
    [SerializeField] float objectDetectionDistance;
    [SerializeField] GameEvent inventoryEventsCall;
    [SerializeField] GameEvent objectIanteractorCall;
    [SerializeField] GameEvent torchCall;
    [SerializeField] GameEvent dialogCall;
    [SerializeField] GameEvent interactorCall;
    [SerializeField] GameEvent navigatorUICall;
    private ActionInputManager actionInputManager;
    private AudioEventCaller audioEventCaller;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        actionInputManager = ActionInputManager.Instance;
        audioEventCaller = AudioEventCaller.Instance;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString(PlayerPrefsNames.UIActive.ToString()) == "off")
        {
            OnFocus();
            OnLoot();
            OnPlace();
            OnTalk();
            OnTorch();
            OnInspect();
            OnHealthConsume();
            OnSanityConsume();
            OnPause();
        }
    }

    private bool RayCameraObserver(ObjectsToObserver obj, out RaycastHit hitData)
    {
        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, objectDetectionDistance) && hit.collider.tag == obj.ToString())
        {
            //Debug.Log("hitting");
            hitData = hit;
            return true;
        }
        hitData = hit;
        return false;
    }


    private void OnFocus()
    {
        RaycastHit hitData;
        bool isHit = RayCameraObserver(ObjectsToObserver.Interactors, out hitData);
        if (isHit && actionInputManager.OnMouseLeftClick())
        {
            //Debug.Log("hit Interactor! "+ isHit);

            GameObject hitGameObject = hitData.collider.gameObject;

            // Get the parent Transform of the hit GameObject
            Transform parentTransform = hitGameObject.transform.parent;

            if (parentTransform != null)
            {
                // If the parent has the AbstractInteractor component, you can get the data from it
                AbstractInteractor parentInteractor = parentTransform.GetComponent<AbstractInteractor>();
                if (parentInteractor != null)
                {
                    List<object> data = new List<object>
                    {
                        parentInteractor.GetEvent(),
                        parentInteractor
                    };
                    //Debug.Log("hit Interactor");
                    interactorCall.Raise(this, data);
                }
                else
                {
                    Debug.Log("Parent GameObject does not have AbstractInteractor component.");
                }
            }
            else
            {
                Debug.Log("Parent GameObject not found.");
            }
        }
    }

    private void OnLoot()
    {
        RaycastHit hitData;
        bool isHit = RayCameraObserver(ObjectsToObserver.ObjectLoot, out hitData);
        if (isHit && actionInputManager.OnLoot())
        {
            CallInteractorOff(InteractorActions.Loot);
            List<object> data = new()
            {
                RequestsType.AddItem,
                hitData.collider.GetComponent<DataHolder>().GetData

            };
            audioEventCaller.PlayOnce(SoundType1.Looting);
            inventoryEventsCall.Raise(this, data);
            Destroy(hitData.collider.gameObject);
        }
        else if (isHit)
        {
            CallInteractorOn(InteractorActions.Loot);
        }
        else
        {
            CallInteractorOff(InteractorActions.Loot);
        }
    }

    private void OnPlace()
    {
        RaycastHit hitData;
        bool isHit = RayCameraObserver(ObjectsToObserver.ObjectPlace, out hitData);
        if (isHit && actionInputManager.OnLoot())
        {
            CallInteractorOff(InteractorActions.Place);
            List<object> data = new()
            {
                RequestsType.PlaceItem,
                ObjectType.Artifact
            };
            audioEventCaller.PlayOnce(SoundType1.Looting);
            inventoryEventsCall.Raise(this, data);
        }
        else if (isHit)
        {
            CallInteractorOn(InteractorActions.Place);
        }
        else
        {
            CallInteractorOff(InteractorActions.Place);
        }
    }

    public void OnInspect()
    {
        RaycastHit hitData;
        bool isHit = RayCameraObserver(ObjectsToObserver.ObjectLoot, out hitData);
        if (isHit && actionInputManager.OnInspect())
        {
            CallInteractorOff(InteractorActions.Inspect);
            List<object> data = new()
            {
                RequestsType.InspectItem,
                hitData.collider.GetComponent<DataHolder>().GetData

            };

            audioEventCaller.PlayOnce(SoundType1.Torch);
            inventoryEventsCall.Raise(this, data);
        }
        else if(isHit)
        {
            CallInteractorOn(InteractorActions.Inspect);
        }
        else
        {
            CallInteractorOff(InteractorActions.Inspect);
        }
    }


    public void OnTalk()
    {
        RaycastHit hitData;
        bool isHit = RayCameraObserver(ObjectsToObserver.NPC, out hitData);
        if (isHit && actionInputManager.OnLoot())
        {
            CallInteractorOff(InteractorActions.Talk);
            List<object> data = new()
            {
                RequestsType.Talk,
                hitData.collider.GetComponent<DialogHolder>().GetDialog(),

            };

            dialogCall.Raise(this, data);
        }
        else if (isHit)
        {
            CallInteractorOn(InteractorActions.Talk);
        }
        else
        {
            
            CallInteractorOff(InteractorActions.Talk);
        }
    }
    public void OnTorch()
    {
        if (actionInputManager.TorchControl())
        {
            audioEventCaller.PlayOnce(SoundType1.Torch);
            //TODO Call Torch Event
            List<object> data = new List<object>
            {
                TorchControl.PlayLogic
            };
                torchCall.Raise(this, data);
        }
        

    }

    private void OnHealthConsume()
    {
        if (actionInputManager.OnHealth())
        {
            //TODO:ADD eating sound
            //audioEventCaller.PlayOnce(SoundType1.);
            List<object> data = new()
            {
                RequestsType.ConsummeItem,
                ConsummableType.Health

            };
            inventoryEventsCall.Raise(this, data);
        }
    }

    private void OnSanityConsume()
    {
        if (actionInputManager.OnSanity())
        {
            //TODO:ADD eating sound
            //audioEventCaller.PlayOnce(SoundType1.);
            List<object> data = new()
            {
                RequestsType.ConsummeItem,
                ConsummableType.Sanity

            };
            inventoryEventsCall.Raise(this, data);
            
        }
    }


    private void CallInteractorOn(InteractorActions action)
    {
        List<object> data;
        switch (action)
        {
            case InteractorActions.Loot:

                data = new List<object>
                {
                    InteractorActions.Loot,
                    UIDisplay.On,
                };

                objectIanteractorCall.Raise(this, data);
                data.Clear();

                break;
            case InteractorActions.Inspect:

                data = new List<object>
                {
                    InteractorActions.Inspect,
                    UIDisplay.On,
                };

                objectIanteractorCall.Raise(this, data);
                data.Clear();

                break;
            case InteractorActions.Place:

                data = new List<object>
                {
                    InteractorActions.Place,
                    UIDisplay.On,
                };

                objectIanteractorCall.Raise(this, data);
                data.Clear();

                break;
            case InteractorActions.Talk:

                data = new List<object>
                {
                    InteractorActions.Talk,
                    UIDisplay.On,
                };

                objectIanteractorCall.Raise(this, data);
                data.Clear();

                break;
        }
        
    }

    private void CallInteractorOff(InteractorActions action)
    {
        List<object> data;
        switch (action)
        {
            case InteractorActions.Loot:

                data = new List<object>
                {
                    InteractorActions.Loot,
                    UIDisplay.Off,
                };

                objectIanteractorCall.Raise(this, data);
                data.Clear();

                break;
            case InteractorActions.Inspect:

                data = new List<object>
                {
                    InteractorActions.Inspect,
                    UIDisplay.Off,
                };

                objectIanteractorCall.Raise(this, data);
                data.Clear();

                break;
            case InteractorActions.Place:

                data = new List<object>
                {
                    InteractorActions.Place,
                    UIDisplay.Off,
                };

                objectIanteractorCall.Raise(this, data);
                data.Clear();

                break;
            case InteractorActions.Talk:

                data = new List<object>
                {
                    InteractorActions.Talk,
                    UIDisplay.Off,
                };

                objectIanteractorCall.Raise(this, data);
                data.Clear();
                break;
        }
    }

    private void OnPause()
    {
        if (actionInputManager.OnPause())
        {
            List<object> data = new()
            {
                GameGUI.PauseGUI,

            };
            navigatorUICall.Raise(this, data);

        }
    }
}
