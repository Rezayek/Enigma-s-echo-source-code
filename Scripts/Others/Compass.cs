using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    [SerializeField] RawImage compassImage;

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        compassImage.uvRect = new Rect(playerTransform.localEulerAngles.y / 360f, 0f, 1f, 1f);
    }
}
