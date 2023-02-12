using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer.UI
{
    public class StartUI : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button startBtn;
        [SerializeField] private Button settingsBtn;
        [SerializeField] private Button lvlRoomBtn;
        [SerializeField] private Button socialBtn;
        [SerializeField] private Button likeBtn;
        [SerializeField] private Button skinsBtn;
        [SerializeField] private Button noADSBtn;
        [SerializeField] private StartButton[] startButtons;

        public event Action OnStartGame;
        public event Action OnLevelRoom;
        public event Action OnSettings;
        public event Action OnLikeCliked;

        private void Awake()
        {
            startBtn.onClick.AddListener(() =>
            {
                OnStartGame?.Invoke();
            });

            lvlRoomBtn.onClick.AddListener(() =>
            {
                OnLevelRoom?.Invoke();
            });

            settingsBtn.onClick.AddListener(() =>
            {
                OnSettings?.Invoke();
            });

            likeBtn.onClick.AddListener(()=>
            { 
                likeBtn.GetComponent<StartButton>().Hide();
                OnLikeCliked?.Invoke();
            });
        }

        public void Init()
        {
            Open();
        }

        public void Open()
        {
            for (int i = 0; i < startButtons.Length; i++)
            {
                startButtons[i].Hide();
            }

            gameObject.SetActive(true);
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }

        private void ShowButtons() // Start Animation
        {
            StartCoroutine(ShowStartButtons());
        }

        private IEnumerator ShowStartButtons()
        {
            WaitForSeconds wait = new WaitForSeconds(0.2f);

            for (int i = 0; i < startButtons.Length; i++)
            {
                startButtons[i].Show();
                yield return wait;
            }
        }
    }
}
