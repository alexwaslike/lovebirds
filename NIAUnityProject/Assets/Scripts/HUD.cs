using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

    public RawImage energyImage;
    public GameController controller;
    public float height = 15.0f;
    public float maxWidth = 250.0f;
    public float x = -350.0f;
    public float y = -200.0f;

	void Start () {
	    
	}
	
	void Update () {

        energyImage.rectTransform.sizeDelta = new Vector2(maxWidth*controller.energy, height);
        energyImage.rectTransform.anchoredPosition = new Vector2(x+energyImage.rectTransform.sizeDelta.x/2, y);

	}
}
