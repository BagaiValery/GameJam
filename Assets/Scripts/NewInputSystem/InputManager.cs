using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;

    public delegate void PerformTap(Vector2 position);
    public event PerformTap OnPerformTap;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        playerControls.ComputerInputMap.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        playerControls.ComputerInputMap.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);

        playerControls.ComputerInputMap.MouseTap.started += ctx => PerformPrimaryTap(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        //if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(mainCamera, locMapInputControls.TouchScreenControl.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
        if (OnStartTouch != null) OnStartTouch(playerControls.ComputerInputMap.MouseTapPosition.ReadValue<Vector2>(), (float)context.startTime);
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        //if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(mainCamera, locMapInputControls.TouchScreenControl.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
        if (OnEndTouch != null) OnEndTouch(playerControls.ComputerInputMap.MouseTapPosition.ReadValue<Vector2>(), (float)context.time);
    }

    public Vector2 PrimaryPosition()
    {
        //return Utils.ScreenToWorld(mainCamera, locMapInputControls.TouchScreenControl.PrimaryPosition.ReadValue<Vector2>());
        return playerControls.ComputerInputMap.MouseTapPosition.ReadValue<Vector2>();
    }

    private void PerformPrimaryTap(InputAction.CallbackContext context)
    {
        if (OnPerformTap != null) OnPerformTap(playerControls.ComputerInputMap.MouseTapPosition.ReadValue<Vector2>());
    }
}
