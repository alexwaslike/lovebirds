using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public enum GameState{
        Begin,
        Gameplay,
        Accept,
        Reject,
        EnergyOut,
        End
    }
    public GameState gameState;

    // ------------GAME OBJECTS------------
    public GameObject CharacterObject;
    public GameObject SeedObject;
    public GameObject PlayerCage;

	public Camera MainCamera;

    private CharacterObject CharacterDO;
    private SeedObject SeedDO;

    public PauseGame PauseMenuScript;

    public GameObject EndCanvasObject;

    // ------------ANIMATION PREFABS------------
    public GameObject rejectAnimation;
    public GameObject acceptAnimation;
    public GameObject cuddleAnimation;
    public GameObject snatchAnimation;
    public GameObject sadAnimation;
    public GameObject seedAnimation;
    public GameObject sleepAnimation;
    public GameObject happyAnimation;

    // ------------CAMERA ANGLES------------
    public Vector3 originalCameraLoc = new Vector3(0.0f, 0.88f, 0.0f);
    public Vector3 originalCameraRot = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 newCameraLoc = new Vector3(-6.0f, 1.5f, -2.0f);
    public Vector3 newCameraRot = new Vector3(8.0f, 60.0f, 350.0f);
    public Vector3 cuddleCameraLoc;
    public Vector3 cuddleCameraRot;
    public Vector3 snatchCameraLoc;
    public Vector3 snatchCameraRot;
    public Vector3 seedCameraLoc;
    public Vector3 seedCameraRot;
    public Vector3 sleepCameraLoc;
    public Vector3 sleepCameraRot;
    public Vector3 endCameraLoc;
    public Vector3 endCameraRot;

    // ------------CONTROLLER VARS------------
    public float energy;
    public float energyChange;
    public CageObj cageVisited;
    private bool reset;

    // ------------BIRD GENERATOR VARS------------
    // original bird color
    public Material originalFemaleColor;
    // object
    public GameObject femaleBirdPrefab;
    // color
    public float rBase;
    public float gBase;
    public float bBase;
    public float standardDev = 20.0f;
    // locations
    [SerializeField]
    public List<SpawnLocation> spawnLocs;

    // ------------SEED VARS------------
    public Vector3 originalSeedLocation;
    public Vector3 originalSeedRotation;

    void Start () {

        // find objects
        PauseMenuScript.CharMouseLook = CharacterObject.GetComponent<MouseLook>();

        CharacterDO = CharacterObject.GetComponent<CharacterObject>();
        CharacterDO.Controller = this;

        SeedObject.GetComponent<SeedObject>().Controller = this;
		SeedObject.GetComponentInChildren<SeedUI>().Controller = this;

        SeedDO = SeedObject.GetComponent<SeedObject>();

        ChangeGameState(GameState.Begin);

    }
	
	void Update () {

        if(gameState == GameState.Gameplay)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (SeedDO.PickedUp)
                {
                    SeedDO.Release();
                }
            }

            energy -= energyChange * Time.deltaTime;
            if (energy <= 0)
            {
                ChangeGameState(GameState.EnergyOut);
            }
        }
        
	}

    public void PlaySadBackwards()
    {
        // move camera to frame bird
        MainCamera.transform.localPosition = sleepCameraLoc;
        MainCamera.transform.localEulerAngles = sleepCameraRot;
        
        GameObject sadObj = Instantiate(happyAnimation) as GameObject;
        sadObj.GetComponent<AnimationObject>().controller = this;
        // place sad animation at character loc
        sadObj.transform.parent = CharacterObject.gameObject.transform;
        sadObj.transform.localPosition = new Vector3(0, -.7f, -7.54f);
        sadObj.transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    public void SadBackwardsDone()
    {
        // place camera above bird
        MainCamera.transform.localPosition = endCameraLoc;
        MainCamera.transform.localEulerAngles = endCameraRot;

        // "the end"
        EndCanvasObject.GetComponent<Canvas>().enabled = true;
    }

    public void PlaySleepAnimation()
    {
        // move camera to frame bird
        MainCamera.transform.localPosition = sleepCameraLoc;
        MainCamera.transform.localEulerAngles = sleepCameraRot;

        // place sleep animation at character loc
        GameObject sleepObj = Instantiate(sleepAnimation) as GameObject;
        sleepObj.transform.parent = CharacterObject.gameObject.transform;
        sleepObj.transform.localPosition = new Vector3(0, -.7f, -7.54f);
        sleepObj.transform.localEulerAngles = new Vector3(0, 0, 0);
        sleepObj.GetComponent<AnimationObject>().controller = this;
    }

    public void SleepAnimationFinished()
    {
        Debug.Log("well, we made it here");
        ChangeGameState(GameState.Gameplay);
    }

    public void RejectAnimationFinished()
    {
        ChangeGameState(GameState.Gameplay);
    }

    public void PlayGivingSeedAnimation()
    {
        // move camera to frame birds
        MainCamera.transform.parent = PlayerCage.transform;
        MainCamera.transform.localPosition = seedCameraLoc;
        MainCamera.transform.localEulerAngles = seedCameraRot;

        // place seed animation at spawn point
        GameObject cuddleObj = Instantiate(seedAnimation) as GameObject;
        cuddleObj.transform.parent = PlayerCage.gameObject.transform;
        cuddleObj.transform.localPosition = PlayerCage.GetComponentInChildren<SpawnLocation>().seedSpawnLoc;
        cuddleObj.transform.localEulerAngles = PlayerCage.GetComponentInChildren<SpawnLocation>().seedSpawnRot;
        cuddleObj.GetComponent<AnimationObject>().controller = this;

        // change female bird color to original girl bird color
        GameObject femaleBird = cuddleObj.GetComponent<AnimationObject>().femaleBird;
        femaleBird.GetComponent<SkinnedMeshRenderer>().material = originalFemaleColor;

    }

    public void SeedAnimationFinished()
    {
        PlayCuddleAnimation();
    }

    public void PlayCuddleAnimation()
    {
        // move camera to frame birds
        MainCamera.transform.parent = PlayerCage.transform;
        MainCamera.transform.localPosition = cuddleCameraLoc;
        MainCamera.transform.localEulerAngles = cuddleCameraRot;

        // place accept animation at female bird spawn point
        GameObject cuddleObj = Instantiate(cuddleAnimation) as GameObject;
        cuddleObj.transform.parent = PlayerCage.gameObject.transform;
        cuddleObj.transform.localPosition = PlayerCage.GetComponentInChildren<SpawnLocation>().cuddleSpawnLoc;
        cuddleObj.transform.localEulerAngles = PlayerCage.GetComponentInChildren<SpawnLocation>().animationSpawnRot;
        cuddleObj.GetComponent<AnimationObject>().controller = this;

        // change female bird color to current bird color
        GameObject femaleBird;
        if (gameState == GameState.Begin)
        {
            femaleBird = cuddleObj.GetComponent<AnimationObject>().femaleBird;
            femaleBird.GetComponent<SkinnedMeshRenderer>().material = originalFemaleColor;
        }
        else
        {
            femaleBird = cuddleObj.GetComponent<AnimationObject>().femaleBird;
            femaleBird.GetComponent<SkinnedMeshRenderer>().material = cageVisited.GetComponentInChildren<FemaleBirdObj>().GetComponentInChildren<SkinnedMeshRenderer>().material;
        }
    }

    public void CuddleAnimationFinished()
    {
        PlayBirdStolenAnimation();
    }

    public void PlayBirdStolenAnimation()
    {
        // move camera to frame birds
        MainCamera.transform.parent = PlayerCage.transform;
        MainCamera.transform.localPosition = snatchCameraLoc;
        MainCamera.transform.localEulerAngles = snatchCameraRot;

        // place sad boy animation at spawn point
        GameObject sadBoyObj = Instantiate(sadAnimation) as GameObject;
        sadBoyObj.transform.parent = PlayerCage.gameObject.transform;
        sadBoyObj.transform.localPosition = PlayerCage.GetComponentInChildren<SpawnLocation>().sadSpawnLoc;
        sadBoyObj.transform.localEulerAngles = PlayerCage.GetComponentInChildren<SpawnLocation>().sadSpawnRot;

        // place accept animation at spawn point
        GameObject snatchObj = Instantiate(snatchAnimation) as GameObject;
        snatchObj.transform.parent = PlayerCage.gameObject.transform;
        snatchObj.transform.localPosition = PlayerCage.GetComponentInChildren<SpawnLocation>().handSpawnLoc;
        snatchObj.transform.localEulerAngles = PlayerCage.GetComponentInChildren<SpawnLocation>().handSpawnRot;
        snatchObj.GetComponent<AnimationObject>().controller = this;

        // change female bird color to current bird color
        GameObject femaleBird;
        if (gameState == GameState.Begin)
        {
            femaleBird = snatchObj.GetComponent<AnimationObject>().femaleBird;
            femaleBird.GetComponent<MeshRenderer>().material = originalFemaleColor;
        }
        else
        {
            femaleBird = snatchObj.GetComponent<AnimationObject>().femaleBird;
            femaleBird.GetComponent<MeshRenderer>().material = cageVisited.GetComponentInChildren<FemaleBirdObj>().GetComponentInChildren<SkinnedMeshRenderer>().material;
        }
            
        
    }

    public void BirdStolenAnimationFinished()
    {
        reset = true;
        Destroy(GameObject.Find("sadboy2(Clone)"));
        ChangeGameState(GameState.Gameplay);
    }

    public void AcceptAnimationFinished()
    {
        PlayCuddleAnimation();
    }

    public void ChangeGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.Begin:
                gameState = GameState.Begin;        PlayGivingSeedAnimation();  break;
            case GameState.Gameplay:
                gameState = GameState.Gameplay;     BeginInCage();  break;
            case GameState.Accept:
                gameState = GameState.Accept;       Accept();       break;
            case GameState.Reject:
                gameState = GameState.Reject;       Reject();       break;
            case GameState.EnergyOut:
                gameState = GameState.EnergyOut;    EnergyOut();    break;
            case GameState.End:
                gameState = GameState.End;          End();          break;
        }
    }

    private void BeginInCage()
    {
        if (reset)
        {
            // initiate random vars
            rBase = Random.Range(0.0f, 255.0f);
            gBase = Random.Range(0.0f, 255.0f);
            bBase = Random.Range(0.0f, 255.0f);

            // remove old NPCs
            RemoveOldBirds();

            // generate NPCs
            GenerateFemaleBirds();

            // move seed to original loc
            SeedObject.transform.parent = null;
            SeedObject.transform.position = originalSeedLocation;
            SeedObject.transform.eulerAngles = originalSeedRotation;

            // place character in cage
            CharacterObject.transform.position = new Vector3(CharacterDO.originalX, CharacterDO.originalY, CharacterDO.originalZ);
        }

        if(cageVisited != null)
            cageVisited.GetComponentInChildren<FemaleBirdObj>().gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;

        // place camera in original pos
        MainCamera.transform.parent = CharacterObject.transform;
        MainCamera.transform.localPosition = originalCameraLoc;
        MainCamera.transform.localEulerAngles = originalCameraRot;

        // give player control
        CharacterDO.ReturnControl();

    }

    private void Accept()
    {
        CharacterDO.RemoveControl();

        // move camera to frame birds
        MainCamera.transform.localPosition = newCameraLoc;
        MainCamera.transform.localEulerAngles = newCameraRot;

        // place accept animation at female bird spawn point
        GameObject acceptObj = Instantiate(acceptAnimation) as GameObject;
        acceptObj.transform.parent = cageVisited.gameObject.transform;
        acceptObj.transform.localPosition = cageVisited.GetComponentInChildren<SpawnLocation>().acceptSpawnLoc;
        acceptObj.transform.localEulerAngles = cageVisited.GetComponentInChildren<SpawnLocation>().animationSpawnRot;
        acceptObj.GetComponent<AnimationObject>().controller = this;

        // change female bird color to current bird color
        GameObject femaleBird = GameObject.FindGameObjectWithTag("FemaleBird");
        femaleBird.GetComponent<SkinnedMeshRenderer>().material = cageVisited.GetComponentInChildren<FemaleBirdObj>().GetComponentInChildren<SkinnedMeshRenderer>().material;

        // hide female bird
        cageVisited.GetComponentInChildren<FemaleBirdObj>().gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;

        reset = true;
    }

    private void Reject()
    {
        CharacterDO.RemoveControl();

        // move camera to frame birds
        MainCamera.transform.localPosition = newCameraLoc;
        MainCamera.transform.localEulerAngles = newCameraRot;

        // place rejection animation at female bird spawn point
        GameObject rejectObj = Instantiate(rejectAnimation) as GameObject;
        rejectObj.transform.parent = cageVisited.gameObject.transform;
        rejectObj.transform.localPosition = cageVisited.GetComponentInChildren<SpawnLocation>().animationSpawnLoc;
        rejectObj.transform.localEulerAngles = cageVisited.GetComponentInChildren<SpawnLocation>().animationSpawnRot;
        rejectObj.GetComponent<AnimationObject>().controller = this;

        // change female bird color to current bird color
        GameObject femaleBird = GameObject.FindGameObjectWithTag("FemaleBird");
        femaleBird.GetComponent<SkinnedMeshRenderer>().material = cageVisited.GetComponentInChildren<FemaleBirdObj>().GetComponentInChildren<SkinnedMeshRenderer>().material;

        // hide female bird
        cageVisited.GetComponentInChildren<FemaleBirdObj>().gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;

        reset = false;
    }

    private void End()
    {
        CharacterDO.RemoveControl();

        // play ending animation
        PlaySadBackwards();
    }

    private void EnergyOut()
    {
        CharacterDO.RemoveControl();

        // reset energy to 100
        energy = 1.0f;

        // play sleeping animation
        PlaySleepAnimation();

    }

    private void GenerateFemaleBirds()
    {
        for(int i=0; i<spawnLocs.Count; i++)
        {
            // create bird in that location
            GameObject birb = GameObject.Instantiate(femaleBirdPrefab, spawnLocs[i].transform.position, spawnLocs[i].transform.rotation) as GameObject;
            birb.transform.parent = spawnLocs[i].transform.parent;
            spawnLocs[i].cage.GetComponent<CageObj>().bird = birb;
        }
    }

    private void RemoveOldBirds()
    {
        for (int i = 0; i < spawnLocs.Count; i++)
        {
            Destroy(spawnLocs[i].cage.GetComponent<CageObj>().bird.gameObject);
        }
    }

    public static float RandomGaussian(float standardDev, float median)
    {
        float randomVal = 0.0f;

        float U1 = Random.Range(0.0f, 1.0f);
        float U2 = Random.Range(0.0f, 1.0f);

        randomVal = Mathf.Sqrt(-2 * Mathf.Log(U1)) * Mathf.Cos(2 * Mathf.PI * U2);

        randomVal = randomVal * standardDev + median;

        return randomVal;
    }
}
