using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player movement control
public class PlayerControl : MonoBehaviour
{
    [SerializeField] ControlManager control;
    [SerializeField] private float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * control.TargetVector * Time.deltaTime);
    }
}
