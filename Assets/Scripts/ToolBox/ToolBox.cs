using System.Collections;
using System.Collections.Generic;
using Rnd = System.Random;
using UnityEngine;


public struct Address
{
    public Address(int p, int q)
    {
        this.addrP = p;
        this.addrQ = q;
        this.addrR = 0 - p - q;
    }
    public int addrP { get; set; }
    public int addrQ { get; set; }
    public int addrR { get; set; }
    public override string ToString() => $"(P:{addrP}, Q:{addrQ}, R:{addrR})";

    public bool Equals(Address anotherAddress)
    {
        return (this.addrP == anotherAddress.addrP) && (this.addrQ == anotherAddress.addrQ);
    }
}

public class ToolBox
{
    int seed;

    public void Init(int initSeed = 0)
    {
        this.seed = initSeed;
        Random.InitState(this.seed);
    }

    public int RandomInt(int lower, int higher)
    {
        return Random.Range(lower, higher);
    }

}

    public static class Weighted
    {
        /// <summary>
        /// A class that returns a random index in an IEnumerable, with higher values in the IEnumerable having higher probability of their index being selected
        /// </summary>
        /// <param name="weights">A Collection (IEnumerable) of weights</param>
        /// <returns></returns>
        public static int Random(IEnumerable<int> weights)
        {
            int result = 0;

            // sum weights
            int maxValue = 0;
            foreach (var weight in weights)
            {
                maxValue += weight;
            }

            // roll a random
            Rnd rnd = new Rnd();
            int rndValue = rnd.Next(0, maxValue);

            // start accumulated comparison
            int accumulated = 0;
            int counter = 1;
            foreach (var weight in weights)
            {
                accumulated += weight;
                if (accumulated >= rndValue)
                {
                    result = counter;
                    break;
                }
                counter++;
            }
            return result;
        }

        public static int Random(IEnumerable<int> weights, int seed)
        {
            int result = 0;

            // sum weights
            int maxValue = 0;
            foreach (var weight in weights)
            {
                maxValue += weight;
            }

            // roll a random
            Rnd rnd = new Rnd(seed);
            int rndValue = rnd.Next(0, maxValue);

            // start accumulated comparison
            int accumulated = 0;
            int counter = 1;
            foreach (var weight in weights)
            {
                accumulated += weight;
                if (accumulated >= rndValue)
                {
                    result = counter;
                    break;
                }
                counter++;
            }
            return result;
        }

        public static int RandomNormal(IEnumerable<double> weights)
        {
            int result = 0;

            // sum weights
            double maxValue = 0;
            foreach (var weight in weights)
            {
                maxValue += weight;
            }
            double factor = 1 / maxValue;

            // normalize weights
            List<double> normWeights = new List<double>();
            foreach (var weight in weights)
            {
                normWeights.Add(weight * factor);
            }

            // roll a random
            Rnd rnd = new Rnd();
            double rndValue = rnd.NextDouble();

            // start accumulated comparison
            double accumulated = 0;
            int counter = 1;
            foreach (var weight in weights)
            {
                accumulated += weight;
                if (accumulated >= rndValue)
                {
                    result = counter;
                    break;
                }
                counter++;
            }
            return result;
        }

        public static int RandomNormal(IEnumerable<double> weights, int seed)
        {
            int result = 0;

            // sum weights
            double maxValue = 0;
            foreach (var weight in weights)
            {
                maxValue += weight;
            }
            double factor = 1 / maxValue;

            // normalize weights
            List<double> normWeights = new List<double>();
            foreach (var weight in weights)
            {
                normWeights.Add(weight * factor);
            }

            // roll a random
            Rnd rnd = new Rnd(seed);
            double rndValue = rnd.NextDouble();

            // start accumulated comparison
            double accumulated = 0;
            int counter = 1;
            foreach (var weight in weights)
            {
                accumulated += weight;
                if (accumulated >= rndValue)
                {
                    result = counter;
                    break;
                }
                counter++;
            }
            return result;
        }

        public static double Balance(IEnumerable<int> collection, bool zeroesAllowed)
        {
            // validate non-negative or positive members
            // calculate sum, count, max, min, span and average
            int sum = 0;
            int count = 0;
            int max = 0;
            float average = 0f;
            foreach (int member in collection)
            {
                sum += member;
                if ((zeroesAllowed && member == 0) || member != 0)
                {
                    count++;
                }
                max = UnityEngine.Mathf.Max(max, member);
            }
            average = (sum / count);

            // calculate deviations
            double totalDeviation = 0;
            foreach (int member in collection)
            {
                if ((zeroesAllowed && member == 0) || member != 0)
                {
                    double deviation = UnityEngine.Mathf.Abs(member - average);
                    totalDeviation += deviation;
                }
            }
            double averageDeviation = totalDeviation / count;
            double percentileDeviation = averageDeviation / max;
            double balance = 1 - percentileDeviation;

            return balance;
        }

        public static double Balance(IEnumerable<double> collection, bool zeroesAllowed)
        {
            // validate non-negative or positive members
            // calculate sum, count, max, min, span and average
            float sum = 0f;
            float count = 0f;
            float max = 0f;
            float average = 0f;
            foreach (float member in collection)
            {
                sum += member;
                if ((zeroesAllowed && member == 0) || member != 0)
                {
                    count++;
                }
                max = UnityEngine.Mathf.Max((float)max, (float)member);
            }
            average = sum / count;

            // calculate deviations
            double totalDeviation = 0;
            foreach (float member in collection)
            {
                if ((zeroesAllowed && member == 0) || member != 0)
                {
                    double deviation = UnityEngine.Mathf.Abs(member - average);
                    totalDeviation += deviation;
                }
            }
            double averageDeviation = totalDeviation / count;
            double percentileDeviation = averageDeviation / max;
            double balance = 1 - percentileDeviation;

            return balance;
        }
    }
