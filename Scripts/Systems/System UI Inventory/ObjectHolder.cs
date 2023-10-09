using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ObjectHolder : MonoBehaviour
{
    [SerializeField] private Button objectButton;
    [SerializeField] private Image objectImage;
    [SerializeField] private TextMeshProUGUI qte;
    [SerializeField] private GameEvent descriptionEventCall;

    private ItemData item;
    public void AsignData(ItemData item, bool callDescription)
    {
        this.item = item;
        this.objectButton.onClick.AddListener(delegate { ButtonCall(); });
        this.objectImage.sprite = item.sprite;
        this.qte.text = "" + item.Qte;
        if (callDescription)
        {
            DescriptionCall();
        }
    }

    public void ButtonCall()
    {
        DescriptionCall();
    }

    private void DescriptionCall()
    {
        List<object> data = new List<object>
        {
            item
        };

        descriptionEventCall.Raise(this, data);


    }
}
