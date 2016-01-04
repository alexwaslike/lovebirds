using UnityEngine;
using System.Collections;

public class SeedObject : MonoBehaviour {

    public GameController Controller;

    public float _xDisplacement = 0.0f;
    public float _yDisplacement = 0.5f;
    public float _zDisplacement = 1.0f;

    public float _xRot = 270.0f;
    public float _yRot = 75.0f;
    public float _zRot = 0.0f;

    
    private bool _pickedUp = false;
    public bool PickedUp
    {
        get { return _pickedUp; }
    }

    private Rigidbody _rigidBody;
    private SeedUI _UI;

    // color
    private Renderer renderer;
    public Color highlightColor;
    private Color originalColor;

    // sounds
    public AudioSource audioSource;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _UI = GetComponentInChildren<SeedUI>();
        audioSource = GetComponent<AudioSource>();

        renderer = GetComponentInChildren<Renderer>();
        originalColor = renderer.material.color;
    }

	void OnMouseDown()
    {
        gameObject.transform.parent = Controller.CharacterObject.transform;
        gameObject.transform.localPosition = new Vector3(_xDisplacement, _yDisplacement, _zDisplacement);
        gameObject.transform.localEulerAngles = new Vector3(_xRot, _yRot, _zRot);
        _rigidBody.detectCollisions = false;
        _rigidBody.isKinematic = true;
        _pickedUp = true;
        audioSource.PlayOneShot(audioSource.clip);
    }

    void OnMouseOver()
    {
        if (!_pickedUp) { 
            _UI.EnableUI();
            Highlight(true);
        }
    }

    void OnMouseExit()
    {
        _UI.DisableUI();
        Highlight(false);
    }

    public void Highlight(bool highlight)
    {
        if (highlight)
            renderer.material.color = highlightColor;
        else
            renderer.material.color = originalColor;
    }

    public void Release()
    {
        gameObject.transform.localPosition = new Vector3(_xDisplacement, _yDisplacement, _zDisplacement*2);
        gameObject.transform.parent = null;
        _rigidBody.detectCollisions = true;
        _rigidBody.isKinematic = false;
        _pickedUp = false;
        audioSource.PlayOneShot(audioSource.clip);
    }
}
