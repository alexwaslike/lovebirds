using UnityEngine;
using System.Collections;

public class SeedUI : MonoBehaviour {

    private Canvas _canvas;
	public GameController Controller;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

	void Update() 
	{
		Vector3 v = Controller.MainCamera.transform.position - transform.position;
		v.x = v.z = 0.0f;
		transform.LookAt( Controller.MainCamera.transform.position - v ); 
		transform.Rotate(0,180,0);
	}

	public void EnableUI()
    {
        if(!_canvas.enabled)
            _canvas.enabled = true;
    }

    public void DisableUI()
    {
        _canvas.enabled = false;
    }

}
