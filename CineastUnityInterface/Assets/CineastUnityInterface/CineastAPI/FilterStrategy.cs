using System.Collections.Generic;

namespace CineastUnityInterface.CineastAPI {
    public interface FilterStrategy {

        /**
         * <summary>Filters some mmos out and returnes the filtered list</summary>
         */
        List<MultimediaObject> applyFilter(List<MultimediaObject> list);
    }
}