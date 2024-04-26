using Unity.Netcode;
using UnityEngine;

namespace ScrapPack
{
    public class ScrapPack : MonoBehaviour
    {
        public bool grabbable;

        [Space(3f)]
        public Transform parentObject;

        [Space(5f)]
        public int scrapValue;

        [Space(10f)]
        public float useCooldown;

        [Space(10f)]
        public string customGrabTooltip;

        public MeshRenderer mainObjectRenderer;

        public bool scrapPersistedThroughRounds;

        [Space(3f)]
        public bool grabbableToEnemies;
        //public GameObject fireEffect;

        //public AudioClip startJetpackSFX;

        //public AudioClip jetpackWarningBeepSFX;

        //public ParticleSystem smokeTrailParticle;
    }
}
