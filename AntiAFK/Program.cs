using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using SharpDX;
using System;

namespace AntiAFK
{
    public sealed class Program
    {
        static Vector3 currentPosition;
        static Vector3 lastPosition;
        static int currentTick;
        static int lastTick;

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += gameLoadEventArgs =>
            {
                Hacks.AntiAFK = false;
                lastTick = 0;
                Game.OnUpdate += OnUpdate;
            };
        }

        private static void OnUpdate(EventArgs args)
        {
            if (ObjectManager.Player.IsMoving)
            {
                return;
            }
            if (!ObjectManager.Player.IsDead)
            {
                currentTick = Environment.TickCount;
                if (lastTick == 0)
                    lastTick = Environment.TickCount;
                currentPosition = Player.Instance.ServerPosition;
                if (lastPosition.IsZero)
                    lastPosition = Player.Instance.ServerPosition;
                if (currentPosition.Distance(lastPosition) > 200)
                {
                    lastTick = currentTick;
                    lastPosition = currentPosition;
                }
                if ((currentTick - lastTick) > 70000)
                    Game.QuitGame();
            }
        }
    }
}
