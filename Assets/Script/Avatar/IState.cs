using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APAtelier.DS.Avatar
{
    public interface IStateble
    {
        public void OnStateStart();

        public void OnStateUpdate();

        public void OnStateEnd();
    }
}
