using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(GameEventListener))]
public class ReaderManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bookName;
    [SerializeField] private TextMeshProUGUI pageText;
    [SerializeField] private TextMeshProUGUI pageNumber;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private GameEvent generalUIManager;
    [SerializeField] private UnityEngine.GameObject readerUI;

    [Header("Items Holder")]
    [SerializeField] UnityEngine.GameObject readerPageUI;
    [SerializeField] private GameEvent UIEffectsCall;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 currentScale;
    [SerializeField] private Vector2 targetScale;



    private int currentPage;
    private List<string> pages;
    // Start is called before the first frame update
    void Start()
    {
        closeButton.onClick.AddListener(delegate { CloseReader(); });
        nextButton.onClick.AddListener(delegate { NextPage(); });
        previousButton.onClick.AddListener(delegate { PreviousPage(); });
    }

    public void NextPage()
    {
        if (currentPage >= pages.Count-1)
            return;
        currentPage += 1;
        pageNumber.text = currentPage + 1 + "/" + pages.Count;
        pageText.text = pages[currentPage];

    }

    public void PreviousPage()
    {
        if (currentPage <= 0)
            return;

        currentPage -= 1;
        pageNumber.text = currentPage + 1 + "/" + pages.Count;
        pageText.text = pages[currentPage];
    }

    public void CloseReader()
    {
        List<object> data = new List<object>
        {
            GameGUI.ReaderGUI,
            UIDisplay.Off,
            readerUI,
        };

        generalUIManager.Raise(this, data);
    }

    public void BookDataListener(Component sender, List<object> data)
    {
        if (data.Count != 2)
            return;
        if (data[0] is not string && data[1] is not List<string>)
            return;
        bookName.text = (string)data[0];
        pages = (List<string>)data[1];
        currentPage = 0;
        pageNumber.text = currentPage + 1 + "/" + pages.Count;
        pageText.text = pages[currentPage];
        ObjectHolderAnimationScaleInCall();
    }

    private void ObjectHolderAnimationScaleInCall()
    {
        List<object> data = new List<object>
        {
            UIAnimations.Scale,
            UIAnimations.ScaleIn,
            currentScale,
            targetScale,
            rectTransform,
        };

        UIEffectsCall.Raise(this, data);
    }
}
