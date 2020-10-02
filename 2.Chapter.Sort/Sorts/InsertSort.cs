using System;

namespace Sorts
{
    public class InsertSort : SortAbstract
    {
        public override void Sort(IComparable[] a)
        {
            for (int i = 1; i < a.Length; i++)
            {
                for (int j = i; j - 1 >= 0 && Less(a[j], a[j - 1]); j--)
                {
                    Swap(a, j, j - 1);
                }
            }
        }

        /// <summary>
        /// 把 [endIndex,startIndex]的元素都往后移动一位
        /// </summary>
        /// <param name="a"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        private void MoveNext(IComparable[] a, int startIndex, int endIndex)
        {
            int moveIndex = endIndex;
            while (moveIndex >= startIndex && moveIndex < a.Length - 1)
            {
                a[moveIndex + 1] = a[moveIndex];
                moveIndex--;
            }
        }
    }
}
