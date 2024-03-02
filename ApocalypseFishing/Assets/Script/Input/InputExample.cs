using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputExample : MonoBehaviour
{
    private bool m_bShouldInit = true;

    private int count = 0;

    void Update()
    {
        if(m_bShouldInit)
        {
            InputManager.Get.Input(ActionType.Player_Move).onInputEvent += OnMoveEvent;

            m_bShouldInit = false;
        }

        if(InputManager.Get.Input(ActionType.Player_Fishing).IsClicked())
        {
            Debug.Log("Fishing");
        }
    }

    void OnMoveEvent(InputState inputState)
    {
        count = (count + 1) % 100;

        if(inputState.IsClicked())
        {
            Debug.Log(count.ToString() + ": " + "Move Event: START!");
        }
        if(inputState.IsPressing())
        {
            Debug.Log(count.ToString() + ": " + InputManager.Get.Input(ActionType.Player_Move).GetValue());
            // or you can use template function to get input value if you know value-type
            // Debug.Log(InputManager.Get.Input(ActionType.Player_Move).GetValue<Vector2>());
        }
        if(inputState.IsReleased())
        {
            Debug.Log(count.ToString() + ": " + "Move Event: End!");
        }
    }
}
