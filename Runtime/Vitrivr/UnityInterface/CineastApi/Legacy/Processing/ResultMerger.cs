using System;
using System.Collections.Generic;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Models.Messages.Result;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Processing;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Processing {
    public class ResultMerger {
        private sealed class Triple {
            private string category;
            private string key;
            private double score;

            public string Category
            {
                get { return category; }
                set { category = value; }
            }

            public string Key
            {
                get { return key; }
                set { key = value; }
            }

            public double Score
            {
                get { return score; }
                set { this.score = value; }
            }

            public Triple(string category, string key, double score) {
                this.category = category;
                this.key = key;
                this.score = score;
            }

            public static Triple FromValues(string category, string key, string value) {
                return new Triple(category, key, Convert.ToDouble(value));
            }

            private bool Equals(Triple other) {
                return string.Equals(key, other.key);
            }

            public override bool Equals(object obj) {
                if (ReferenceEquals(null, obj)) {
                    return false;
                }

                if (ReferenceEquals(this, obj)) {
                    return true;
                }

                return obj is Triple && Equals((Triple) obj);
            }

            public override int GetHashCode() {
                return (key != null ? key.GetHashCode() : 0);
            }

            private sealed class KeyEqualityComparer : IEqualityComparer<Triple> {
                public bool Equals(Triple x, Triple y) {
                    if (ReferenceEquals(x, y)) {
                        return true;
                    }

                    if (ReferenceEquals(x, null)) {
                        return false;
                    }

                    if (ReferenceEquals(y, null)) {
                        return false;
                    }

                    if (x.GetType() != y.GetType()) {
                        return false;
                    }

                    return string.Equals(x.key, y.key);
                }

                public int GetHashCode(Triple obj) {
                    return (obj.key != null ? obj.key.GetHashCode() : 0);
                }
            }

            private static readonly IEqualityComparer<Triple> KeyComparerInstance = new KeyEqualityComparer();

            public static IEqualityComparer<Triple> KeyComparer
            {
                get { return KeyComparerInstance; }
            }
        }

        /// <summary>
        /// Merges multiple ResultObjects of different categories to a single list of ContentObjects.
        /// The merge is based on a set of weights.
        /// </summary>
        /// <param name="results">An array of different ResultObjects for several categories</param>
        /// <param name="ratio">An array with weights for the different categories in the results array</param>
        /// <returns>A list of ContentObjects where the content's value is the weighted score per category. The list is sorted in descending order (the higher the value, the earlier in the list)</returns>
        public List<ContentObject> Merge(ResultObject[] results, CategoryRatio ratio) {
            // Attetion! No sanity checks: Must be different result.categories, ratios must sum up to 1.
            List<ContentObject> totalResults = new List<ContentObject>();
            List<List<Triple>> scores = Prepare(results);
            while (scores.Count > 0) {
                List<Triple> list = Pop(scores);
                while (list.Count > 0) {
                    Triple needle = Pop(list);
                    bool lonely = true;
                    foreach (List<Triple> haystack in scores) {
                        int size = haystack.Count;
                        for (int i = 0; i < size; i++) {
                            if (needle.Equals(haystack[i])) {
                                Triple t = haystack[i];
                                totalResults.Add(MergeWeighted(needle.Key, new []{needle.Score, t.Score}, new []{ratio.GetRatio(needle.Category), ratio.GetRatio(t.Category)}));
                                haystack.Remove(t);
                                size--;
                                lonely = false;
                            }
                        }
                    }
                    if (lonely) {
                        double weightedScore = needle.Score * ratio.GetRatio(needle.Category);
                        totalResults.Add(new ContentObject(needle.Key, weightedScore.ToString()));
                    }
                }
            }
            totalResults.Sort((a, b) => -1* a.CompareTo(b));
            
            
            return totalResults;
            
        }
        

        private static T  Pop<T>(List<T> list) {
            if (list.Count >= 1) {
                T t = list[0];
                list.RemoveAt(0);
                return t;
            } else {
                return default(T);
            }
        }

        private List<Triple> ConvertResultObject(ResultObject result) {
            List<Triple> output = new List<Triple>();
            foreach (ContentObject co in result.content) {
                output.Add(Triple.FromValues(result.category, co.key, co.value));
            }

            return output;
        }

        private List<List<Triple>> Prepare(ResultObject[] results) {
            List<List<Triple>> output = new List<List<Triple>>();
            foreach (ResultObject ro in results) {
                output.Add(ConvertResultObject(ro));
            }

            return output;
        }

        /// <summary>
        /// Merges the given scores based on the given weights and returns a new ContentObject (key-value pair) with
        /// the specified key and the weighted sum as value.
        /// The weighted sum is calculated by summing up the result of multiplying the i-th score with the i-th weight.
        /// </summary>
        /// <param name="key">The key to use for the ContentObject</param>
        /// <param name="scores">An array of scores to merge. Size must match the weights' array</param>
        /// <param name="weights">An array of weights. Size must match the scores' array. A weight is a floating point value in the interval 0 and 1 (inclusive)</param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        private ContentObject MergeWeighted(string key, double[] scores, double[] weights) {
            if (scores.Length != weights.Length) {
                throw new IndexOutOfRangeException("Cannot merge scores if weights and scores do not correspond!");
            }

            double weightedScroe = 0;
            for (int i = 0; i < scores.Length; i++) {
                weightedScroe += scores[i] * weights[i];
            }
            return new ContentObject(key, weightedScroe.ToString());
        }
    }
}