using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BowControlManager : MonoBehaviour
{
    [SerializeField] ControlManager controlManager;
    [SerializeField] BallTrajectory ballTest;

    public delegate void ShootCharm();
    public event ShootCharm OnShootCharm;

    private InputManager inputManager;

    Coroutine coroutine;
    public Vector2 BowTargetVector { get; set; }

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
        Debug.Log("1");
        foreach (var obj in objectPool.pooledObjects)
        {
            var component = obj.GetComponent<CharmBall>();
            component.trajectory = ballTrajectory;
            component.bowControlManager = this;
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
        Debug.Log("Stop");
        StopCoroutine(coroutine);

        objectPool.GetPooledObject();
        if (OnShootCharm != null) OnShootCharm();
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
