﻿using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Utils
{
    public class FileUtils
    {
        /// <summary>
        /// Attempts to read the with path specified json file using Unity's JSON utility
        /// </summary>
        /// <param name="path"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ReadJsonUnity<T>(string path)
        {
            StreamReader sr = File.OpenText(path);
            string content = sr.ReadToEnd();
            sr.Close();
            return UnityEngine.JsonUtility.FromJson<T>(content);
        }

        /// <summary>
        /// Attempts to write a json file using Unity's JSON utility
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        public static void WriteJsonUnity(object obj, string path)
        {
            StreamWriter sw = File.CreateText(path);
            sw.Write(JsonUtility.ToJson(obj));
            sw.WriteLine(""); // empty line at EOF
            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// Attempts to read the with path specified json file using Unity's JSON utility
        /// </summary>
        /// <param name="path"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ReadJson<T>(string path)
        {
            StreamReader sr = File.OpenText(path);
            string content = sr.ReadToEnd();
            sr.Close();
            return JsonConvert.DeserializeObject<T>(content);
        }

        /// <summary>
        /// Attempts to write a json file using Unity's JSON utility
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        public static void WriteJson(object obj, string path)
        {
            StreamWriter sw = File.CreateText(path);
            sw.Write(JsonConvert.SerializeObject(obj));
            sw.WriteLine(""); // empty line at EOF
            sw.Flush();
            sw.Close();
        }
    }
}
