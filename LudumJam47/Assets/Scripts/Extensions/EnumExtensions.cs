using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

namespace EnumExtensions
{
    public static class EnumExtensions
    {
        public static void BetterThanSwitch<T>(object enumInstanceToFind, Action[] actions)
        {
            var enumAsDictionary = ConvertEnumToDictionary<T>();
            Type enumType = typeof(T);
            string instanceName = GetEnumInstanceName(enumType, enumInstanceToFind);

            int actionNumber = enumAsDictionary[instanceName];
            actions[actionNumber].Invoke();
        }

        private static string GetEnumInstanceName(Type enumType, object enumInstance) => Enum.GetName(enumType, enumInstance);

        private static IDictionary<String, Int32> ConvertEnumToDictionary<K>()
        {
            if (typeof(K).BaseType != typeof(Enum))
            {
                throw new InvalidCastException();
            }

            return Enum.GetValues(typeof(K)).Cast<Int32>().ToDictionary(currentItem => Enum.GetName(typeof(K), currentItem));
        }
        
    }
}
