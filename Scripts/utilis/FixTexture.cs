using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixTexture : MonoBehaviour
{
    private void Start()
    {
        Camera cam = GetComponent<Camera>();
        cam.depthTextureMode = cam.depthTextureMode | DepthTextureMode.Depth;
    }
}
