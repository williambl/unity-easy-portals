using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour {

    private static PortalManager instance;

    public GameObject portalPairPrefab;

    void Start () {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    public static GameObject InstantiatePortalPair (Vector3[] positions, Quaternion[] rotations) {
        if (positions.Length != 2)
            throw new ArgumentOutOfRangeException("positions", "Positions argument must have a length of 2");
        if (rotations.Length != 2)
            throw new ArgumentOutOfRangeException("rotations", "Rotations argument must have a length of 2");

        GameObject portalPair = Instantiate(instance.portalPairPrefab);
        Transform portalA = portalPair.transform.Find("PortalA");
        Transform portalB = portalPair.transform.Find("PortalB");

        portalA.position = positions[0];
        portalA.rotation = rotations[0];
        portalB.position = positions[1];
        portalB.rotation = rotations[1];

        return portalPair;
    }
}
