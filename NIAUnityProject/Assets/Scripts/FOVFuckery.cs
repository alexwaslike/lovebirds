using UnityEngine;
using System.Collections;

public class FOVFuckery : MonoBehaviour {

	public float fovScaler = 0.5f;

	private Camera charCamera;
	private CharacterController charController;
	private float originalFOV;

	private bool increaseFOV;

	// Use this for initialization
	void Start () {
		charCamera = GetComponentInChildren<Camera>();
		originalFOV = charCamera.fieldOfView;
		charController = GetComponent<CharacterController>();
		increaseFOV = false;
	}

	void Update(){
		if(charController.velocity.magnitude > 0)
			increaseFOV = true;
		else
			increaseFOV = false;

		if(increaseFOV)
			charCamera.fieldOfView += fovScaler;
		else if(charCamera.fieldOfView > originalFOV)
			charCamera.fieldOfView -= fovScaler;

	}


}
