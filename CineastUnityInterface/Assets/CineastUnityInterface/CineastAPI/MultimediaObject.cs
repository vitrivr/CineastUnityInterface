using System;
using System.Collections.Generic;
using System.Linq;
using CineastUnityInterface.CineastAPI.Result;

namespace CineastUnityInterface.CineastAPI {
    /**
     * The object represents an object in cineast.
     * An object has primarily an ID with which additional meta data is requestable.
     * 
     * */
    [Serializable]
    public class MultimediaObject : IEquatable<MultimediaObject> {
        private static readonly IdEqualityComparer comparer = new IdEqualityComparer();

        public string datetime;
        public string id;

        public double latitude = double.NaN;
        public double longitude = double.NaN;
        public double bearing = double.NaN;

        public string name;
        public string path;

        public int resultIndex = -8;

        public bool Equals(MultimediaObject other) {
            return comparer.Equals(this, other);
        }

        public override string ToString() {
            return base.ToString() + "[id=" + id + ", LAT=" + latitude + ", LON=" + longitude +", bearing="+bearing+ ", name=" +
                   (name ?? "NULL") + ", path=" + (path ?? "NULL") +", datetime="+(datetime ?? "NULL")+ "]";
        }


        /**
         * Adds the given meta data to this object.
         * If the meta data has an unkown key or the ID is not matching, FALSE will be returned.
         * */
        public bool AddMetaData(MetaDataObject meta) {
            if (id.Equals(meta.objectId)) {
                if (CineastUtils.KNOWN_KEYS.Contains(meta.key)) {
                    var changes = false;
                    switch (meta.key) {
                        case CineastUtils.LATITUDE_KEY:
                            if (double.IsNaN(latitude)) {
                                latitude = double.Parse(meta.value);
                                changes = true;
                            }
                            break;
                        case CineastUtils.LONGITUDE_KEY:
                            if (double.IsNaN(longitude)) {
                                longitude = double.Parse(meta.value);
                                changes = true;
                            }
                            break;
                        case CineastUtils.DATETIME_KEY:
                            if (datetime == null) {
                                datetime = meta.value;
                                changes = true;
                            }
                            break;
                        case CineastUtils.BEARING_KEY:
                            if (double.IsNaN(bearing)) {
                                bearing = double.Parse(meta.value);
                                changes = true;
                            }
                            break;
                    }

                    return changes;
                }
                return false;
            }
            return false;
        }

        /**
         * Merges this and the passed object with a 'this-first' strategy.
         * If any of the fields of this object are not set and
         * the other object's corresponding field is set, the field
         * of this object is then set with the value of the other object.
         * 
         * Objects must have the same ID, otherwise no merge is performed
         * 
         * @param other - The other object to merge into this object
         * @return TRUE if a merge action happened - false otherwise
         * */
        public bool Merge(MultimediaObject other) {
            if (!Equals(other)) {
                return false;
            }
            bool output;
            output = MergeLat(other);
            //Debug.Log("Merged lat: " + output);
            output = MergeLon(other);
            //Debug.Log("Merged lon: " + output);
            output = MergeBearing(other);
            //Debug.Log("Merged bearing: "+output);
            output = MergeName(other);
            //Debug.Log("Merged name: " + output);
            output = MergePath(other);
            //Debug.Log("Merged path: " + output);
            output = MergeTime(other);
            //Debug.Log("Merged time: " + output);
            return output;
        }

        private bool MergeBearing(MultimediaObject other) {
            if (double.IsNaN(bearing) && !double.IsNaN(other.bearing)) {
                bearing = other.bearing;
                return true;
            }

            return false;
        }

        private bool MergeLat(MultimediaObject other) {
            if (double.IsNaN(latitude) && !double.IsNaN(other.latitude)) {
                latitude = other.latitude;
                return true;
            }
            return false;
        }

        private bool MergeLon(MultimediaObject other) {
            if (double.IsNaN(longitude) && !double.IsNaN(other.longitude)) {
                longitude = other.longitude;
                return true;
            }
            return false;
        }

        private bool MergeName(MultimediaObject other) {
            if ((name == null) && (other.name != null)) {
                name = other.name;
                return true;
            }
            return false;
        }

        private bool MergePath(MultimediaObject other) {
            if ((path == null) && (other.path != null)) {
                path = other.path;
                return true;
            }
            return false;
        }

        private bool MergeTime(MultimediaObject other) {
            if ((datetime == null) && (other.datetime != null)) {
                datetime = other.datetime;
                return true;
            }
            return false;
        }

        public sealed class IdEqualityComparer : IEqualityComparer<MultimediaObject> {
            public bool Equals(MultimediaObject x, MultimediaObject y) {
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
                return string.Equals(x.id, y.id);
            }

            public int GetHashCode(MultimediaObject obj) {
                return obj.id != null ? obj.id.GetHashCode() : 0;
            }
        }
    }
}