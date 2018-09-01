using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeGraphPropagation.Core
{
    public class FastestTreeGraphPropagation<TNode> where TNode : class, IEqualityComparer<TNode>
    {
        private readonly List<Tuple<TNode, TNode>> EdgesList;
        private int DividingIndex;


        public FastestTreeGraphPropagation(List<Tuple<TNode, TNode>> edgesList)
        {
            EdgesList = edgesList;
            DividingIndex = 1;
        }

        public FastestTreeGraphPropagation(List<Tuple<TNode, TNode>> edgesList, int dividingIndex)
        {
            EdgesList = edgesList;
            DividingIndex = dividingIndex;
        }

        public Root<TNode>[] GetFastOptimisedRoot()
        {
            if (EdgesList == null || !EdgesList.Any()) return null;
            if (EdgesList.Count == 1) return new Root<TNode>[2] { new Root<TNode>(EdgesList.FirstOrDefault().Item1, 1), new Root<TNode>(EdgesList.FirstOrDefault().Item2, 1) };
            var itemList = EdgesList.SelectMany(x => new List<TNode> { x.Item1, x.Item2 }).ToList();

            var susbListSize = (itemList.Count / DividingIndex);
            susbListSize = susbListSize + susbListSize % 2;

            var subItemList = SplitList(itemList, susbListSize).ToList();

            var indexPropagation = 0;
            while (subItemList.Sum(item => item.Count) > 2)
            {

                var extremumsList = subItemList.SelectMany(x => x).GroupBy(c => c).Where(x => x.Count() == 1).Select(x => x.Key).ToList();

                if (extremumsList.Count() + 1 == subItemList.SelectMany(x => x).Distinct().Count())
                {
                    indexPropagation++;
                    break;
                }
                for (int i = 0; i < subItemList.Count; i++)
                {
                    foreach (var item in extremumsList.Intersect(subItemList[i]))
                    {

                        if (!subItemList[i].Any()) break;

                        var index = subItemList[i].IndexOf(item);


                        subItemList[i].RemoveRange(index - (index % 2), 2);

                    }
                }
                indexPropagation++;
            }
            var result = subItemList.SelectMany(x => x).ToList();
            if (subItemList.Sum(item => item.Count) == 2) indexPropagation++;
            if (subItemList.Sum(item => item.Count) > 2) result = result.GroupBy(c => c).Where(x => x.Count() > 1).Select(x => x.Key).ToList();
            return result.Select(x => new Root<TNode>(x, indexPropagation)).ToArray();
        }

        private IEnumerable<List<TNode>> SplitList(List<TNode> locations, int nSize)
        {
            for (int i = 0; i < locations.Count; i += nSize)
            {
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
            }
        }


    }
}
