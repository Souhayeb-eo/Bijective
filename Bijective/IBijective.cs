using System;
using System.Collections.Generic;

namespace Bijective
{
    interface IBijective<T1, T2>
    {
        bool TryAddBijection(T1 leftElement, T2 rightElement);
        bool CanAddBijection(T1 leftElement, T2 rightElement);

        bool TryAddReverseBijection(T2 rightElement, T1 leftElement);
        bool CanAddReverseBijection(T2 rightElement, T1 leftElement);

        T2 this[T1 leftElement] { get; set; }
        T1 this[T2 rightElement] { get; set; }

        IEnumerable<T1> GetAllLeftElements();
        IEnumerable<T2> GetAllRightElements();

        bool TryGetBijection(T1 leftElement, out Tuple<T1, T2> bijection);
        bool TryGetReverseBijection(T2 rightElement, out Tuple<T2, T1> reverseBijection);

        IEnumerable<Tuple<T1, T2>> GetAllBijections();
        IEnumerable<Tuple<T2, T1>> GetAllReverseBijections();

        void RemoveLeftElement(T1 leftElement);
        void RemoveRightElement(T2 rightElement);

        bool LeftElementExists(T1 leftElement);
        bool RightElementExists(T2 rightElement);

        void Clear();
    }
}
