using UnityEngine;

namespace Assets.Scripts
{
    class NpcHaterBehaviour : INpcBehaviour
    {
        private float speed = 2.0f;
        public Transform npc;
        private Vector3 go = new Vector3(   UnityEngine.Random.Range(-100f, 100f), 
                                            0, 
                                            UnityEngine.Random.Range(-100f, 0f));

        public NpcHaterBehaviour(Transform npc)
        {
            this.npc = npc;
        }

        public void Enter() 
        {
            Debug.Log("Enter HATE");
        }
        public void Update() 
        {
            var rotation = Quaternion.LookRotation(go - npc.position);
            npc.rotation = rotation;

            // go forward player
            float step = speed * Time.deltaTime;
            npc.position = Vector3.MoveTowards(npc.position, go, step);
            Debug.Log(go);
        }
        public void Exite() 
        {
            Debug.Log("Exite HATE");
        }
    }
}
