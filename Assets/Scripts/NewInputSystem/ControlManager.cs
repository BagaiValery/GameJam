using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public Vector3 TargetVector { get; set; }
    public Vector3 RightTargetVector { get; set; }
    public Vector3 LeftTargetVector { get; set; }

    Coroutine moveRightCoroutine;
    Coroutine moveLeftCoroutine;

    private InputManager inputManager;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        inputManager.OnPerformTap += GetTap;

        inputManager.OnStartLeft += StartMovement;
        inputManager.OnStartRight += StartMovement;

        inputManager.OnEndRight += EndRightMovement;
        inputManager.OnEndLeft += EndLeftMovement;
    }

    private void OnDisable()
    {
        inputManager.OnPerformTap -= GetTap;

        inputManager.OnStartLeft -= StartMovement;
        inputManager.OnStartRight -= StartMovement;

        inputManager.OnEndRight -= EndRightMovement;
        inputManager.OnEndLeft -= EndLeftMovement;
    }

    private void GetTap(Vector2 v)
    {

    }

    #region Left-Right Movement
    public void StartMovement(Vector3 vector3, bool right)
    {
        switch (right)
        {
            case true: // right
                RightTargetVector = vector3;
                moveRightCoroutine = StartCoroutine(SetMovementVector(vector3));
                break;
            case false: // left
                LeftTargetVector = vector3;
                moveLeftCoroutine = StartCoroutine(SetMovementVector(vector3));
                break;
        }
    }

    public void EndRightMovement()
    {
        StopCoroutine(moveRightCoroutine);
        TargetVector = Vector3.zero;
        RightTargetVector = Vector3.zero;
    }
    public void EndLeftMovement()
    {
        StopCoroutine(moveLeftCoroutine);
        TargetVector = Vector3.zero;
        LeftTargetVector = Vector3.zero;
    }

    IEnumerator SetMovementVector(Vector3 vector3)
    {
        while (true)
        {
            TargetVector = Vector3.zero;
            TargetVector += LeftTargetVector + RightTargetVector;
            yield return null;
        }
    }
    #endregion
}
