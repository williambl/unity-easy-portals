using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMoveCamera : MonoBehaviour {

    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        Vector3 cameraOffset = playerCamera.position - otherPortal.position;
        transform.position = portal.position + cameraOffset;

        float portalAngleDifference = Quaternion.Angle(portal.rotation, otherPortal.rotation);

        Quaternion portalRotationDifference = Quaternion.AngleAxis(portalAngleDifference, Vector3.up);
        Vector3 newCameraDirection = portalRotationDifference * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}
