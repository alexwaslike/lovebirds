using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour{

    public MouseLook CharMouseLook;
    public Canvas canvas;

    private float _timeScale;

    private bool _gamePaused;
    public bool GamePaused
    {
        get { return _gamePaused; }
    }

	// Use this for initialization
	void Start () {
        _timeScale = Time.timeScale;
        
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Escape"))
        {
            if (_gamePaused)
            {
                Time.timeScale = _timeScale;
                CharMouseLook.enabled = true;
                canvas.enabled = false;
                _gamePaused = false;
            } else
            {
                Time.timeScale = 0.0f;
                CharMouseLook.enabled = false;
                canvas.enabled = true;
                _gamePaused = true;
            }
        }


	}
}
