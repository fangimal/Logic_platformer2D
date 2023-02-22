using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace LogicPlatformer.Hint
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
