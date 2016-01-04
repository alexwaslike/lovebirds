using UnityEngine;
using System.Collections;

public class FemaleBirdObj : MonoBehaviour {

    public GameController controller;
    public bool accept;
    private Renderer renderer;
    public float animSpeed = 1.0f;
    
	void Start () {

        // find game controller
        controller = GameObject.Find("Controllers").GetComponent<GameController>();

        // randomize whether bird will romance you or not
        // TODO: make sure on in env likes you!
        int accepts = (int)Random.Range(0,2);
        if (accepts == 0)
            accept = true;
        else
            accept = false;

        // randomize color
        renderer = GetComponentInChildren<Renderer>();
        int r = (int)GameController.RandomGaussian(controller.standardDev, controller.rBase);
        int g = (int)GameController.RandomGaussian(controller.standardDev, controller.gBase);
        int b = (int)GameController.RandomGaussian(controller.standardDev, controller.bBase);
        renderer.material.SetColor("_Color", new Color32((byte)r, (byte)g, (byte)b, 1));

    }
}
