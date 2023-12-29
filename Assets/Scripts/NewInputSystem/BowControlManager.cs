using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BowControlManager : MonoBehaviour
{
    [SerializeField] ControlManager controlManager;
    [SerializeField] BallTrajectory ballTest;

    private InputManager inputManager;

    Coroutine coroutine;

    private Vector2 bowTargetVector;

    [SerializeField] float sensetivity = 200;

    [SerializeField] BallTrajectory ballTrajectory;

    private ObjectPool objectPool;


    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void Start()
    {
        objectPool = GetComponent<ObjectPool>();
        objectPool.Init();
        foreach (var obj in objectPool.pooledObjects)
        {
            var component = obj.GetComponent<CharmBall>();
            component.trajectory = ballTrajectory;
            component.ReleasePosition = ballTrajectory.transform.parent;
        }
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

    private void EndBow(Vector2 position)
    {
        prevVector = Vector2.zero;
        //Debug.Log("Stop");
        StopCoroutine(coroutine);

        var charmBall = objectPool.GetPooledObject();

        charmBall.transform.position = ballTrajectory.gameObject.transform.position;
        charmBall.SetActive(true);
        charmBall.GetComponent<CharmBall>().ThrowCharm();

        //hide bow
        ballTrajectory.ShowTrajectory = false;
    }
    Vector2 prevVector;
    Vector2 curVector;

    IEnumerator BowPositionCoroutine()
    {
        while (true)
        {
            curVector = inputManager.MousePosition();
            bowTargetVector = curVector;//- prevVector;
            bowTargetVector /= sensetivity;

            if (ballTest.velocity.z < 0
                && (Vector3.Dot(ballTest.transform.forward, new Vector3(ballTest.velocity.x + bowTargetVector.x, ballTest.velocity.y, ballTest.velocity.z + bowTargetVector.y)) > 0))
            {
                bowTargetVector = Vector2.zero;
            }
            else
            {

                //Debug.Log(bowTargetVector);
                ballTest.velocity = new Vector3(ballTest.velocity.x + bowTargetVector.x, ballTest.velocity.y, ballTest.velocity.z + bowTargetVector.y);
            }

            //Debug.Log(ballTest.velocity);
            prevVector = curVector;

            yield return new WaitForFixedUpdate();
        }
    }

    private void StartBow(Vector2 position)
    {
        //show bow
        ballTrajectory.ShowTrajectory = true;
        ballTrajectory.velocity = ballTrajectory.defaultVelocity;
        coroutine = StartCoroutine(BowPositionCoroutine());
    }


}
