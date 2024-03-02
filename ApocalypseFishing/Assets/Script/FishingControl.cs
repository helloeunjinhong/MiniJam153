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
    }

    // Update is called once per frame
    void Update()
    {
        //Started Fishing by pressing SPACE
        if (InputManager.Get.Input(ActionType.Player_Fishing).IsClicked())
        {
            Debug.Log("Space was pressed");

			StartFishing();
        }

        if(isFishing == true)
        {
			//calculate time
			time = time + 1f * Time.deltaTime;

			//after delay
			if (time >= timeDelay)
			{
				CaughtFish();
                time = 0f;
			}
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

    void Delay(float seconds)
    {

    }    

    private IEnumerator CatchDelay()
    {
        yield return new WaitForSeconds(2);
    }
}
