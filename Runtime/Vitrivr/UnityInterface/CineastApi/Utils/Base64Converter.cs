using System;
using UnityEngine;

namespace CineastUnityInterface.Runtime.Vitrivr.UnityInterface.CineastApi.Utils
{
  public static class Base64Converter
  {
    private const string PrefixPrefix = "data:";
    private const string PrefixSuffix = ";base64,";
    public static readonly string JsonPrefix = PrefixPrefix + "application/json" + PrefixSuffix;
    public static readonly string PNGPrefix = PrefixPrefix + "image/png" + PrefixSuffix;

    public static string StringToBase64(string str)
    {
      return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(str));
    }

    public static string StringFromBase64(string str)
    {
      return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(str));
    }

    public static string ImageToBase64PNG(Texture2D image)
    {
      return Convert.ToBase64String(image.EncodeToPNG());
    }

    public static Texture2D ImageFromBase64PNG(string encodedImage)
    {
      var data = Convert.FromBase64String(encodedImage);
      // Texture size doesn't matter because it will be replaced during load
      var texture = new Texture2D(2, 2);
      texture.LoadImage(data);
      return texture;
    }
  }
}