using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APAtelier.DS.Avatar
{
    public class FireBall : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        private void Update()
        {
            transform.position += Time.deltaTime * speed * transform.forward;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() == null)
            {
                Destroy(gameObject);
            }
        }
    }
}
