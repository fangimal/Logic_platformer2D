using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer
{
    public class StartButton : MonoBehaviour
    {
        [SerializeField] private Image circleFillImage;
        [SerializeField] private Image neonImage;
        [SerializeField] private Transform icon;

        public void Hide()
        {
            gameObject.SetActive(false);
            circleFillImage.fillAmount = 0f;
            Color tmp = neonImage.color;
            tmp.a = 0f;
            neonImage.color = tmp;
            icon.gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            StartCoroutine(StartAnimation());
        }

        private IEnumerator StartAnimation()
        {
            WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
            
            float ctime = 0f;
            float time = 1f;

            while (ctime <= time)
            {
                float t = ctime / time;
                circleFillImage.fillAmount = t;

                ctime += Time.deltaTime;
                yield return waitForEndOfFrame;
            }

            circleFillImage.fillAmount = 1f;

            Color tmp = neonImage.color;
            tmp.a = 0f;
            ctime = 0f;
            time -= 0.5f;

            while (ctime <= time)
            {
                float t = ctime / time;

                tmp.a = t;
                neonImage.color = tmp;

                ctime += Time.deltaTime;
                yield return waitForEndOfFrame;
            }

            tmp.a = 1;
            neonImage.color = tmp;
            icon.gameObject.SetActive(true);
        }
    }
}
