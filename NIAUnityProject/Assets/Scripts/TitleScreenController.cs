using UnityEngine;
using System.Collections;

public class TitleScreenController : MonoBehaviour {

    public Canvas creditsCanvas;

	public void StartGame()
    {
        Application.LoadLevel("Game");
    }

    public void ShowCredits()
    {
        creditsCanvas.enabled = true;
    }

    public void HideCredits()
    {
        creditsCanvas.enabled = false;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
