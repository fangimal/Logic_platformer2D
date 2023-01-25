using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace LogicPlatformer
{
    public class Yandex : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI usernameText;
        [SerializeField] private RawImage userIcon;

        [DllImport("__Internal")]
        private static extern void Hello();

        [DllImport("__Internal")]
        private static extern void GiveMePlayerData();

        [DllImport("__Internal")]
        private static extern void RateGame();

        public void HelloButton()
        {
            GiveMePlayerData();
        }

        public void SetName(string name)
        {
            usernameText.text = name;
        }

        public void SetPhoto(string url)
        {
            StartCoroutine(DomwLoadImage(url));
        }

        public void RateGameButton()
        {
            RateGame();
        }
        private IEnumerator DomwLoadImage(string mediaUrl)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaUrl);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
            }
            else
            {
                userIcon.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            }
        }
    }
}
