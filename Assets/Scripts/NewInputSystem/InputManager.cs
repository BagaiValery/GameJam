using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    public delegate void StartTouch(Vector2 position);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;

    public delegate void PerformTap(Vector2 position);
    public event PerformTap OnPerformTap;

    public delegate void EndBow(Vector2 position);
    public event EndBow OnEndBow;

    public delegate void StartLeft(Vector3 vector3, bool right);
    public event StartLeft OnStartLeft;
    public delegate void StartRight(Vector3 vector3, bool right);
    public event StartRight OnStartRight;
    
    public delegate void EndLeft();
    public event EndLeft OnEndLeft;
    public delegate void EndRight();
    public event EndRight OnEndRight;

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
        playerControls.ComputerInputMap.MouseHoldButton.started += ctx => StartTouchPrimary(ctx);
        playerControls.ComputerInputMap.MouseHoldButton.canceled += ctx => EndSwipe(ctx);

        playerControls.ComputerInputMap.MouseTap.started += ctx => PerformPrimaryTap(ctx);

        playerControls.ComputerInputMap.LeftArrow.started += ctx => StartLeftArrow(ctx);
        playerControls.ComputerInputMap.RightArrow.started += ctx => StartRightArrow(ctx);
        playerControls.ComputerInputMap.LeftArrow.canceled += ctx => EndLeftArrow(ctx);
        playerControls.ComputerInputMap.RightArrow.canceled += ctx => EndRightArrow(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        //if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(mainCamera, locMapInputControls.TouchScreenControl.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
        if (OnStartTouch != null) OnStartTouch(playerControls.ComputerInputMap.MouseHoldPosition.ReadValue<Vector2>());
    }

    private void EndSwipe(InputAction.CallbackContext context)
    {
        if (OnEndBow != null) OnEndBow(playerControls.ComputerInputMap.MouseHoldPosition.ReadValue<Vector2>());
    }

    public Vector2 MousePosition()
    {
        //return Utils.ScreenToWorld(mainCamera, locMapInputControls.TouchScreenControl.PrimaryPosition.ReadValue<Vector2>());
        return playerControls.ComputerInputMap.MouseHoldPosition.ReadValue<Vector2>();
    }

    private void PerformPrimaryTap(InputAction.CallbackContext context)
    {
        if (OnPerformTap != null) OnPerformTap(playerControls.ComputerInputMap.MouseTapPosition.ReadValue<Vector2>());
    }

    private void StartLeftArrow(InputAction.CallbackContext context)
    {
        if (OnStartLeft != null) OnStartLeft(Vector3.left, false);
    }
    
    private void StartRightArrow(InputAction.CallbackContext context)
    {
        if (OnStartRight != null) OnStartRight(Vector3.right, true);
    }
    
    private void EndLeftArrow(InputAction.CallbackContext context)
    {
        if (OnEndLeft != null) OnEndLeft();
    }
    
    private void EndRightArrow(InputAction.CallbackContext context)
    {
        if (OnEndRight != null) OnEndRight();
    }
}
