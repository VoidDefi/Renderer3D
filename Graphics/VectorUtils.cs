using System;
using Microsoft.Xna.Framework;

namespace Renderer3D.Graphics
{
    public static class VectorUtils
    {
        public static float Angle(this Vector2 origin, Vector2 target)
        {
            return MathF.Atan2(target.Y - origin.Y, target.X - origin.X);
        }

        public static float Angle(this Vector3 origin, Vector3 target)
        {
            float dot = Vector3.Dot(origin, target);
            float lengths = origin.Length() * target.Length();

            return MathF.Acos(dot * lengths);
        }
    }
}
