using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmBall : MonoBehaviour
{
    [SerializeField] private float InitialVelocity;
    [SerializeField] private float Angle = 1;

    private Coroutine movementCoroutine;

    [SerializeField] private LineRenderer LineRenderer;

    [Header("Display Controls")]
    [SerializeField] [Range(10, 100)] private int LinePoints = 25;
    [SerializeField] [Range(0.01f, 0.25f)] private float TimeBetweenPoints = 0.1f;

    public Transform ReleasePosition;

    [SerializeField] private Rigidbody CharmBallRb;
    private LayerMask CharmBallCollisionLayerMask;

    [SerializeField] BallTest trajectory;

    private void Awake()
    {
        int charmBallLayer = CharmBallRb.gameObject.layer;
        for (int i = 0; i < 32; i++)
        {
            if (i != charmBallLayer && i != 6)
                Physics.IgnoreLayerCollision(charmBallLayer, i);

            if (Physics.GetIgnoreLayerCollision(charmBallLayer, i))
            {
                CharmBallCollisionLayerMask |= 1 << i;
            }
        }
    }

    private void OnEnable()
    {
        ControlManager.OnTapOverFan += ThrowCharm;
    }

    private void OnDisable()
    {
        ControlManager.OnTapOverFan -= ThrowCharm;
    }

    private void Update()
    {
        float angle = Angle * Mathf.Deg2Rad;

        DrawTrajectory(InitialVelocity, angle);
    }

    private void ThrowCharm(GameObject obj)
    {
        float angle = Angle * Mathf.Deg2Rad;
        if (movementCoroutine != null) StopCoroutine(movementCoroutine);
        movementCoroutine = StartCoroutine(MovementCoroutine(InitialVelocity, angle));
    }

    IEnumerator MovementCoroutine(float initVelocity, float angle)
    {
        float t = 0;
        while (t < 100) // until 100 seconds
        {
            Vector3 startPosition = ReleasePosition.position;
            float x = initVelocity * t * Mathf.Cos(angle);
            float y = initVelocity * t * Mathf.Sin(angle) + 0.5f * Physics.gravity.y * t * t;

            transform.position = startPosition + new Vector3(x, y, 0);
            t += Time.deltaTime;
            yield return null;
        }

    }

    private void DrawTrajectory(float initVelocity, float angle)
    {
        float x, y;
        TimeBetweenPoints = Mathf.Max(0.01f, TimeBetweenPoints);
        LineRenderer.positionCount = Mathf.CeilToInt(LinePoints / TimeBetweenPoints) + 1;
        Vector3 startPosition = ReleasePosition.position;
        int i = 0;
        LineRenderer.SetPosition(i, startPosition);
        for (float time = 0; time < LinePoints; time += TimeBetweenPoints)
        {
            i++;
            x = initVelocity * time * Mathf.Cos(angle);
            y = initVelocity * time * Mathf.Sin(angle) + 0.5f * Physics.gravity.y * time * time;
            Vector3 point = new Vector3(x, y, 0);
            LineRenderer.SetPosition(i, startPosition + point);

            Vector3 lastPosition = LineRenderer.GetPosition(i - 1);
            //if (Physics.Raycast(lastPosition, (point - lastPosition).normalized, out RaycastHit hit, ((point - lastPosition).normalized).magnitude, CharmBallCollisionLayerMask))
            if (Physics.Linecast(lastPosition, lastPosition, out RaycastHit hit, CharmBallCollisionLayerMask))
            {
                LineRenderer.SetPosition(i, hit.point);
                LineRenderer.positionCount = i + 1;
                Debug.Log(point);
                return;
            }
        }

        //x = initVelocity * LinePoints * Mathf.Cos(angle);
        //y = initVelocity * LinePoints * Mathf.Sin(angle) + 0.5f * Physics.gravity.y * LinePoints * LinePoints;
        //LineRenderer.SetPosition(i, startPosition + new Vector3(x, y, 0));

    }

}
