using UnityEngine;
using System.Collections;

public class CharacterObject : MonoBehaviour {

    public GameController Controller;

    public float originalX;
    public float originalY;
    public float originalZ;

    private CharacterController movementController;
    private MouseLook mouseLook;
    
	void Start () {
        movementController = GetComponent<CharacterController>();
        mouseLook = GetComponent<MouseLook>();
	}
	
    public void RemoveControl()
    {
        movementController.enabled = false;
        mouseLook.enabled = false;
        Controller.PauseMenuScript.enabled = false;
    }

    public void ReturnControl()
    {
        movementController.enabled = true;
        mouseLook.enabled = true;
        Controller.PauseMenuScript.enabled = true;
    }
}
