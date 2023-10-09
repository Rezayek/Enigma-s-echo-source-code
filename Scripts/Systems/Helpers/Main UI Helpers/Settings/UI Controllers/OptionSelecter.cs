using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionSelecter : ButtonAbs
{
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Image optionHolder;
    [SerializeField] private List<Sprite> options;
    [SerializeField] private PlayerPrefsNames selectionType;
    [SerializeField] private bool addEvent;
    [SerializeField] private GameEvent settingsEvent;

    private int currentIndex;
    private void Start()
    {
        if (PlayerPrefs.HasKey(selectionType.ToString()))
        {
            currentIndex = PlayerPrefs.GetInt(selectionType.ToString());   
        }
        else
        {
            currentIndex = 0;
        }
        optionHolder.sprite = options[currentIndex];
        nextButton.onClick.AddListener(delegate { Next(); });
        previousButton.onClick.AddListener(delegate { Back(); });
        nextButton.onClick.AddListener(delegate { PlaySound(); });
        previousButton.onClick.AddListener(delegate { PlaySound(); });
    }

    private void Next()
    {
        if (currentIndex < (options.Count - 1))
        {
            currentIndex += 1;
            ChnageOption();
        }
            
    }

    private void Back()
    {
        if (currentIndex > 0)
        {
            currentIndex -= 1;
            ChnageOption();
        }
            
    }

    private void ChnageOption()
    {
        PlayerPrefs.SetInt(selectionType.ToString(), currentIndex);
        optionHolder.sprite = options[currentIndex];
        if (addEvent)
            settingsEvent.Raise(this, new List<object> { selectionType });
    }

}
