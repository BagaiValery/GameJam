using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmBall : MonoBehaviour
{
    private Coroutine movementCoroutine;

    public Transform ReleasePosition;

    public BallTrajectory trajectory;

    [SerializeField] private LayerMask whatAreCharmable;

    [SerializeField] private GameObject particles;
    [SerializeField] private float damageRadius = 10f;

    bool move = true;
    public bool IsMove { get { return move; } set { move = value; } }

    #region CollisionsWithPeoples

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Star")
        {
            CheckForCharmable();
            move = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(DieCoroutine());
        }
    }

    IEnumerator DieCoroutine()
    {
        particles.SetActive(true);
        particles.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.3f);
        this.gameObject.SetActive(false);
    }

    private void CheckForCharmable()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius, whatAreCharmable);
        foreach (Collider c in colliders)
        {
            if (c.GetComponent<Assets.Scripts.NPC>())
            {
                c.GetComponent<Assets.Scripts.NPC>().SetFanBehaviour();
            }
        }
    }

    #endregion

    #region Movement

    private void OnEnable()
    {
        particles.SetActive(false);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        move = true;

        StartCoroutine(SetFalseCoroutine());
    }

    IEnumerator SetFalseCoroutine()
    {
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
    }

    // OnBowEnd - ThrowCharm
    // OnBowStart - coroutine for z and y coordinates in velocity

    public void ThrowCharm()
    {
        if (this.gameObject.activeInHierarchy)
        {
            if (movementCoroutine != null) StopCoroutine(movementCoroutine);
            movementCoroutine = StartCoroutine(MovementCoroutine());
        }
    }

    IEnumerator MovementCoroutine()
    {
        Vector3[] points = trajectory.CalculateLineArray();
        float t = 0;

        for (int i = 0; i < points.Length; i++)
        {
            if (move)
            {
                transform.position = points[i];
                t += Time.deltaTime;
                yield return null;
            }
        }


    }

    #endregion

}
