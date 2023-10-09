using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(GameEventListener))]
public class ObjectsHolder : MonoBehaviour
{
    [SerializeField] GameEvent inventoryEventsCall;
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject content;
    [SerializeField] GameObject rowPrefab;
    [SerializeField] GameEvent gameGUIGeneral;
    [SerializeField] private Button closeButton;
    [Header("Items Holder")]
    [SerializeField] UnityEngine.GameObject itemsHolderUI;
    [SerializeField] private GameEvent UIEffectsCall;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private Vector3 endPoint;

    private List<ItemData> items;
    private ActionInputManager bagInput;
    private bool isActive;
    private List<GameObject> rowsHolders; 

    private void Start()
    {
        isActive = false;
        bagInput = ActionInputManager.Instance;
        rowsHolders = new List<UnityEngine.GameObject>();
        closeButton.onClick.AddListener(delegate { CloseInventory(); });
    }

    private void Update()
    {

        CallInventoryUI();

        if (!inventoryUI.activeSelf && rowsHolders.Count >= 0)
            DestroyHolders();

        
    }

    public void GetItems(Component sender, List<object> data)
    {
        if (data.Count != 1)
            return;
        if (data[0] is not List<ItemData>)
            return;

        items = (List<ItemData>)data[0];
        //Debug.LogError("<color=green> Call Request Items </color>" + items.Count);
        if (items.Count == 0)
            return;
        DisplayItems();
    }

    private void DisplayItems()
    {
        int length = items.Count;
        int rows = (int) (Mathf.Ceil(length / 9)) + 1;
        for (int index = 0; index < rows; index++)
        {
            UnityEngine.GameObject newComponent = Instantiate(rowPrefab, content.transform);
            rowsHolders.Add(newComponent);
            ObjectsRowHolder rowHolder = newComponent.GetComponent<ObjectsRowHolder>();

            if (rowHolder != null)
            {

                int startIndex = 9 * index;
                int rangeCount = Mathf.Min(9, length - startIndex);
                List<ItemData> rangeItems = items.GetRange(startIndex, rangeCount);
                if(rangeItems.Count <= 0)
                {
                    rowsHolders.Remove(newComponent);
                    Destroy(newComponent);
                }
                else
                {
                    rowHolder.SetItems(rangeItems, index);
                }
                
            }
            else
            {
                return;
            }
        }

    }

    private void CallInventoryUI()
    {
        if(!isActive && bagInput.OnBag() && rowsHolders.Count <= 0)
        {
            isActive = true;

            List<object> itemsCall = new List<object>
            {
                RequestsType.AllItems,
            };
            inventoryEventsCall.Raise(this, itemsCall);

            List<object> data = new List<object>
            {
                GameGUI.InventoryGUI,
                UIDisplay.On,
                inventoryUI,
            };
            gameGUIGeneral.Raise(this, data);
            ObjectHolderAnimationMoveInCall();
        }
        else if (isActive && bagInput.OnBag())
        {
            CloseInventory();
        }
    }



    private void CloseInventory()
    {
        

        List<object> data = new List<object>
            {
                GameGUI.InventoryGUI,
                UIDisplay.Off,
                inventoryUI,
            };
        gameGUIGeneral.Raise(this, data);
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

    private void DestroyHolders()
    {
        isActive = false;

        foreach (UnityEngine.GameObject obj in rowsHolders)
        {
            Destroy(obj);
        }
        rowsHolders.Clear();
    }

}
