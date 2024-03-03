using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingControl : MonoBehaviour
{
    //idle begin waiting catching complete
    public enum PlayerState
    { 
        IDLE, //default state
        BEGIN, //throw fishing pole
        WAITING, //wait for arrow key inputs
        FISHING, //arrow key input
        COMPLETE //success fail
    }


    public PlayerState playerState;

    float time = 0f;
    float timeDelay = 1f;

    public GameObject FishingText;
    public GameObject CaughtText;

    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.IDLE;

        FishingText.SetActive(false);
        CaughtText.SetActive(false);

        InputManager.Get.Input(ActionType.Player_Fishing).onInputEvent += OnFishingEvent;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnFishingEvent(InputState inputState)
    {
        if (inputState.IsClicked())
        {
			Debug.Log("Space was pressed");

            playerState = PlayerState.BEGIN;
            BeginFishing();
            StartCoroutine(CatchDelay());
		}
	}

    void BeginFishing()
    {
        Debug.Log("Begin");

        playerState = PlayerState.BEGIN;
        FishingText.SetActive(true);
    }


    //get arrow key inputs.
    void Fishing()
    {
        Debug.Log("Fishing");

        playerState = PlayerState.FISHING;
    }


    //success?
    void CaughtFish()
    {
        Debug.Log("Caught Object");

        playerState = PlayerState.COMPLETE;
        FishingText.SetActive(false);
        CaughtText.SetActive(true);
    }

    private IEnumerator CatchDelay()
    {
        //playerState = PlayerState.WAITING
        yield return new WaitForSeconds(2);
        
        CaughtFish();

        yield return null;
    }
}
