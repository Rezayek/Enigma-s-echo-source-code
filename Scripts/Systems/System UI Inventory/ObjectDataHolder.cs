using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ObjectDataHolder : MonoBehaviour
{
    [SerializeField] private UnityEngine.GameObject descriptionHolder;
    [SerializeField] private Image objectImage;
    [SerializeField] private TextMeshProUGUI objectName;
    [SerializeField] private TextMeshProUGUI obejctCategorie;
    [SerializeField] private TextMeshProUGUI objectType;
    [SerializeField] private TextMeshProUGUI obejctQte;
    [SerializeField] private TextMeshProUGUI objectCQte;
    [SerializeField] private TextMeshProUGUI objectDescription;
    [SerializeField] private GameEvent inspectorCall;
    [SerializeField] private GameObject InspectorActionHolder;
    [SerializeField] private Button InspectorAction;
    [SerializeField] private GameEvent readerCall;
    [SerializeField] private GameObject readerActionHolder;
    [SerializeField] private Button readerAction;
    [SerializeField] private GameEvent generalUIEvent;
    [SerializeField] private GameObject readerUI;

    [Header("Animation Data")]
    [SerializeField] private GameEvent UIEffectsCall;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private Vector3 endPoint;



    private ItemData currentItem;

    // Start is called before the first frame update
    void Start()
    {
        descriptionHolder.SetActive(false);
        InspectorAction.onClick.AddListener(delegate { InspectorCall(); });
        readerAction.onClick.AddListener(delegate { ReaderCall(); });
    }


    public void InspectorCall()
    {
        inspectorCall.Raise(this, new List<object> { currentItem });
    }

    private void ReaderCall()
    {
        List<string> texts = new List<string>();
        foreach (Pages p in currentItem.pages)
        {
            texts.Add(p.text);
        }
        readerCall.Raise(this, new List<object> { currentItem.itemName, texts });
        ReaderUICall();
    }

    private void ReaderUICall()
    {
        List<object> data = new List<object>
        {
            GameGUI.ReaderGUI,
            UIDisplay.On,
            readerUI,
        };

        generalUIEvent.Raise(this, data);
    }

    
    private void AsignFields()
    {
        objectImage.sprite = currentItem.sprite;
        objectName.text = currentItem.itemName;
        obejctCategorie.text = "Categorie: " + currentItem.category;
        obejctQte.text = "Qte: " + currentItem.Qte;
        objectDescription.text = currentItem.itemDescription;
        if (currentItem.category == Category.Consummable)
        {
            if (currentItem.consummableType == ConsummableType.Health)
            {
                objectType.text = "Type: " + ConsummableType.Health;
                objectCQte.text = "Regenerate: +" + currentItem.consummeGain + " Hp";
            }

            else if (currentItem.consummableType == ConsummableType.Sanity)
            {
                objectType.text = "Type: " + ConsummableType.Sanity;
                objectCQte.text = "Regenerate: +" + currentItem.consummeGain + " Sanity";
            }

        }
        else
        {
            objectCQte.text = "";
            objectType.text = "Type: " + currentItem.objectType;
        }

        if (currentItem.objectType == ObjectType.Book)
        {
            readerActionHolder.SetActive(true);
        }
        else
        {
            readerActionHolder.SetActive(false);
        }

        if (!descriptionHolder.activeSelf)
            descriptionHolder.SetActive(true);
    }
    public void DisplayDescription(Component sender, List<object> data)
    {
        if (data.Count != 1)
            return;
        if (data[0] is not ItemData)
            return;
        currentItem = (ItemData)data[0];
        AsignFields();
        ObjectHolderAnimationMoveInCall();

    }

    private void ObjectHolderAnimationMoveInCall()
    {
        List<object> data = new List<object>
        {
            UIAnimations.Move,
            UIAnimations.MoveIn,
            startPoint,
            endPoint,
            rectTransform,
        };

        UIEffectsCall.Raise(this, data);

    }
}
