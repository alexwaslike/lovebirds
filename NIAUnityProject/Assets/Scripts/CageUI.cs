using UnityEngine;
using System.Collections;

public class CageUI : MonoBehaviour {

    private Canvas _canvas;
    public GameController Controller;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void EnableUI()
    {
        if (!_canvas.enabled)
            _canvas.enabled = true;
    }

    public void DisableUI()
    {
        if (_canvas.enabled)
            _canvas.enabled = false;
    }
}
