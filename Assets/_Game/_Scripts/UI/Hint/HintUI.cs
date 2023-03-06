using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace LogicPlatformer.Hint
{
    public class HintUI : MonoBehaviour
    {
        [SerializeField] private LocalizeStringEvent local;

        public void SetHint(LocalizedStringTable table, string key)
        {
            local.SetTable(table.TableReference);
            local.SetEntry(key);
            // Debug.Log("Hint key: " + key);
        }
    }
}
