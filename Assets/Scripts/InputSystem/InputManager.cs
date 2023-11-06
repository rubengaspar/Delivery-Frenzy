using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    // Instance of the singleton
    public static InputManager Instance;

    public PlayerInputActions inputActions;

    public InputActionMap currentActionMap = InputActionMap.NormalMode;

    public enum InputActionMap
    {
        NormalMode,
        PrecisionMode,
        UIMode
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            inputActions = new PlayerInputActions();
            inputActions.NormalMode.Enable(); // Initial game mode
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject); // For scene changes, so that we keep only 1 instance
        }
    }

    private void OnDestroy()
    {
        DisableAllActionMaps();
    }

    void ActivateActionMap (InputActionMap actionMap)
    {
        // Disable all action maps and activate the called action map
        DisableAllActionMaps();
        switch(actionMap)
        {
            case InputActionMap.NormalMode:
                inputActions.NormalMode.Enable();
                currentActionMap = InputActionMap.NormalMode;
                break;
            case InputActionMap.PrecisionMode: 
                inputActions.PrecisionMode.Enable();
                currentActionMap = InputActionMap.PrecisionMode;
                break;
        }
    }

    void DisableAllActionMaps()
    {
        inputActions.NormalMode.Disable();
        inputActions.PrecisionMode.Disable();
    }

    public Vector2 GetMovementInput()
    {
        return inputActions.NormalMode.Movement.ReadValue<Vector2>();
    }

}
