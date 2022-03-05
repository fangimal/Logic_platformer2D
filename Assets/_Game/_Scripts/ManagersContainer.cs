using LogicPlatformer.UI;
using LogicPlatformer.Level;
using UnityEngine;

namespace LogicPlatformer
{
    public class ManagersContainer : MonoBehaviour
    {
        [SerializeField] private IMainUI mainUI;

        [SerializeField] private ILevelManager LevelManager;

        //[SerializeField] private ILevelController levelController;

        public IMainUI GetMainUI => mainUI;

        public ILevelManager GetLevelManager => LevelManager;

        //public ILevelController GetLevelController => levelController;
    }
}
