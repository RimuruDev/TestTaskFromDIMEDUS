using System;
using System.Collections.Generic;
using Newtonsoft.Json.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace DIMEDUS.RimuruDev
{
    // TODO: Use (Template Method)
    public sealed class SortHandler : MonoBehaviour
    {
        private SceneDataContainer dataContainer = null;
        private UIController uIController = null;

        private void Awake()
        {
            dataContainer = FindObjectOfType<SceneDataContainer>();
            uIController = FindObjectOfType<UIController>();
        }

        public void SortInt(Toggle toggle)
        {
            if (toggle == null) { Debug.LogWarning($"Null Reference Exception->{typeof(SortHandler).Name}.SortInt(Toggle)"); return; }

            SortHelper<int>.SortInt<int>(toggle.isOn, dataContainer);
           // SortInt(toggle.isOn);
        }

        public void SortString(Toggle toggle)
        {
            if (toggle == null) { Debug.LogWarning($"Null Reference Exception->{typeof(SortHandler).Name}.SortString(Toggle)"); return; }

            SortString(toggle.isOn);
        }

        private void SortInt(bool isSortMode)
        {
            List<int> tempListInt = new List<int>();
            int childCount = dataContainer.RightGridParent.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                tempListInt.Add(int.Parse(dataContainer.RightGridParent.transform.GetChild(i).GetComponentInChildren<Text>().text));
            }

            if (isSortMode)
                tempListInt.Sort();
            else
                tempListInt.Reverse();

            SortIntPanel(tempListInt);
        }

        private void SortString(bool isSortMode)
        {
            List<string> tempListString = new List<string>();
            int childCount = dataContainer.RightGridParent.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                tempListString.Add(dataContainer.RightGridParent.transform.GetChild(i).GetChild(1).GetChild(0).GetComponentInChildren<Text>().text);
            }

            if (isSortMode)
                tempListString.Sort();
            else
                tempListString.Reverse();

            SortStringPanel(tempListString);
        }

        private void SortIntPanel(List<int> list)
        {
            GameObject tempParentGO = new GameObject();
            {
                int childCount = dataContainer.RightGridParent.transform.childCount;

                for (int i = 0; i < childCount; i++)
                {
                    dataContainer.RightGridParent.transform.GetChild(0).SetParent(tempParentGO.transform);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    foreach (Transform item in tempParentGO.transform)
                    {
                        if (item.GetChild(0).GetChild(0).GetComponent<Text>().text == list[i].ToString())
                        {
                            item.SetParent(dataContainer.RightGridParent.transform);
                        }
                    }
                }
            }
            Destroy(tempParentGO);
        }

        private void SortStringPanel(List<string> list)
        {
            GameObject tempParentGO = new GameObject();
            {
                int childCount = dataContainer.RightGridParent.transform.childCount;

                for (int i = 0; i < childCount; i++)
                {
                    dataContainer.RightGridParent.transform.GetChild(0).SetParent(tempParentGO.transform);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    foreach (Transform item in tempParentGO.transform)
                    {
                        if (item.GetChild(1).GetChild(0).GetComponent<Text>().text == list[i])
                        {
                            item.SetParent(dataContainer.RightGridParent.transform);
                        }
                    }
                }
            }
            Destroy(tempParentGO);
        }
    }

    public class SortHelper<T> : MonoBehaviour// where T : struct
    {
        public static void SortInt<Value>(bool isSortMode, SceneDataContainer dataContainer)
        {
            List<Value> tempListInt = new List<Value>();
            int childCount = dataContainer.RightGridParent.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                tempListInt.Add((Value)Convert.ChangeType(dataContainer.RightGridParent.transform.GetChild(i).GetComponentInChildren<Text>().text, typeof(Value)));
            }

            if (isSortMode)
                tempListInt.Sort();
            else
                tempListInt.Reverse();

            SortIntPanel<Value>(tempListInt, dataContainer);
        }

      //  public static T GetValue<T>(String value)
       // {
        //    return (T)Convert.ChangeType(value, typeof(T));
      //  }

        private static void SortIntPanel<Value>(List<Value> list, SceneDataContainer dataContainer)
        {
            GameObject tempParentGO = new GameObject();
            {
                int childCount = dataContainer.RightGridParent.transform.childCount;

                for (int i = 0; i < childCount; i++)
                {
                    dataContainer.RightGridParent.transform.GetChild(0).SetParent(tempParentGO.transform);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    foreach (Transform item in tempParentGO.transform)
                    {
                        if (item.GetChild(0).GetChild(0).GetComponent<Text>().text == list[i].ToString())
                        {
                            item.SetParent(dataContainer.RightGridParent.transform);
                        }
                    }
                }
            }
            Destroy(tempParentGO);
        }
    }
}