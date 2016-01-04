using UnityEngine;
using System.Collections;

public class CageObj : MonoBehaviour {

    public GameController controller;
    private Color startColor;
    public Color highlightColor;
    private Renderer cageRenderer;
    private Canvas cageUI;
    public GameObject bird;
    public bool isPlayerCage;
    
    void Start () {
        controller = GameObject.Find("Controllers").GetComponent<GameController>();
        cageRenderer = GetComponentInChildren<Renderer>();
        cageUI = GetComponentInChildren<Canvas>();
        startColor = cageRenderer.material.color;
    }

    void OnMouseOver()
    {
        if(controller.gameState == GameController.GameState.Gameplay)
        {
            cageUI.enabled = true;
            cageRenderer.material.color = highlightColor;
            controller.SeedObject.GetComponent<SeedObject>().Highlight(true);
        }
    }

    void OnMouseExit()
    {
        if (controller.gameState == GameController.GameState.Gameplay)
        {
            cageUI.enabled = false;
            cageRenderer.material.color = startColor;
            controller.SeedObject.GetComponent<SeedObject>().Highlight(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == controller.SeedObject)
        {
            if (isPlayerCage)
            {
                controller.ChangeGameState(GameController.GameState.End);
            }
            else
            {
                controller.cageVisited = this;
                if (bird.GetComponent<FemaleBirdObj>().accept)
                    controller.ChangeGameState(GameController.GameState.Accept);
                else
                    controller.ChangeGameState(GameController.GameState.Reject);
            }
        }
    }
}
