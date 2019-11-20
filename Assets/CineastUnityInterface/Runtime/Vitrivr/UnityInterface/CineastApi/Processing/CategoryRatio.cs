using System;
using System.Collections.Generic;
using System.Text;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Processing {
    public class CategoryRatio {

        private Dictionary<string, double> ratios = new Dictionary<string, double>();

        private Guid guid;
        
        public CategoryRatio() {
            guid = Guid.NewGuid();
        }

        public CategoryRatio(string[] categories, double[] weights) {
            if (categories.Length != weights.Length) {
                throw new IndexOutOfRangeException("Must have equal indices");
            }

            for (int i = 0; i < categories.Length; i++) {
                ratios.Add(categories[i], weights[i]);
            }
        }

        public void AddWeight(string category, double wheight) {
            ratios.Add(category, wheight);
        }

        public double GetRatio(string category) {
            return ratios[category];
        }

        private sealed class GuidEqualityComparer : IEqualityComparer<CategoryRatio> {
            public bool Equals(CategoryRatio x, CategoryRatio y) {
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

                return x.guid.Equals(y.guid);
            }

            public int GetHashCode(CategoryRatio obj) {
                return obj.guid.GetHashCode();
            }
        }

        private static readonly IEqualityComparer<CategoryRatio> GuidComparerInstance = new GuidEqualityComparer();

        public static IEqualityComparer<CategoryRatio> GuidComparer
        {
            get { return GuidComparerInstance; }
        }

        protected bool Equals(CategoryRatio other) {
            return guid.Equals(other.guid);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }

            if (ReferenceEquals(this, obj)) {
                return true;
            }

            if (obj.GetType() != this.GetType()) {
                return false;
            }

            return Equals((CategoryRatio) obj);
        }

        public override int GetHashCode() {
            return guid.GetHashCode();
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder("{");
            foreach (KeyValuePair<string, double> pair in ratios) {
                sb.Append(pair.Key).Append("=").Append(pair.Value).Append(',');
            }

            string result = sb.ToString();
            result = result.TrimEnd(',');
            return result+"}";
        }
    }
}