using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class NpcHaterBehaviour : INpcBehaviour
    {
        public void Enter() 
        {
            Debug.Log("Enter HATE");
        }
        public void Update() 
        {
            Debug.Log("Update HATE");
        }
        public void Exite() 
        {
            Debug.Log("Exite HATE");
        }
    }
}
