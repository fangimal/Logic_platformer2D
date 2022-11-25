using UnityEngine;

namespace LogicPlatformer
{
    public interface IControlable
    {
        void Move(Vector2 direction);
        void Jump();
    }
}
