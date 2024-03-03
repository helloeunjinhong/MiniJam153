using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingControl : MonoBehaviour
{
    bool isFishing = false;
    bool isCaught = false;

    float time = 0f;
    float timeDelay = 1f;

    public GameObject FishingText;
    public GameObject CaughtText;

    // Start is called before the first frame update
    void Start()
    {
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
			
            StartFishing();
            StartCoroutine(CatchDelay());
		}
	}

    void StartFishing()
    {
        Debug.Log("Fishing Started");

        isCaught = false;
        
        isFishing = true;
        FishingText.SetActive(true);
    }

    void CaughtFish()
    {
        Debug.Log("Caught Object");

        isFishing = false;
        isCaught = true;

        FishingText.SetActive(false);
        CaughtText.SetActive(true);
    }

    private IEnumerator CatchDelay()
    {
        yield return new WaitForSeconds(2);
        CaughtFish();

        yield return null;
    }
}
