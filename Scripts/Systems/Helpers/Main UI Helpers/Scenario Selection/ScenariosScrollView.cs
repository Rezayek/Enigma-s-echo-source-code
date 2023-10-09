using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenariosScrollView : MonoBehaviour
{
    [SerializeField] private GameObject scrollViewContent; 
    [SerializeField] private GameObject uiPrefab;
    [SerializeField] private ScenariosData scenariosData;
    private void Start()
    {
        InstantiatePrefabs();
    }
    private void InstantiatePrefabs()
    {
        for (int i = 0; i < scenariosData.scenarios.Count; i++)
        {
            // Instantiate the UI prefab as a child of the Content GameObject in the Scroll View
            GameObject newUIPrefab = Instantiate(uiPrefab, scrollViewContent.transform);

            ScenarioHolder holder = newUIPrefab.GetComponent<ScenarioHolder>();
            holder.SetElements(scenariosData.scenarios[i], i == 0);
        }
    }
}
