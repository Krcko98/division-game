using UnityEngine;

namespace Grid.Static.Helper
{
    public static class GameplayHelper
    {
        public static int minNumber = 2;
        public static int maxNumber = 24;

        public static int FetchRandomNumber()
        {
            return Random.Range(minNumber, maxNumber);
        }
    }
}