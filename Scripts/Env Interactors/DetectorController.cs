using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorController : AbstractInteractor
{
    [SerializeField] GameEvent puzzelEvent;
    // Start is called before the first frame update
    
    private void Start()
    {
        action = EnvActions.PuzzelAction;
    }
    public override EnvActions GetEvent()
    {
        return action;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            puzzelEvent.Raise(this, new List<object> { action });
        }
    }

}
