// *================================================================*
// This script is taken from the github Rimuru Dev
// GitHub: https://github.com/RimuruDev/JSONUtility
// This script has not been tested before being used in this project.
// This script was written a long time ago. And no longer serviced.
// *================================================================*

using System;
using UnityEngine;
using UnityEngine.Networking;

namespace DIMEDUS.RimuruDev.JSONUtility
{
    [Serializable]
    public sealed class JSONReader<T>
    {
        [Obsolete]
        public static T Read(string jsonFileName, bool isDefaultPath = true)
        {
            try
            {
                //TODO: UnityWebRequest
                WWW data = null;

                if (isDefaultPath)
                    data = new WWW($"{Application.streamingAssetsPath}/{jsonFileName}.json");
                else
                    data = new WWW(jsonFileName);

                return JsonUtility.FromJson<T>(data.text);
            }
            catch (Exception ex)
            {
                Debug.LogError("RimuruDev.JSONUtility.JSONReader<T>...");
                Debug.LogException(ex);
                Debug.LogError($"JSONReader<{typeof(T)}>.{typeof(T)} Read(string {jsonFileName}");
                throw;
            }
        }
    }
}