using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameEventListener))]
public class GameSceneLoader : GenericSingleton<GameSceneLoader>
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameEvent UIGeneralCall;


    public void LoadSceneListener(Component sender, List<object> data)
    {
        if (data.Count != 1)
            return;
        if (data[0] is not ScenarioName)
            return;


        StartCoroutine(OpenLoading((int)(ScenarioName)data[0]));
    }

    public void LoadCasesListener(Component sender, List<object> data)
    {
        if (data.Count != 1)
            return;
        if (data[0] is not OtherLoadingCases)
            return;
        StartCoroutine(OpenLoadingCase());
    }

    private IEnumerator OpenLoading(int scenarioIndex)
    {
        UIGeneralCall.Raise(this, new List<object> { GameGUI.LoadingUI, UIDisplay.On, loadingScreen });
        Debug.Log("Loading Scene Starting");
        Task.Delay(1000);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scenarioIndex);
        while (!operation.isDone)
        {
            yield return null;
        }
        

        // Hide the loading screen
        UIGeneralCall.Raise(this, new List<object> { GameGUI.LoadingUI, UIDisplay.OffAll, loadingScreen });
        UIGeneralCall.Raise(this, new List<object> { GameGUI.LoadingUI, UIDisplay.Off, loadingScreen });


    }

    private IEnumerator OpenLoadingCase()
    {
        UIGeneralCall.Raise(this, new List<object> { GameGUI.LoadingUI, UIDisplay.On, loadingScreen });
        Debug.Log("Loading Scene Starting");
        float startTime = Time.realtimeSinceStartup;
        float delay = 3.0f; // Adjust the delay as needed

        while (Time.realtimeSinceStartup - startTime < delay)
        {
            yield return null;
        }

        Debug.Log("Loading Scene END ");
        UIGeneralCall.Raise(this, new List<object> { GameGUI.LoadingUI, UIDisplay.Off, loadingScreen });
    }
}
