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
        
        Vector3 playerOffsetFromPortal = otherPortal.InverseTransformPoint(mainCamera.transform.position);
        transform.position = portal.TransformPoint(playerOffsetFromPortal);

        Quaternion relative = Quaternion.Inverse(otherPortal.transform.rotation) * mainCamera.transform.rotation;
        transform.rotation = portal.transform.rotation * relative;
        
        Plane portalPlane = new Plane(portal.forward, portal.position);
        //Create an oblique clip plane in the same position and rotation as the portal
        Vector4 clipPlaneWorld = new Vector4(
                    -portal.forward.x,
                    -portal.forward.y,
                    -portal.forward.z,
                    Mathf.Abs(portalPlane.GetDistanceToPoint(transform.position)));

        //Put it into Camera Space
        Vector4 clipPlaneCamera = Matrix4x4.Transpose(portalCamera.cameraToWorldMatrix) * clipPlaneWorld;
        //Apply it to the projection matrix
        portalCamera.projectionMatrix = portalCamera.CalculateObliqueMatrix(clipPlaneCamera);

    }
}
