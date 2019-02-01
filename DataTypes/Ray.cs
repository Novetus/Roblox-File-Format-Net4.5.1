﻿namespace RobloxFiles.DataTypes
{
    public class Ray
    {
        public readonly Vector3 Origin;
        public readonly Vector3 Direction;

        public Ray Unit
        {
            get
            {
                Ray unit;

                if (Direction.Magnitude == 1.0f)
                    unit = this;
                else
                    unit = new Ray(Origin, Direction.Unit);

                return unit;
            }
        }

        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public override string ToString()
        {
            return '{' + Origin.ToString() + "}, {" + Direction.ToString() + '}';
        }

        public Vector3 ClosestPoint(Vector3 point)
        {
            Vector3 result = Origin;
            float t = Direction.Dot(point - result);

            if (t >= 0)
                result += (Direction * t);

            return result;
        }

        public float Distance(Vector3 point)
        {
            Vector3 closestPoint = ClosestPoint(point);
            return (closestPoint - point).Magnitude;
        }
    }
}
