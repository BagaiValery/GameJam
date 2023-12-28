using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBehaiver : MonoBehaviour
{
    public float speed = 2.0f;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rotation = Quaternion.LookRotation(player.transform.position - transform.position);

        // go forward player
        float step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(transform.position, player.position - Vector3.forward, step);
    }
}
