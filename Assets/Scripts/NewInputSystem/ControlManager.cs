using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    private InputManager inputManager;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        inputManager.OnPerformTap += GetTap;
    }

    private void OnDisable()
    {
        inputManager.OnPerformTap -= GetTap;
    }

    private void GetTap(Vector2 v)
    {

    }

    
}
