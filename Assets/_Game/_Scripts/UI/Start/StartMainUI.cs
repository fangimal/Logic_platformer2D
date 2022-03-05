using System;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer.UI
{
    public class StartMainUI : IStartMainUI
    {
        [Header("Buttons")]
        [SerializeField] private Button startBtn;
        [SerializeField] private Button optionsBtn;
        [SerializeField] private Button lvlBtn;
        [SerializeField] private Button socialBtn;
        [SerializeField] private Button likeBtn;
        [SerializeField] private Button skinsBtn;
        [SerializeField] private Button noADSBtn;

        public override event Action OnStartButton;

        private void Awake()
        {
            startBtn.onClick.AddListener(() =>
            {
                OnStartButton?.Invoke();
            });
        }

        public override void Init()
        {
            Debug.Log("Init: " + name);
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
