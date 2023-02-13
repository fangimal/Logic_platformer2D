using System;
using UnityEditor.Localization;
using UnityEngine;

namespace LogicPlatformer
{
    [CreateAssetMenu(fileName = "LocalConfig", menuName = "Config/LocalConfig", order = 4)]
    public class LocalizationConfig : ScriptableObject
    {
        [Tooltip("ѕор€док €зыков в списке должен быть таким же, как в Localization Settings // " +
            "The order of languages in the list should be the same as in the Localization Settings")]
        
        [SerializeField] private StringTableCollection table;

        [SerializeField] private LocalData[] localDatas;

        public StringTableCollection GetHintsTable => table;
        public LocalData[] GetLocalDatas => localDatas;

    }

    [Serializable]
    public struct LocalData
    {
        public string name;
        public Sprite langImage;
    }
}
