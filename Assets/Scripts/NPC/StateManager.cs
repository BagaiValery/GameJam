using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class StateManager : MonoBehaviour
    {

        private void OnEnable()
        {
            ControlManager.OnTapOverFan += CharemedFan;
        }

        private void OnDisable()
        {
            ControlManager.OnTapOverFan -= CharemedFan;
        }

        void CharemedFan(GameObject gameObject)
        {
            NPC fan = gameObject.GetComponent<NPC>();
            if (fan != null)
            {
                fan.SetFanBehaviour();
            }
        }
    }
}
