using System.Collections.Generic;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Models;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Models;
using CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Processing;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Legacy.Processing {
    public class FilterEngine {

//        private Logger logger = LogManager.GetInstance().GetLogger(typeof(FilterEngine));

        private Queue<FilterStrategy> strategies = new Queue<FilterStrategy>();

        public void AddFilterStrategy(FilterStrategy strategy) {
//            logger.Debug("Adding filter strategy: "+strategy.GetType().FullName);
            strategies.Enqueue(strategy);
        }

        public void Reset() {
//            logger.Debug("Clearing filter list");
            strategies.Clear();
        }

        public List<MultimediaObject> ApplyFilters(List<MultimediaObject> list) {
            
            List<MultimediaObject> working = new List<MultimediaObject>(list);
//            logger.Debug("Applying filters. Original list size: " + working.Count);
            foreach (FilterStrategy strategy in strategies) {
//                logger.Debug("Going to apply filter of "+strategy.ToString());
                working = strategy.applyFilter(working);
            }

//            logger.Debug("Finished applying all filters. Remaining list size "+working.Count);
            return working;
        }

        public int GetFilterCount() {
            return strategies.Count;
        }
    }
}