using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour {

    public Transform reciever;

    private List<Transform> overlappingTransforms = new List<Transform>();
    private Collider coll;

    void Start () {
        coll = GetComponent<Collider>();
    }

    void Update () {
        if (overlappingTransforms.Count > 0)
        {
            List<Transform> forDeletion = new List<Transform>();
            foreach (Transform overlappingTransform in overlappingTransforms) {
                if (coll.bounds.Contains(overlappingTransform.position))
                {
                    float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
                    Debug.Log(rotationDiff);
                    overlappingTransform.Rotate(Vector3.up, rotationDiff);

                    Vector3 portalToTarget = overlappingTransform.position - transform.position;
                    Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToTarget;
                    Debug.Log(positionOffset);
                    overlappingTransform.position = reciever.position + positionOffset;

                    forDeletion.Add(overlappingTransform);
                }
            }
            foreach (Transform toDelete in forDeletion)
                overlappingTransforms.Remove(toDelete);
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (!overlappingTransforms.Contains(other.transform))
            overlappingTransforms.Add(other.transform);
    }

    void OnTriggerExit (Collider other)
    {
        overlappingTransforms.Remove(other.transform);
    }
}
