using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
        public void Update() 
        {
            var rotation = Quaternion.LookRotation(player.transform.position - npc.position);

            // go forward player
            float step = speed * Time.deltaTime;
            npc.position = Vector3.MoveTowards(npc.position, player.position - Vector3.forward, step);
        }
        public void Exite() 
        {
            
        }
    }
}
