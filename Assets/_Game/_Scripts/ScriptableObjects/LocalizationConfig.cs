using System;
using UnityEngine;

namespace LogicPlatformer
{
    [CreateAssetMenu(fileName = "LocalConfig", menuName = "Config/LocalConfig", order = 4)]
    public class LocalizationConfig : ScriptableObject
    {
        [Tooltip("������� ������ � ������ ������ ���� ����� ��, ��� � Localization Settings// " +
            "The order of languages in the list should be the same as in the Localization Settings")]
        [SerializeField] private LocalData[] localDatas;

        public LocalData[] GetLocalDatas => localDatas;
    }

    [Serializable]
    public struct LocalData
    {
        public string name;
        public Sprite langImage;
    }
}
