using System;
using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

namespace APAtelier.DS.Avatar
{
    public abstract class Actor : MonoBehaviour
    {
        [SerializeField] 
        protected AnimancerComponent _animancerComponent;
    }
}