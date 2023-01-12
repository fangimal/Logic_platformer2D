using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer
{
    public abstract class IActivate: MonoBehaviour
    {
        public abstract void Activate();

        public abstract void Deactivate();
    }
}
