using System;

namespace DragPositionDemonstrateProject
{
    public interface ICloneable<T> : ICloneable
    {
        new T Clone();
    }
}
