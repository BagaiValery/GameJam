using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class NpcCharmedCheck : MonoBehaviour
    {
        public GameObject playerTarget { get; set; }
        private NPC npc;

        private void Awake()
        {
            playerTarget = GameObject.FindGameObjectWithTag("Star");
            npc = GetComponent<NPC>();
        }
    }
}
