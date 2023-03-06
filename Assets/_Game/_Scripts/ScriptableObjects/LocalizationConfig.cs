using System;
using UnityEngine;
using UnityEngine.Localization;

namespace LogicPlatformer
{
    [CreateAssetMenu(fileName = "LocalConfig", menuName = "Config/LocalConfig", order = 4)]
    public class LocalizationConfig : ScriptableObject
    {
        [Tooltip("ѕор€док €зыков в списке должен быть таким же, как в Localization Settings // " +
            "The order of languages in the list should be the same as in the Localization Settings")]

        [SerializeField] private LocalizedStringTable hintsTable;

        [SerializeField] private LocalData[] localDatas;

        public LocalizedStringTable GetHintsTable => hintsTable;
        public LocalData[] GetLocalDatas => localDatas;

    }

    [Serializable]
    public struct LocalData
    {
        public string name;
        public Sprite langImage;
    }
}
