using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmBall : MonoBehaviour
{
    [SerializeField] private float InitialVelocity;
    [SerializeField] private float Angle = 1;

    private Coroutine movementCoroutine;

    public Transform ReleasePosition;

    public BallTrajectory trajectory;


    private void OnEnable()
    {
        StartCoroutine(SetFalseCoroutine());
    }

    IEnumerator SetFalseCoroutine()
    {
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
    }

    // OnBowEnd - ThrowCharm
    // OnBowStart - coroutine for z and y coordinates in velocity

    private void Update()
    {
        float angle = Angle * Mathf.Deg2Rad;
    }

    public void ThrowCharm()
    {
        if (this.gameObject.activeInHierarchy)
        {
            float angle = Angle * Mathf.Deg2Rad;
            if (movementCoroutine != null) StopCoroutine(movementCoroutine);
            movementCoroutine = StartCoroutine(MovementCoroutine(InitialVelocity, angle));
        }
    }

    IEnumerator MovementCoroutine(float initVelocity, float angle)
    {
        Vector3[] points = trajectory.CalculateLineArray();
        float t = 0;

        for(int i =0;i< points.Length;i++)
        {
            Vector3 startPosition = ReleasePosition.position;
            float x = initVelocity * t * Mathf.Cos(angle);
            float y = initVelocity * t * Mathf.Sin(angle) + 0.5f * Physics.gravity.y * t * t;

            transform.position = points[i];
            t += Time.deltaTime;
            yield return null;
        }

    }

}
