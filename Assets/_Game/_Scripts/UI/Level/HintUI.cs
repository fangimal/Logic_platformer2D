using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LogicPlatformer
{
    public class HintUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hintText;

        public void SetHint(string hint)
        {
            hintText.text = hint;
        }
    }
}
