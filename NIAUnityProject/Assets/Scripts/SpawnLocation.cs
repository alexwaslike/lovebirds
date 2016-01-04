using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpawnLocation : MonoBehaviour {

    public GameObject cage;
    public Vector3 birdSpawnLoc;
    public Vector3 birdSpawnRot;
    public Vector3 animationSpawnLoc;
    public Vector3 animationSpawnRot;
    public Vector3 acceptSpawnLoc;
    public Vector3 cuddleSpawnLoc;
    public Vector3 handSpawnLoc;
    public Vector3 handSpawnRot;
    public Vector3 sadSpawnLoc;
    public Vector3 sadSpawnRot;
    public Vector3 seedSpawnLoc;
    public Vector3 seedSpawnRot;

	void Awake () {
        cage = transform.parent.gameObject;
	}
}
