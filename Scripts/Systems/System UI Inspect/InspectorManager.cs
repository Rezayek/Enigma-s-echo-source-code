using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InspectorManager : MonoBehaviour
{
    [SerializeField] private GameEvent gameGUIEvent;
    [SerializeField] private GameObject inspectorGUI;
    [SerializeField] private GameObject parentHolder;
    [SerializeField] private TextMeshProUGUI objectName;
    [SerializeField] private Button closeButton;
    [SerializeField] private float rotationSpeed = 0.2f;

    private GameObject inspectedObj;
    private ActionInputManager mouseMove;
    private bool inspectorOn;
    // Start is called before the first frame update
    void Start()
    {
        inspectorOn = false;
        mouseMove = ActionInputManager.Instance;
        closeButton.onClick.AddListener(delegate { CloseInspector(); });
    }

    // Update is called once per frame
    void Update()
    {

        MouseObjectRotate();

    }

    private void MouseObjectRotate()
    {
        if (!inspectorOn)
            return;

        if (!mouseMove.OnMouseLeftPress())
           return;

        Vector2 rotation = mouseMove.OnMouseMove() * rotationSpeed;

        inspectedObj.transform.Rotate(Vector3.up, rotation.x, Space.World);
        inspectedObj.transform.Rotate(Vector3.right, rotation.y, Space.World);
    }

    public void InspectorListener(Component sender, List<object> data)
    {

        if (data.Count != 1)
            return;
        if (data[0] is not ItemData)
            return;

        inspectorOn = true;

        List<object> dataGUI = new List<object>
        {
            GameGUI.InspectorGUI,
            UIDisplay.On,
            inspectorGUI
        };
        gameGUIEvent.Raise(this, dataGUI);

        ItemData item = (ItemData)data[0];
        objectName.text = item.itemName;
        inspectedObj = Instantiate(item.itemPrefab, parentHolder.transform.position, Quaternion.identity, parentHolder.transform);

        if (item.itemInspectorScale != Vector3.zero) 
            inspectedObj.transform.localScale = item.itemInspectorScale;
        else
            inspectedObj.transform.localScale = new Vector3(300, 300, 300);

        inspectedObj.layer = LayerMask.NameToLayer("UI");
        inspectorOn = true;


    }
    public void CloseInspector()
    {
        inspectorOn = false;
        List<object> dataGUI = new List<object>
        {
            GameGUI.InspectorGUI,
            UIDisplay.Off,
            inspectorGUI
        };
        gameGUIEvent.Raise(this, dataGUI);
        Destroy(inspectedObj);
    }
}
