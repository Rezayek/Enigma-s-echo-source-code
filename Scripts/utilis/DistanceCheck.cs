using UnityEngine;

public class DistanceCheck : MonoBehaviour
{
    [SerializeField] private TreeSettings treeSettings; // Distance threshold for deactivation
    [SerializeField] private string playerTag = "Player"; // Tag assigned to the player object
    [SerializeField] private int priority;
    private GameObject player; // Reference to the player object
    private float Threshold;
    private int frameCounter;

    private void Awake()
    {
        gameObject.tag = "Tree";
    }
    private void Start()
    {
        // Find the player object based on the assigned tag
        player = GameObject.FindGameObjectWithTag(playerTag);
        Threshold = treeSettings.activationDistance;
        if (player == null)
        {
            Debug.LogError("Player object not found. Make sure to assign the correct tag.");
        }
    }

    private void Update()
    {
        //frameCounter++;

        //if (frameCounter % 20 != 0)
        //{
        //    return;
        //}

        //Threshold = treeSettings.activationDistance;

        //// Check the distance between the object and the player
        //float distance = Vector3.Distance(transform.position, player.transform.position);

        ////if (distance > Threshold + 100 && gameObject.activeSelf)
        ////{
        ////    // Deactivate the object if it's far from the player
        ////    gameObject.SetActive(false);
        ////    Debug.Log("Tree script called");
        ////}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<DistanceCheck>().GetPriority() > priority)
        {
            Destroy(gameObject);
        }
        
    }

    public int GetPriority()
    {
        return priority;
    }
}
