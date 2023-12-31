using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarmBoom : MonoBehaviour
{
    [SerializeField] private LayerMask whatAreCharmable;

    public void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckForCharmable();
        Destroy(this, .5f);
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Booooooom!");
       //     CheckForCharmable();
        }
    }

    private void CheckForCharmable()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, whatAreCharmable);
        foreach (Collider c in colliders)
        {
            if (c.GetComponent<NPC>())
            {
                c.GetComponent<NPC>().SetFanBehaviour();
            }
        }
    }
}
