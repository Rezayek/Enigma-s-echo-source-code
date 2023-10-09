using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(GameEventListener))]
public class SystemUIGeneral : MonoBehaviour
{
    [SerializeField] private bool isGameScene = true;
    [SerializeField] private GameObject playModeGUI;
    [SerializeField] private GameObject crossHair;
    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private GameEvent UIEffectCall;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameGUI mainMenuEnum;
    private Stack<GameObject> gameGUIs;
    private Stack<GameGUI> gameGUITypes;
    

    private void Start()
    {

        gameGUIs = new Stack<GameObject>();
        gameGUITypes = new Stack<GameGUI>();
        if (!isGameScene)
        {
            gameGUIs.Push(mainMenu);
            gameGUITypes.Push(mainMenuEnum);
        }
        
    }


    public void GUIListener(Component sender, List<object> data)
    {
        if (data.Count < 3)
            return;
        if (data[0] is not GameGUI)
            return;
        if (data[1] is not UIDisplay)
            return;

        GameGUI castEnum1 = (GameGUI)data[0];
        UIDisplay GUImode = (UIDisplay)data[1];
        GameObject gameGUI = (GameObject)data[2];

        switch (GUImode)
        {
            case UIDisplay.On:
                GUIActivate(gameGUI, castEnum1);
                break;
            case UIDisplay.Off:
                GUIDeactivate(gameGUI, castEnum1);
                break;
            case UIDisplay.OffAll:
                GUIDeactivateAll();
                break;
        }

    }

    private void GUIActivate(GameObject gameGUI, GameGUI type)
    {
        if(gameGUIs.Count == 0 && isGameScene)
        {
            PlayerPrefs.SetString(PlayerPrefsNames.UIActive.ToString(), "on");
            playModeGUI.SetActive(false);
            crossHair.SetActive(false);
            cinemachineBrain.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            
        }

        if (gameGUITypes.Contains(type))
            return;

        gameGUI.SetActive(true);
        CallFadeIn(gameGUI.GetComponent<CanvasGroup>());
        gameGUIs.Push(gameGUI);
        gameGUITypes.Push(type);
    }

    private void GUIDeactivate(GameObject gameGUI, GameGUI type)
    {

        if (gameGUIs.Count == 0 && gameGUITypes.Count == 0)
            return;

        GameObject obj = gameGUIs.Peek();
        GameGUI guiType = gameGUITypes.Peek();

        if (obj != gameGUI || guiType != type)
            return;

        gameGUITypes.Pop();
        obj = gameGUIs.Pop();

        CallFadeOut(gameGUI.GetComponent<CanvasGroup>());
        obj.SetActive(false);

        if (gameGUIs.Count == 0 && isGameScene)
        {
            Time.timeScale = 1f;
            cinemachineBrain.enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            playModeGUI.SetActive(true);
            crossHair.SetActive(true);
            PlayerPrefs.SetString(PlayerPrefsNames.UIActive.ToString(), "off");
        }
            

    }


    private void GUIDeactivateAll()
    {
        while(gameGUIs.Count > 0)
        {
            GameObject ui = gameGUIs.Pop();
            ui.SetActive(false);

        }

        gameGUIs.Clear();
        gameGUITypes.Clear();

}


    private void CallFadeIn(CanvasGroup canvasGroup)
    {
        List<object> data = new List<object>
        {
            UIAnimations.Fade,
            UIAnimations.FadeIn,
            canvasGroup,
        };
        UIEffectCall.Raise(this, data);
    }

    private void CallFadeOut(CanvasGroup canvasGroup)
    {
        List<object> data = new List<object>
        {
            UIAnimations.Fade,
            UIAnimations.FadeOut,
            canvasGroup,
        };
        UIEffectCall.Raise(this, data);
    }
}
