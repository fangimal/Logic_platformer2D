using System;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer.UI
{
    public class StartUI : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button startBtn;
        [SerializeField] private Button optionsBtn;
        [SerializeField] private Button lvlRoomBtn;
        [SerializeField] private Button socialBtn;
        [SerializeField] private Button likeBtn;
        [SerializeField] private Button skinsBtn;
        [SerializeField] private Button noADSBtn;

        public event Action OnStartGame;
        public event Action OnLevelRoom;

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
        }

        public void Init()
        {
            Open();
            Debug.Log("Init: " + name);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }

    }
}
