using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMoveCamera : MonoBehaviour {

    private Camera portalCamera;

    public Transform portal;
    public Transform otherPortal;

    void Start () {
        portalCamera = GetComponent<Camera>();
    }

    void Update ()
    {
        //Modified from https://github.com/sclark39/Portal-In-Unity/blob/master/Portal/Assets/PortalCamera.cs
        Camera mainCamera = Camera.main;
        portalCamera.fieldOfView = mainCamera.fieldOfView;
        portalCamera.nearClipPlane = Vector3.Distance(transform.position, portal.position);

        Vector3 playerOffsetFromPortal = otherPortal.InverseTransformPoint(mainCamera.transform.position);
        transform.position = portal.TransformPoint(playerOffsetFromPortal);

        Quaternion relative = Quaternion.Inverse(otherPortal.transform.rotation) * mainCamera.transform.rotation;
        transform.rotation = portal.transform.rotation * relative;
    }
}
