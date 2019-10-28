using System;
using System.IO;
using UnityEngine;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Utils {
    public class CineastConfiguration {
        public CineastConfiguration() { }

        public CineastConfiguration(string cineastHost, string imagesHost) {
            this.cineastHost = cineastHost;
            this.imagesHost = imagesHost;
        }

        public string cineastHost;
        public string imagesHost;

        public bool IsEmpty() {
            return string.IsNullOrEmpty(cineastHost) || string.IsNullOrEmpty(imagesHost);
        }

        public const string API_VERSION = "api/v1/";
        public const string SIMILAR_QUERY_URL = "find/segments/similar";
        public const string SEGMENTS_QUERY_URL = "find/segments/by/id";
        public const string METAS_QUERY_URL = "find/metadata/by/id";
        public const string OBJECT_QUERY_URL = "find/objects/by/id";

        public string FindSimilarSegmentsUrl() {
            return cineastHost + API_VERSION + SIMILAR_QUERY_URL;
        }

        public string FindSegmentsByIdUrl() {
            return cineastHost + API_VERSION + SEGMENTS_QUERY_URL;
        }

        public string FindMetadataUrl() {
            return cineastHost + API_VERSION + METAS_QUERY_URL;
        }

        public string FindObjectsUrl() {
            return cineastHost + API_VERSION + OBJECT_QUERY_URL;
        }

        public bool Store()
        {
            WriteJson(JsonUtility.ToJson(this), GetFilePath());
            return File.Exists(GetFilePath());
        }

        public const string FILE_NAME = "cineast";
        public const string FILE_EXTENSION = "json";
        public const string FILE_EXTENSION_LEGACY = "config";

        public static Boolean HasConfig() {
            return File.Exists(GetFilePath()) || File.Exists(GetFilePath(true));
        }

        public static CineastConfiguration Load() {
            if (!HasConfig()) {
                throw new FileNotFoundException("Configuration not found", GetFilePath());
            }

            CineastConfiguration config = ReadJsonFirst<CineastConfiguration>(GetFilePath(), GetFilePath(true));

            // Sanatize
            if (!string.IsNullOrEmpty(config.cineastHost) && !config.cineastHost.EndsWith("/")) {
                config.cineastHost += "/";
            }

            if (!string.IsNullOrEmpty(config.imagesHost) && !config.imagesHost.EndsWith("/")) {
                config.imagesHost += "/";
            }

            return config;
        }

        private static void WriteJson(string json, string path) {
            StreamWriter sw = File.CreateText(path);
            sw.WriteLine(json);
            sw.Flush();
            sw.Close();
        }

        private static T ReadJson<T>(string path) {
            StreamReader sr = File.OpenText(path);
            string content = sr.ReadToEnd();
            sr.Close();
            return UnityEngine.JsonUtility.FromJson<T>(content);
        }

        private static T ReadJsonFirst<T>(params string[] paths)
        {
            foreach (string path in paths)
            {
                if (File.Exists(path))
                {
                    StreamReader sr = File.OpenText(path);
                    string content = sr.ReadToEnd();
                    sr.Close();
                    return UnityEngine.JsonUtility.FromJson<T>(content);                    
                }
            }
            throw new FileNotFoundException("Configuration not found");
        }

        private static string GetFilePath(bool legacy = false) {
            #if UNITY_EDITOR
                return Application.dataPath + "/" + $"{FILE_NAME}.{(legacy ? FILE_EXTENSION_LEGACY : FILE_EXTENSION)}";
            #elif UNITY_ANDROID
                return Application.persistentDataPath + "/" + FILE_NAME;
            #endif
        }

        public static void StoreEmpty() {
            WriteJson(JsonUtility.ToJson(new CineastConfiguration()), GetFilePath());
        }

        public static CineastConfiguration GetDefault() {
            return new CineastConfiguration("http://localhost:4567/", "http://localhost/");
        }
    }
}