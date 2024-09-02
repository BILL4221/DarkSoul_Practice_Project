using Animancer;
using UnityEngine;

namespace APAtelier.DS.Avatar
{
    [CreateAssetMenu(fileName = "AttackConfig", menuName = "Config/Attack/AttackConfig", order = 1)]
    public class AttackStateParameter : ScriptableObject
    {
        [SerializeField]
        private float statminaCost;
        [SerializeField]
        private ClipTransitionSequence _transition;
        private Player player;

        public ClipTransitionSequence Transition => _transition;
        public float StatminaCost => statminaCost;
        
        public void CastSpell(GameObject spellObject)
        {
            var spell = Instantiate(spellObject);
            spell.transform.position = player.transform.position + Vector3.up;
            spell.transform.forward = player.transform.forward;
        }

        public void SetPlayer(Player player)
        {
            this.player = player;
        }
    }
}