using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExtensionMethods
{
    /// <summary>
    /// A class used to store extension methods to several Unity classes.
    /// </summary>
    public static class MyExtensions
    {
        static System.Random randomGenerator = new System.Random();

        public static T GetRandom<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
                return default;

            var randomIndex = randomGenerator.Next(list.Count);
            return list[randomIndex];
        }
    }
}
