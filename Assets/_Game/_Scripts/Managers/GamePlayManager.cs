using LogicPlatformer.GamePlay;
using LogicPlatformer.Level;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer
{
    public class GamePlayManager : IGamePlayManager
    {
        public override event Action<LevelManager> OnLevelPassed;
    }
}
