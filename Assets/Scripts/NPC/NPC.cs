using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class NPC : MonoBehaviour
    {
        public delegate void CharmPerson(GameObject obj);
        public static event CharmPerson OnCharmPerson;

        private Dictionary<Type, INpcBehaviour> behaviourMap;
        private INpcBehaviour currentBehaviour;

        private void Start()
        {
            this.InitBehaviours();
            this.SetDefoultBehaviour();
        }

        private void InitBehaviours()
        {
            this.behaviourMap = new Dictionary<Type, INpcBehaviour>();

            this.behaviourMap[typeof(NpcFanBehaviour)] = new NpcFanBehaviour(this.transform);
            this.behaviourMap[typeof(NpcHaterBehaviour)] = new NpcHaterBehaviour(this.transform);
            this.behaviourMap[typeof(NpcIdleBehaviour)] = new NpcIdleBehaviour();
        }

        private void SetBehaviour(INpcBehaviour newBehaviour)
        {
            if (this.currentBehaviour != null)
                this.currentBehaviour.Exite();

            this.currentBehaviour = newBehaviour;
            this.currentBehaviour.Enter();
        }

        private void SetDefoultBehaviour()
        {
            this.SetIdleBehaviour();
        }

        private INpcBehaviour GetBehaviourFromMap<T>() where T : INpcBehaviour
        {
            var type = typeof(T);
                return this.behaviourMap[type];
        }

        private void Update()
        {
            if (this.currentBehaviour != null)
                this.currentBehaviour.Update();
        }

        public void SetIdleBehaviour()
        {
            var behaiveour = GetBehaviourFromMap<NpcIdleBehaviour>();
            this.SetBehaviour(behaiveour);
        }

        public void SetFanBehaviour()
        {
            var behaiveour = GetBehaviourFromMap<NpcFanBehaviour>();
            this.SetBehaviour(behaiveour);

            if (OnCharmPerson != null) OnCharmPerson(this.gameObject);
        }

        public void SetHaterBehaviour()
        {
            var behaiveour = GetBehaviourFromMap<NpcHaterBehaviour>();
            this.SetBehaviour(behaiveour);
        }
    }
}
