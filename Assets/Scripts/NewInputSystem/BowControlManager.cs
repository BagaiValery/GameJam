using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowControlManager : MonoBehaviour
{
    [SerializeField] ControlManager controlManager;
    [SerializeField] BallTest ballTest;

    private InputManager inputManager;

    Coroutine coroutine;
    public Vector2 BowTargetVector { get; set; }

    [SerializeField] float sensetivity = 200;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        inputManager.OnEndBow += EndBow;
        inputManager.OnStartTouch += StartBow;
    }

    void OnDisable()
    {
        inputManager.OnEndBow -= EndBow;
        inputManager.OnStartTouch -= StartBow;
    }

    void Update()
    {

    }

    private void EndBow(Vector2 position)
    {
        prevVector = Vector2.zero;
        Debug.Log("Stop");
        StopCoroutine(coroutine);

    }
    Vector2 prevVector;
    Vector2 curVector;

    IEnumerator BowPositionCoroutine()
    {
        while (true)
        {
            //Debug.Log(curVector.ToString() + prevVector.ToString());

            curVector = inputManager.MousePosition();
            BowTargetVector = curVector;//- prevVector;

            BowTargetVector /= sensetivity;
            ballTest.velocity = new Vector3(ballTest.velocity.x + BowTargetVector.x, ballTest.velocity.y, ballTest.velocity.z + BowTargetVector.y);
            Debug.Log(ballTest.velocity);
            prevVector = curVector;

            yield return new WaitForFixedUpdate();
        }
    }

    private void StartBow(Vector2 position)
    {
        //show bow
        coroutine = StartCoroutine(BowPositionCoroutine());
    }


}
