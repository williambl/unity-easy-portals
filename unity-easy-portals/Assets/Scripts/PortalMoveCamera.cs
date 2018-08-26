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

        //Create a camera space near clip plane
        Vector4 clipPlaneCamera = CameraSpacePlane(portalCamera, portal.position, portal.forward, 1.0f);
        //Apply it to the projection matrix
        portalCamera.projectionMatrix = portalCamera.CalculateObliqueMatrix(clipPlaneCamera);

    }

    // Given position/normal of the plane, calculates plane in camera space.
    private Vector4 CameraSpacePlane (Camera cam, Vector3 pos, Vector3 normal, float sideSign)
    {
        Matrix4x4 m = cam.worldToCameraMatrix;
        Vector3 cpos = m.MultiplyPoint(pos);
        Vector3 cnormal = m.MultiplyVector(normal).normalized * sideSign;
        return new Vector4(cnormal.x, cnormal.y, cnormal.z, -Vector3.Dot(cpos,cnormal));
    }
}
