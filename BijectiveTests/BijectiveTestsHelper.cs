using Bijective;
using System;
using System.Collections.Generic;

namespace BijectiveTests
{
    public static class BijectiveTestsHelper
    {
        public static Bijective<string, int> GetBijective1()
        {
            var bijective = new Bijective<string, int>();
            bijective["A"] = 10;
            bijective["B"] = 20;
            bijective["C"] = 30;

            return bijective;
        }

        public static List<Tuple<string, int>> GetAllBijections1()
        {
            return new List<Tuple<string, int>>
            {
                new Tuple<string, int>("A", 10),
                new Tuple<string, int>("B", 20),
                new Tuple<string, int>("C", 30)
            };
        }

        public static List<Tuple<int, string>> GetAllReverseBijections1()
        {
            return new List<Tuple<int, string>>
            {
                new Tuple<int, string>(10, "A"),
                new Tuple<int, string>(20, "B"),
                new Tuple<int, string>(30, "C")
            };
        }

        public static List<string> GetAllLeftElements1()
        {
            return new List<string> { "A", "B", "C" };
        }

        public static List<int> GetAllRightElements1()
        {
            return new List<int> { 10, 20, 30 };
        }
    }
}
