using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace LogicPlatformer
{
    public class TapActivation : MonoBehaviour
    {
        [SerializeField] private IActivate targetActivate;
        [SerializeField] private ParticleSystem particle;

        private int tapCount = 0;
        private bool isActivate = false;

        private void OnMouseDown()
        {
            if (!isActivate)
            {
                tapCount++;
                StartCoroutine(Timer());
            }
            if (tapCount > 1 && !isActivate)
            {
                targetActivate.Activate();
                isActivate = true;
            }
        }

        private IEnumerator Timer()
        {
            particle?.Play();
            yield return new WaitForSeconds(1);
            tapCount--;
        }

    }
}
