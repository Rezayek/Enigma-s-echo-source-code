using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPosition : MonoBehaviour
{
    [SerializeField] private Transform childTransforms;

    private void Update()
    {
        childTransforms.localPosition = new Vector3(0, 0, 0);
        childTransforms.localEulerAngles = new Vector3(0, 0, 0);
    }
}
