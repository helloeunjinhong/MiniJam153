using System;
using UnityEngine;

[Serializable]
public enum ActionCategory // Input Action Map
{
    PLAYER, UI, MENU_MAIN, Count
}

public enum ActionType
{
    None,
    // Player Input Action Map
    Player_Fishing,
    Player_Move,
    Player_ToggleUI,

    // UI Input Action Map
    UI_Move,
    UI_ToggleUI,

    // Menu - Main Action Map
    Menu_Main_Move,

    Count
}

public partial class InputManager : MonoBehaviour
{
    private static Vector2Int[] m_actionMapRange = null;

    private static void InitActionTypeRange()
    {
        m_actionMapRange = new Vector2Int[(int)ActionCategory.Count];
        
        m_actionMapRange[(int)ActionCategory.PLAYER] = new Vector2Int(1, 3);
        m_actionMapRange[(int)ActionCategory.UI] = new Vector2Int(4, 5);
        m_actionMapRange[(int)ActionCategory.MENU_MAIN] = new Vector2Int(6, 6);
    }

    private static string ToString(ActionType type)
    {
        // Return string as same as input action in Input Action Asset.
        switch(type)
        {
            // Player Input Action Map
            case ActionType.Player_Fishing: return "Fishing";
            case ActionType.Player_Move: return "Move";
            case ActionType.Player_ToggleUI: return "ToggleUI";

            // UI Input Action Map
            case ActionType.UI_Move: return "Move";
            case ActionType.UI_ToggleUI: return "ToggleUI";

            // Menu - Main Action Map
            case ActionType.Menu_Main_Move: return "Move";
        }
        return "";
    }
    
    private bool IsEqual(string actionName, ActionType actionEnum)
    {
        return actionName == ToString(actionEnum);
    }

    private static string ToString(ActionCategory category)
    {
        switch(category)
        {
            case ActionCategory.PLAYER: return "Player";
            case ActionCategory.UI: return "UI";
            case ActionCategory.MENU_MAIN: return "Menu.Main";
        }
        return "";
    }

}