using System;
using System.Collections.Generic;
using System.Linq;

namespace Bijective
{
    public class Bijective<T1, T2> : IBijective<T1, T2>
    {
        private Dictionary<T1, int> setOfLeftElements;
        private Dictionary<T2, int> setOfRightElements;

        private List<Tuple<int, int>> bijections;

        public Bijective()
        {
            setOfLeftElements = new Dictionary<T1, int>();
            setOfRightElements = new Dictionary<T2, int>();

            bijections = new List<Tuple<int, int>>();
        }

        #region Exists
        public bool LeftElementExists(T1 leftElement)
        {
            return setOfLeftElements.ContainsKey(leftElement);
        }

        public bool RightElementExists(T2 rightElement)
        {
            return setOfRightElements.ContainsKey(rightElement);
        }
        #endregion

        #region IndexOf
        private int? IndexOfLeftElement(T1 leftElement)
        {
            if(!setOfLeftElements.ContainsKey(leftElement))
            {
                return null;
            }

            return setOfLeftElements[leftElement];
        }

        private int? IndexOfRightElement(T2 rightElement)
        {
            if (!setOfRightElements.ContainsKey(rightElement))
            {
                return null;
            }

            return setOfRightElements[rightElement];
        }
        #endregion

        #region GetByIndex
        private T1 GetLeftElementByIndex(int indexOfLeftElement)
        {
            return setOfLeftElements.Keys.Single(leftElement => setOfLeftElements[leftElement] == indexOfLeftElement);
        }

        private T2 GetRightElementByIndex(int indexOfRightElement)
        {
            return setOfRightElements.Keys.Single(rightElement => setOfRightElements[rightElement] == indexOfRightElement);
        }
        #endregion

        #region GetBijectionIndex
        private int GetRightIndexByBijection(int indexOfLeftElement)
        {
            return bijections.Single(b => b.Item1 == indexOfLeftElement).Item2;
        }

        private int GetLeftIndexByBijection(int indexOfRightElement)
        {
            return bijections.Single(b => b.Item2 == indexOfRightElement).Item1;
        }
        #endregion

        #region []
        public T2 this[T1 leftElement]
        {
            get
            {
                if (!LeftElementExists(leftElement))
                {
                    throw new ArgumentOutOfRangeException($"Bijection's set of left elements does not contain the provided one");
                }

                var indexOfLeftElement = IndexOfLeftElement(leftElement).Value;
                var indexOfRightElement = GetRightIndexByBijection(indexOfLeftElement);
                var rightElement = GetRightElementByIndex(indexOfRightElement);

                return rightElement;
            }

            set
            {
                if(!CanAddBijection(leftElement, value))
                {
                    throw new ArgumentException("Bijection already contains one of the provided elements");
                }

                Add(leftElement, value);
            }
        }

        private void Add(T1 leftElement, T2 rightElement)
        {
            var indexOfLeftElement = setOfLeftElements.Count;
            var indexOfRightElement = setOfRightElements.Count;

            setOfLeftElements[leftElement] = indexOfLeftElement;
            setOfRightElements[rightElement] = indexOfRightElement;

            bijections.Add(new Tuple<int, int>(indexOfLeftElement, indexOfRightElement));
        }

        public T1 this[T2 rightElement]
        {
            get
            {
                if (!RightElementExists(rightElement))
                {
                    throw new ArgumentOutOfRangeException($"Bijection's set of right elements does not contain the provided element");
                }

                var indexOfRightElement = IndexOfRightElement(rightElement).Value;
                var indexOfLeftElement = GetLeftIndexByBijection(indexOfRightElement);
                var leftElement = GetLeftElementByIndex(indexOfLeftElement);

                return leftElement;
            }

            set
            {
                if (!CanAddBijection(value, rightElement))
                {
                    throw new ArgumentException("Bijection already contains one of the provided elements");
                }

                Add(value, rightElement);
            }
        }
        #endregion

        #region CanAdd
        public bool CanAddBijection(T1 leftElement, T2 rightElement)
        {
            return !LeftElementExists(leftElement) && !RightElementExists(rightElement);
        }

        public bool CanAddReverseBijection(T2 rightElement, T1 leftElement)
        {
            return CanAddBijection(leftElement, rightElement);
        }
        #endregion

        #region GetAllBijections
        public IEnumerable<Tuple<T1, T2>> GetAllBijections()
        {
            return bijections.Select(b => new Tuple<T1, T2>(GetLeftElementByIndex(b.Item1), GetRightElementByIndex(b.Item2)));
        }

        public IEnumerable<Tuple<T2, T1>> GetAllReverseBijections()
        {
            return bijections.Select(b => new Tuple<T2, T1>(GetRightElementByIndex(b.Item2), GetLeftElementByIndex(b.Item1)));
        }
        #endregion

        #region GetAllElements
        public IEnumerable<T1> GetAllLeftElements()
        {
            return setOfLeftElements.Keys;
        }

        public IEnumerable<T2> GetAllRightElements()
        {
            return setOfRightElements.Keys;
        }
        #endregion

        #region TryAdd
        public bool TryAddBijection(T1 leftElement, T2 rightElement)
        {
            try
            {
                this[leftElement] = rightElement;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryAddReverseBijection(T2 rightElement, T1 leftElement)
        {
            try
            {
                this[rightElement] = leftElement;
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region TryGet
        public bool TryGetBijection(T1 leftElement, out Tuple<T1, T2> bijection)
        {
            bijection = null;

            if (!LeftElementExists(leftElement))
            {
                return false;
            }

            bijection = new Tuple<T1, T2>(leftElement, this[leftElement]);
            return true;
        }

        public bool TryGetReverseBijection(T2 rightElement, out Tuple<T2, T1> reverseBijection)
        {
            reverseBijection = null;

            if (!RightElementExists(rightElement))
            {
                return false;
            }

            reverseBijection = new Tuple<T2, T1>(rightElement, this[rightElement]);
            return true;
        }
        #endregion

        #region Remove
        public void RemoveLeftElement(T1 leftElement)
        {
            if(LeftElementExists(leftElement))
            {
                var indexLeftElement = IndexOfLeftElement(leftElement).Value;
                var rightElement = this[leftElement];

                bijections.RemoveAll(b => b.Item1 == indexLeftElement);

                setOfRightElements.Remove(rightElement);
                setOfLeftElements.Remove(leftElement);
            }
        }

        public void RemoveRightElement(T2 rightElement)
        {
            if (RightElementExists(rightElement))
            {
                var indexRightElement = IndexOfRightElement(rightElement).Value;
                var leftElement = this[rightElement];

                bijections.RemoveAll(b => b.Item2 == indexRightElement);

                setOfRightElements.Remove(rightElement);
                setOfLeftElements.Remove(leftElement);
            }
        }
        #endregion

        #region Clear
        public void Clear()
        {
            bijections.Clear();
            setOfRightElements.Clear();
            setOfLeftElements.Clear();
        }
        #endregion
    }
}
