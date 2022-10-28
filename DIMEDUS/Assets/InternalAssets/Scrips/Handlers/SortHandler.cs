using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DIMEDUS.RimuruDev
{
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

            SortInt(toggle.isOn);
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

        // TODO: У методов (SortIntPanel and SortStringPanel) слишком много ответственности,
        // необходимо вынести функционал отвечающий за визуализацию данных.
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

        private void SortStringPanel(List<string> list) // TODO: List<string> заменить на List<T>
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
}