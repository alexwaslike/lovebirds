using UnityEngine;
using System.Collections;

public class AnimationObject : MonoBehaviour {

    public GameController controller;
    public GameObject femaleBird;

    void Start() { }

    public void RejectAnimationFinished()
    {
        controller.RejectAnimationFinished();
        Destroy(this.gameObject);
    }

    public void AcceptAnimationFinished()
    {
        controller.AcceptAnimationFinished();
        Destroy(this.gameObject);
    }

    public void SeedAnimationFinished()
    {
        controller.SeedAnimationFinished();
        Destroy(this.gameObject);
    }

    public void CuddleAnimationFinished()
    {
        controller.CuddleAnimationFinished();
        Destroy(this.gameObject);
    }

    public void BirdStolenAnimationFinished()
    {
        controller.BirdStolenAnimationFinished();
        Destroy(this.gameObject);
    }

    public void SleepAnimationFinished()
    {
        controller.SleepAnimationFinished();
        Destroy(this.gameObject);
    }

    public void SadAnimationFirstFrame()
    {
        if(controller != null)
            controller.SadBackwardsDone();
    }
}
