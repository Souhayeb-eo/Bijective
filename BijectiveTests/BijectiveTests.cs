using BijectiveTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bijective.Tests
{
    [TestClass()]
    public class BijectiveTests
    {
        [TestMethod()]
        public void CanAddBijectionTest()
        {
            var bijective = new Bijective<string, int>();
            bijective["A"] = 10;

            Assert.IsFalse(bijective.CanAddBijection("A", 10));
            Assert.IsFalse(bijective.CanAddBijection("B", 10));
            Assert.IsFalse(bijective.CanAddBijection("A", 20));
            Assert.IsTrue(bijective.CanAddBijection("B", 20));
        }

        [TestMethod()]
        public void CanAddReverseBijectionTest()
        {
            var bijective = new Bijective<string, int>();
            bijective[10] = "A";

            Assert.IsFalse(bijective.CanAddReverseBijection(10, "A"));
            Assert.IsFalse(bijective.CanAddReverseBijection(10, "B"));
            Assert.IsFalse(bijective.CanAddReverseBijection(20, "A"));
            Assert.IsTrue(bijective.CanAddReverseBijection(20, "B"));
        }

        [TestMethod()]
        public void GetAllBijectionsTest_GivenANonBlankBijective()
        {
            var bijective = BijectiveTestsHelper.GetBijective1();

            var allBijections = bijective.GetAllBijections();
            var allBijectionsList = new List<Tuple<string, int>>(allBijections);

            var expectedAllBijectionsList = BijectiveTestsHelper.GetAllBijections1();

            CollectionAssert.AreEquivalent(expectedAllBijectionsList, allBijectionsList);
        }

        [TestMethod()]
        public void GetAllBijectionsTest_GivenBlankBijective()
        {
            var bijective = new Bijective<object, object>();
            var allBijections = bijective.GetAllBijections();

            Assert.AreEqual(0, new List<Tuple<object, object>>(allBijections).Count);
        }

        [TestMethod()]
        public void GetAllReverseBijectionsTest_GivenANonBlankBijective()
        {
            var bijective = BijectiveTestsHelper.GetBijective1();

            var allReverseBijections = bijective.GetAllReverseBijections();
            var allReverseBijectionsList = new List<Tuple<int, string>>(allReverseBijections);

            var expectedAllReverseBijectionsList = BijectiveTestsHelper.GetAllReverseBijections1();

            CollectionAssert.AreEquivalent(expectedAllReverseBijectionsList, allReverseBijectionsList);
        }

        [TestMethod()]
        public void GetAllReverseBijectionsTest_GivenBlankBijective()
        {
            var bijective = new Bijective<object, object>();
            var allReverseBijections = bijective.GetAllReverseBijections();

            Assert.AreEqual(0, new List<Tuple<object, object>>(allReverseBijections).Count);
        }

        [TestMethod()]
        public void GetAllLeftElementsTest_GivenANonBlankBijective()
        {
            var bijective = BijectiveTestsHelper.GetBijective1();
            var allLeftElements = bijective.GetAllLeftElements();
            var allLeftElementsList = new List<string>(allLeftElements);
            var expectedAllLeftElementsList = BijectiveTestsHelper.GetAllLeftElements1();
            CollectionAssert.AreEquivalent(expectedAllLeftElementsList, allLeftElementsList);
        }

        [TestMethod()]
        public void GetAllLeftElementsTest_GivenABlankBijective()
        {
            var bijective = new Bijective<object, object>();
            var allLeftElements = bijective.GetAllLeftElements();

            Assert.AreEqual(0, new List<object>(allLeftElements).Count);
        }

        [TestMethod()]
        public void GetAllRightElementsTest_GivenANonBlankBijective()
        {
            var bijective = BijectiveTestsHelper.GetBijective1();
            var allRightElements = bijective.GetAllRightElements();
            var allRightElementsList = new List<int>(allRightElements);
            var expectedAllRightElementsList = BijectiveTestsHelper.GetAllRightElements1();
            CollectionAssert.AreEquivalent(expectedAllRightElementsList, allRightElementsList);
        }

        [TestMethod()]
        public void GetAllRightElementsTest_GivenABlankBijective()
        {
            var bijective = new Bijective<object, object>();
            var allRightElements = bijective.GetAllRightElements();

            Assert.AreEqual(0, new List<object>(allRightElements).Count);
        }

        [TestMethod()]
        public void TryAddBijectionTest()
        {
            var bijective = new Bijective<string, int>();
            bijective["A"] = 10;

            Assert.IsFalse(bijective.TryAddBijection("A", 10));
            Assert.IsFalse(bijective.TryAddBijection("B", 10));
            Assert.IsFalse(bijective.TryAddBijection("A", 20));
            Assert.IsTrue(bijective.TryAddBijection("B", 20));

            Assert.IsTrue(bijective.LeftElementExists("B"));
            Assert.IsTrue(bijective.RightElementExists(20));

            var allLeftElementsList = new List<string>(bijective.GetAllLeftElements());
            Assert.AreEqual(2, allLeftElementsList.Count);
            Assert.IsTrue(allLeftElementsList.Contains("B"));

            var allRightElementsList = new List<int>(bijective.GetAllRightElements());
            Assert.AreEqual(2, allRightElementsList.Count);
            Assert.IsTrue(allRightElementsList.Contains(20));

            Assert.IsFalse(bijective.CanAddBijection("B", 20));
            Assert.IsFalse(bijective.CanAddReverseBijection(20, "B"));

            Tuple<string, int> bijection;
            Assert.IsTrue(bijective.TryGetBijection("B", out bijection));
            Assert.AreEqual("B", bijection.Item1);
            Assert.AreEqual(20, bijection.Item2);

            Tuple<int, string> reverseBijection;
            Assert.IsTrue(bijective.TryGetReverseBijection(20, out reverseBijection));
            Assert.AreEqual(20, reverseBijection.Item1);
            Assert.AreEqual("B", reverseBijection.Item2);

            var allBijectionsList = new List<Tuple<string, int>>(bijective.GetAllBijections());
            Assert.AreEqual(2, allBijectionsList.Count);
            Assert.IsTrue(allBijectionsList.Select(b => b.Item1).Contains("B"));
            Assert.IsTrue(allBijectionsList.Select(b => b.Item2).Contains(20));

            var allReverseBijectionsList = new List<Tuple<int, string>>(bijective.GetAllReverseBijections());
            Assert.AreEqual(2, allReverseBijectionsList.Count);
            Assert.IsTrue(allReverseBijectionsList.Select(b => b.Item1).Contains(20));
            Assert.IsTrue(allReverseBijectionsList.Select(b => b.Item2).Contains("B"));
        }

        [TestMethod()]
        public void TryAddReverseBijectionTest()
        {
            var bijective = new Bijective<string, int>();
            bijective[10] = "A";

            Assert.IsFalse(bijective.TryAddReverseBijection(10, "A"));
            Assert.IsFalse(bijective.TryAddReverseBijection(10, "B"));
            Assert.IsFalse(bijective.TryAddReverseBijection(20, "A"));
            Assert.IsTrue(bijective.TryAddReverseBijection(20, "B"));

            Assert.IsTrue(bijective.LeftElementExists("B"));
            Assert.IsTrue(bijective.RightElementExists(20));

            var allLeftElementsList = new List<string>(bijective.GetAllLeftElements());
            Assert.AreEqual(2, allLeftElementsList.Count);
            Assert.IsTrue(allLeftElementsList.Contains("B"));

            var allRightElementsList = new List<int>(bijective.GetAllRightElements());
            Assert.AreEqual(2, allRightElementsList.Count);
            Assert.IsTrue(allRightElementsList.Contains(20));

            Assert.IsFalse(bijective.CanAddBijection("B", 20));
            Assert.IsFalse(bijective.CanAddReverseBijection(20, "B"));

            Tuple<string, int> bijection;
            Assert.IsTrue(bijective.TryGetBijection("B", out bijection));
            Assert.AreEqual("B", bijection.Item1);
            Assert.AreEqual(20, bijection.Item2);

            Tuple<int, string> reverseBijection;
            Assert.IsTrue(bijective.TryGetReverseBijection(20, out reverseBijection));
            Assert.AreEqual(20, reverseBijection.Item1);
            Assert.AreEqual("B", reverseBijection.Item2);

            var allBijectionsList = new List<Tuple<string, int>>(bijective.GetAllBijections());
            Assert.AreEqual(2, allBijectionsList.Count);
            Assert.IsTrue(allBijectionsList.Select(b => b.Item1).Contains("B"));
            Assert.IsTrue(allBijectionsList.Select(b => b.Item2).Contains(20));

            var allReverseBijectionsList = new List<Tuple<int, string>>(bijective.GetAllReverseBijections());
            Assert.AreEqual(2, allReverseBijectionsList.Count);
            Assert.IsTrue(allReverseBijectionsList.Select(b => b.Item1).Contains(20));
            Assert.IsTrue(allReverseBijectionsList.Select(b => b.Item2).Contains("B"));
        }

        [TestMethod()]
        public void TryGetBijectionTest()
        {
            var bijective = new Bijective<string, int>();

            Tuple<string, int> bijection;
            Assert.IsFalse(bijective.TryGetBijection("A", out bijection));
            Assert.IsNull(bijection);

            bijective["A"] = 10;

            Assert.IsTrue(bijective.TryGetBijection("A", out bijection));
            Assert.IsNotNull(bijection);

            Assert.AreEqual("A", bijection.Item1);
            Assert.AreEqual(10, bijection.Item2);
        }

        [TestMethod()]
        public void TryGetReverseBijectionTest()
        {
            var bijective = new Bijective<string, int>();

            Tuple<int, string> reverseBijection;
            Assert.IsFalse(bijective.TryGetReverseBijection(10, out reverseBijection));
            Assert.IsNull(reverseBijection);

            bijective["A"] = 10;

            Assert.IsTrue(bijective.TryGetReverseBijection(10, out reverseBijection));
            Assert.IsNotNull(reverseBijection);

            Assert.AreEqual(10, reverseBijection.Item1);
            Assert.AreEqual("A", reverseBijection.Item2);
        }

        [TestMethod()]
        public void RemoveLeftElementTest()
        {
            var bijective = new Bijective<string, int>();

            try
            {
                bijective.RemoveLeftElement("A");
            }
            catch
            {
                Assert.Fail();
            }

            bijective["A"] = 10;
            bijective["B"] = 20;

            bijective.RemoveLeftElement("A");

            Assert.IsFalse(bijective.LeftElementExists("A"));
            Assert.IsFalse(bijective.RightElementExists(10));

            var allLeftElementsList = new List<string>(bijective.GetAllLeftElements());
            Assert.AreEqual(1, allLeftElementsList.Count);
            Assert.IsTrue(allLeftElementsList.Contains("B"));

            var allRightElementsList = new List<int>(bijective.GetAllRightElements());
            Assert.AreEqual(1, allRightElementsList.Count);
            Assert.IsTrue(allRightElementsList.Contains(20));

            Assert.IsTrue(bijective.CanAddBijection("A", 10));
            Assert.IsTrue(bijective.CanAddReverseBijection(10, "A"));

            Tuple<string, int> bijection;
            Assert.IsFalse(bijective.TryGetBijection("A", out bijection));
            Assert.IsNull(bijection);

            Tuple<int, string> reverseBijection;
            Assert.IsFalse(bijective.TryGetReverseBijection(10, out reverseBijection));
            Assert.IsNull(reverseBijection);

            var allBijectionsList = new List<Tuple<string, int>>(bijective.GetAllBijections());
            Assert.AreEqual(1, allBijectionsList.Count);
            Assert.AreEqual("B", allBijectionsList[0].Item1);
            Assert.AreEqual(20, allBijectionsList[0].Item2);

            var allReverseBijectionsList = new List<Tuple<int, string>>(bijective.GetAllReverseBijections());
            Assert.AreEqual(1, allReverseBijectionsList.Count);
            Assert.AreEqual(20, allReverseBijectionsList[0].Item1);
            Assert.AreEqual("B", allReverseBijectionsList[0].Item2);
        }

        [TestMethod()]
        public void RemoveRightElementTest()
        {
            var bijective = new Bijective<string, int>();

            try
            {
                bijective.RemoveRightElement(10);
            }
            catch
            {
                Assert.Fail();
            }

            bijective["A"] = 10;
            bijective["B"] = 20;

            bijective.RemoveRightElement(10);

            Assert.IsFalse(bijective.LeftElementExists("A"));
            Assert.IsFalse(bijective.RightElementExists(10));

            var allLeftElementsList = new List<string>(bijective.GetAllLeftElements());
            Assert.AreEqual(1, allLeftElementsList.Count);
            Assert.IsTrue(allLeftElementsList.Contains("B"));

            var allRightElementsList = new List<int>(bijective.GetAllRightElements());
            Assert.AreEqual(1, allRightElementsList.Count);
            Assert.IsTrue(allRightElementsList.Contains(20));

            Assert.IsTrue(bijective.CanAddBijection("A", 10));
            Assert.IsTrue(bijective.CanAddReverseBijection(10, "A"));

            Tuple<string, int> bijection;
            Assert.IsFalse(bijective.TryGetBijection("A", out bijection));
            Assert.IsNull(bijection);

            Tuple<int, string> reverseBijection;
            Assert.IsFalse(bijective.TryGetReverseBijection(10, out reverseBijection));
            Assert.IsNull(reverseBijection);

            var allBijectionsList = new List<Tuple<string, int>>(bijective.GetAllBijections());
            Assert.AreEqual(1, allBijectionsList.Count);
            Assert.AreEqual("B", allBijectionsList[0].Item1);
            Assert.AreEqual(20, allBijectionsList[0].Item2);

            var allReverseBijectionsList = new List<Tuple<int, string>>(bijective.GetAllReverseBijections());
            Assert.AreEqual(1, allReverseBijectionsList.Count);
            Assert.AreEqual(20, allReverseBijectionsList[0].Item1);
            Assert.AreEqual("B", allReverseBijectionsList[0].Item2);
        }

        [TestMethod()]
        public void ClearTest_GivenABlankBijective()
        {
            var bijective = new Bijective<object, object>();

            IEnumerable<Tuple<object, object>> allBijections = null;
            IEnumerable<Tuple<object, object>> allReverseBijections = null;
            IEnumerable<object> allItemsLeft = null;
            IEnumerable<object> allItemsRight = null;

            try
            {
                bijective.Clear();
                allBijections = bijective.GetAllBijections();
                allReverseBijections = bijective.GetAllReverseBijections();
                allItemsLeft = bijective.GetAllLeftElements();
                allItemsRight = bijective.GetAllRightElements();
            }
            catch
            {
                Assert.Fail();
            }

            Assert.AreEqual(0, new List<Tuple<object, object>>(allBijections).Count);
            Assert.AreEqual(0, new List<Tuple<object, object>>(allReverseBijections).Count);
            Assert.AreEqual(0, new List<object>(allItemsLeft).Count);
            Assert.AreEqual(0, new List<object>(allItemsRight).Count);
        }

        [TestMethod()]
        public void ClearTest_GivenANonBlankBijective()
        {
            var bijective = BijectiveTestsHelper.GetBijective1();

            bijective.Clear();

            var allBijections = bijective.GetAllBijections();
            Assert.AreEqual(0, new List<Tuple<string, int>>(allBijections).Count);

            var allReverseBijections = bijective.GetAllReverseBijections();
            Assert.AreEqual(0, new List<Tuple<int, string>>(allReverseBijections).Count);

            var allItemsLeft = bijective.GetAllLeftElements();
            Assert.AreEqual(0, new List<string>(allItemsLeft).Count);

            var allItemsRight = bijective.GetAllRightElements();
            Assert.AreEqual(0, new List<int>(allItemsRight).Count);
        }

        [TestMethod()]
        public void IndexerGetTest_GivenABlankBijective()
        {
            var bijective = new Bijective<string, int>();

            var leftToRightIndexerGetFailed = false;
            try
            {
                var rightElement = bijective["A"];
            }
            catch(ArgumentOutOfRangeException)
            {
                leftToRightIndexerGetFailed = true;
            }
            Assert.IsTrue(leftToRightIndexerGetFailed);

            var rightToLeftIndexerGetFailed = false;
            try
            {
                var leftElement = bijective[10];
            }
            catch (ArgumentOutOfRangeException)
            {
                rightToLeftIndexerGetFailed = true;
            }
            Assert.IsTrue(rightToLeftIndexerGetFailed);
        }

        [TestMethod()]
        public void IndexerSetTest_GivenANonBlankBijective()
        {
            var bijective = new Bijective<string, int>();
            bijective["A"] = 10;

            var leftToRightIndexerSetFailed = false;
            try
            {
                bijective["A"] = 10;
            }
            catch (ArgumentException)
            {
                leftToRightIndexerSetFailed = true;
            }
            Assert.IsTrue(leftToRightIndexerSetFailed);

            leftToRightIndexerSetFailed = false;
            try
            {
                bijective["A"] = 20;
            }
            catch (ArgumentException)
            {
                leftToRightIndexerSetFailed = true;
            }
            Assert.IsTrue(leftToRightIndexerSetFailed);

            leftToRightIndexerSetFailed = false;
            try
            {
                bijective["B"] = 10;
            }
            catch (ArgumentException)
            {
                leftToRightIndexerSetFailed = true;
            }
            Assert.IsTrue(leftToRightIndexerSetFailed);

            var rightToLeftIndexerSetFailed = false;
            try
            {
                bijective[10] = "A";
            }
            catch (ArgumentException)
            {
                rightToLeftIndexerSetFailed = true;
            }
            Assert.IsTrue(rightToLeftIndexerSetFailed);

            rightToLeftIndexerSetFailed = false;
            try
            {
                bijective[10] = "B";
            }
            catch (ArgumentException)
            {
                rightToLeftIndexerSetFailed = true;
            }
            Assert.IsTrue(rightToLeftIndexerSetFailed);

            rightToLeftIndexerSetFailed = false;
            try
            {
                bijective[20] = "A";
            }
            catch (ArgumentException)
            {
                rightToLeftIndexerSetFailed = true;
            }
            Assert.IsTrue(rightToLeftIndexerSetFailed);
        }
    }
}