using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.SocialPlatforms;

namespace LogicPlatformer
{
    public class HintUI : MonoBehaviour
    {
        [SerializeField] private LocalizeStringEvent local;

        public void SetHint(StringTableCollection table, string key)
        {
            local.SetTable(table.name);
            local.SetEntry(key);
           // Debug.Log("Hint key: " + key);
        }
    }
}
