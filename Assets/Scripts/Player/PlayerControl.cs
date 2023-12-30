using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player movement control
public class PlayerControl : MonoBehaviour
{
    [SerializeField] ControlManager control;
    [SerializeField] private float speed;
    private float currentSpeed;
    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set { currentSpeed = value; }
    }

    public float NormalSpeed { get { return speed; } }

    void Start()
    {
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(currentSpeed * control.TargetVector * Time.deltaTime);

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime);
    }
}
