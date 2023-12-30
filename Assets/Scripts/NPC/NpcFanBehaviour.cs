using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    class NpcFanBehaviour : INpcBehaviour
    {
        private float speed = 2.0f;
        private Transform player;
        private Transform npc;

        public NpcFanBehaviour(Transform npc)
        {
            this.npc = npc;
        }

        public void Enter()
        {
            player = GameObject.FindGameObjectWithTag("Star").GetComponent<Transform>();
            speed = player.GetComponent<PlayerControl>().CurrentSpeed - 0.5f;

            player.GetComponent<PlayerControl>().StartCoroutine(speedLimit());
        }

        IEnumerator speedLimit()
        {
            float seconds = 3;

            while (speed < player.GetComponent<PlayerControl>().CurrentSpeed)
            {
                speed += (Time.deltaTime * player.GetComponent<PlayerControl>().CurrentSpeed) / seconds;

                yield return null;
            }
        }

        public void Update()
        {
            var rotation = Quaternion.LookRotation(player.transform.position - npc.position);
            npc.rotation = rotation;

            // go forward player
            float step = speed * Time.deltaTime;
            npc.position = Vector3.MoveTowards(npc.position, player.position - Vector3.forward, step);
        }
        public void Exite()
        {

        }
    }
}
