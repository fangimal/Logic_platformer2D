using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.SocialPlatforms;

public class HintUI : MonoBehaviour
{
    [SerializeField] private LocalizeStringEvent local;

    public void SetHint(string key)
    {
        local.SetEntry(key);
        local.RefreshString();
    }
}