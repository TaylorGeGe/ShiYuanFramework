using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sy
{
    public static class MathUtil
    {
        /// <summary>
        /// 输入百分比返回是否命中概率
        /// </summary>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static bool Percent(int percent)
        {
            return Random.Range(0, 100) <= percent;
        }

        /**加*/
        //rate:几率数组（%），  total：几率总和（100%）
        // Debug.Log(rand(new int[] { 10, 5, 15, 20, 30, 5, 5,10 }, 100));
        public static int rand(int[] rate, int total)
        {
            int r = Random.Range(1, total + 1);
            int t = 0;
            for (int i = 0; i < rate.Length; i++)
            {
                t += rate[i];
                if (r < t)
                {
                    return i;
                }
            }
            return 0;
        }
        /**减*/
        //rate:几率数组（%），  total：几率总和（100%）
        // Debug.Log(randRate(new int[] { 10, 5, 15, 20, 30, 5, 5,10 }, 100));
        public static int randRate(int[] rate, int total)
        {
            int rand = Random.Range(0, total + 1);
            for (int i = 0; i < rate.Length; i++)
            {
                rand -= rate[i];
                if (rand <= 0)
                {
                    return i;
                }
            }
            return 0;
        }
        public static int GetRandomValueFrom(int[] values)
        {
            return values[Random.Range(0, values.Length)];
        }

        public static string GetRandomValueFrom(string[] values)
        {
            return values[Random.Range(0, values.Length)];
        }

        public static float GetRandomValueFrom(float[] values)
        {
            return values[Random.Range(0, values.Length)];
        }
        public static object GetRandomValueFrom(object[] values)
        {
            return values[Random.Range(0, values.Length)];
        }
        public static T GetRandomValueFrom<T>(T[] values)
        {
            return values[Random.Range(0, values.Length)];
        }

        // public static T GetRandomValueFrom<T>( params T[] values)
        // {
        //     return values[Random.Range(0, values.Length)];
        // }
    }
}