using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class NpcIdleBehaviour : INpcBehaviour
    {
        public void Enter() 
        {
            Debug.Log("Enter IDLE");
        }
        public void Update() 
        {
            Debug.Log("Update IDLE");
        }
        public void Exite() 
        {
            Debug.Log("Exite IDLE");
        }
    }
}
