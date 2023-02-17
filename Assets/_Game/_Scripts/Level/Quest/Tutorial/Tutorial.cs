using LogicPlatformer.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer
{
    public class Tutorial : MonoBehaviour
    {
        private LevelUI levelUI;

        private bool secondGeneration = false;

        private Tutorial clone;

        public bool GetGeneration => secondGeneration;

        private void Start()
        {
            levelUI = FindObjectOfType<LevelUI>();

            transform.parent = levelUI.transform;

            DeleteClone();

            levelUI.OnRestartClicked += RestartEvent;

        }

        private void RestartEvent(int level)
        {
            secondGeneration = true;

            DeleteClone();
        }

        private void OnDisable()
        {
            levelUI.OnRestartClicked -= RestartEvent;
        }

        public void DeleteClone()
        {
            clone = FindObjectOfType<Tutorial>();

            if (clone != this && !clone.GetGeneration)
            {
                Destroy(clone.gameObject);
            }
        }
    }
}
