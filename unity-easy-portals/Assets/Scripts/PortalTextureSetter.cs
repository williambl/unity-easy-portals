using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetter : MonoBehaviour {

    public Camera cameraA;
    public Material matA;

    public Camera cameraB;
    public Material matB;

    // Use this for initialization
    void Start () {
        if (cameraA.targetTexture != null)
            cameraA.targetTexture.Release();
        cameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        matA.mainTexture = cameraA.targetTexture;
        if (cameraB.targetTexture != null)
            cameraB.targetTexture.Release();
        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        matB.mainTexture = cameraB.targetTexture;
    }

    // Update is called once per frame
    void Update () {

    }
}
