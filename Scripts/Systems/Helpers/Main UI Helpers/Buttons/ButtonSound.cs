using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonSound : ButtonAbs
{
    [SerializeField] private List<Button> buttons;
    void Start()
    {
        foreach(Button button in buttons)
        {
            button.onClick.AddListener(delegate { PlaySound(); });
        }
        
    }
}
