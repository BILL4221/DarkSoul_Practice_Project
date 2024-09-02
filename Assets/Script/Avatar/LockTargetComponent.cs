using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APAtelier.DS.Avatar
{
    public class LockTargetComponent
    {
        private Player player;
        private Transform target;

        public LockTargetComponent(Player player)
        {
            this.player = player;
        }

        private void Update()
        {
            if (target == null)
            {
                return;
            }
            
            //TODO: Implement lock camera position logic
        }

        public void LockTarget(Transform target)
        {
            this.target = target;
        }
    }
}
