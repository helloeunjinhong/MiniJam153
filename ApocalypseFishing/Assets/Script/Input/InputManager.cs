using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class InputState
{
    [Flags] private enum StateBit { NONE = 0, BEGIN = 1, PRESSING = 2, RELEASE = 4 }

    private StateBit state = 0x00;
    private object value = null;
    private int lastFrame = 0;
    
    public delegate void InputEvent(InputState inputState);
    public InputEvent onInputEvent = null;

    public IEnumerator SetState(bool isStarted, bool isReleased, object value, InputActionType type)
    {
        if(isStarted == false && isReleased == false) // Is pressing
        {
            this.value = value;
        }
        else
        {
            // Wait until last frame is completed
            if(type == InputActionType.Button)
            {
                int currFrame = Time.frameCount;
                if(currFrame - lastFrame < 2)
                {
                    yield return new WaitForEndOfFrame();
                }
                lastFrame = currFrame;
            }

            this.value = value;
            if(isStarted)   state = StateBit.BEGIN | StateBit.PRESSING;
            if(isReleased)  state = StateBit.RELEASE;
        }

        onInputEvent?.Invoke(this);
        yield return null;
    }

    public object GetValue() { return value == null ? 0 : value; }
    public T GetValue<T>() { return (T)value; }

    public bool IsClicked() // Detect only once per input
    {
        if((state & StateBit.BEGIN) == StateBit.BEGIN) { state &= ~StateBit.BEGIN; return true; }
        return false;
    }

    public bool IsPressing()
    {
        return (state & StateBit.PRESSING) == StateBit.PRESSING;
    }

    public bool IsReleased()
    {
        if((state & StateBit.RELEASE) == StateBit.RELEASE) { state &= ~StateBit.RELEASE; return true; }
        return false;
    }

    public bool IsAnyEvent() { return state != StateBit.NONE; }
}

public partial class InputManager : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset m_inputAsset = null;
    [SerializeField]
    private ActionCategory m_currInputCategory = new ActionCategory();

    private InputActionMap[] m_actionMap = null;

    private InputState[] m_states = null;

    private static InputManager m_instance = null;

    private bool m_isInitialized = false;

    public static InputManager Get
    {
        get
        {
            if(m_instance == null) m_instance = FindObjectOfType<InputManager>();
            if(m_instance == null)
            {
                GameObject obj = new GameObject("InputManager");
                m_instance = obj.AddComponent<InputManager>();
            }
            m_instance.Initialize();
            //DontDestroyOnLoad(m_instance);
            return m_instance;
        }
    }    

    public void Initialize()
    {
        if(!m_isInitialized)
        {
            m_actionMap = new InputActionMap[(int)ActionCategory.Count];
            for(int i = 0; i < (int)ActionCategory.Count; ++i)
            {
                m_actionMap[i] = m_inputAsset.FindActionMap(ToString((ActionCategory)i));
                m_actionMap[i].actionTriggered += context=>{ OnActionTriggered(context); };
            }
            InitActionTypeRange();

            m_states = new InputState[(int)ActionType.Count];
            for(int i = 0; i < (int)ActionType.Count; ++i)
            {
                m_states[i] = new InputState();
            }

            ChangeInputMap(m_currInputCategory);
            // Set input event to switch the current input action map
            m_states[(int)ActionType.Player_ToggleUI].onInputEvent += (InputState state)=> 
            {
                if(state.IsClicked()) ChangeInputMap(ActionCategory.UI);
            };
            m_states[(int)ActionType.UI_ToggleUI].onInputEvent += (InputState state)=> 
            {
                if(state.IsClicked()) ChangeInputMap(ActionCategory.PLAYER);
            };
            
            m_isInitialized = true;
        }
    }

    private void Update()
    {
        foreach(InputState state in m_states)
        {  
            if(state.IsAnyEvent())
                state.onInputEvent?.Invoke(state);
        }
    }

    public InputState Input(ActionType actionType)
    {
        return m_states[(int)actionType];
    }

    public void ChangeInputMap(ActionCategory inputActionMapToEnable)
    {
        m_actionMap[(int)m_currInputCategory].Disable();
        m_actionMap[(int)inputActionMapToEnable].Enable();
        m_currInputCategory = inputActionMapToEnable;
        Debug.Log("[InputManager] Active InputActionMap has been changed to " + m_currInputCategory.ToString());
    }

    private void OnActionTriggered(InputAction.CallbackContext context)
    {
        // Find ActionType in current input action map
        Vector2Int range = m_actionMapRange[(int)m_currInputCategory];
        for(int i = range.x; i <= range.y; ++i)
        {
            if(IsEqual(context.action.name, (ActionType)i))
            {
                // Set input state
                StartCoroutine(m_states[i].SetState(
                    context.started,
                    !context.action.IsInProgress(),
                    context.ReadValueAsObject(), 
                    context.action.type));
                break;
            }
        }
    }
}
