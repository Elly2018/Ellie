using OpenTK;
using OpenTK.Input;
using System;

namespace GameLibrary
{
    public sealed class EInput
    {
        public static Vector2 MousePosition;
        public static Vector2 MousePositionDelta;

        public static bool[] KeyDown;
        public static bool[] KeyUp;
        public static bool[] Key;

        public static void Initialize()
        {
            int l = Enum.GetNames(typeof(Key)).Length;

            KeyDown = new bool[l];
            KeyUp = new bool[l];
            Key = new bool[l];
            for(int i = 0; i < l; i++)
            {
                KeyDown[i] = false;
                KeyUp[i] = false;
                Key[i] = false;
            }
        }

        public static void Update()
        {

        }

        public static bool IsKeyDown(Key e)
        {
            return false;
        }

        public static bool IsKeyUp(Key e)
        {
            return false;
        }

        public static bool IsKey(Key e)
        {
            return Key[(int)e];
        }

        public static bool MouseDown(int e)
        {
            return false;
        }

        public static bool MouseUp(int e)
        {
            return false;
        }
    }
}
