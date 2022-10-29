using System;
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

        public void SortInt(Toggle toggle) => Sort<int>(toggle.isOn);

        public void SortString(Toggle toggle) => Sort<string>(toggle.isOn);

        private void Sort<Value>(bool isSortMode)
        {
            List<Value> tempList = new List<Value>();
            int childCount = dataContainer.RightGridParent.transform.childCount;

            // Temp solution.
            {
                if (typeof(Value).Equals(typeof(string)))
                    for (int i = 0; i < childCount; i++)
                        tempList.Add((Value)Convert.ChangeType(dataContainer.RightGridParent.transform.GetChild(i).GetChild(1).GetChild(0).GetComponentInChildren<Text>().text, typeof(Value)));

                if (typeof(Value).Equals(typeof(int)))
                    for (int i = 0; i < childCount; i++)
                        tempList.Add((Value)Convert.ChangeType(dataContainer.RightGridParent.transform.GetChild(i).GetComponentInChildren<Text>().text, typeof(Value)));
            }

            if (isSortMode)
                tempList.Sort();
            else
                tempList.Reverse();

            SortPanel<Value>(tempList);
        }

        private void SortPanel<Value>(List<Value> list)
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
                    // Temp solution.
                    {
                        if (typeof(Value).Equals(typeof(string)))
                        {
                            foreach (Transform item in tempParentGO.transform)
                                if (item.GetChild(1).GetChild(0).GetComponent<Text>().text == list[i].ToString())
                                    item.SetParent(dataContainer.RightGridParent.transform);
                        }

                        if (typeof(Value).Equals(typeof(int)))
                        {
                            foreach (Transform item in tempParentGO.transform)
                                if (item.GetChild(0).GetChild(0).GetComponent<Text>().text == list[i].ToString())
                                    item.SetParent(dataContainer.RightGridParent.transform);
                        }
                    }
                }
            }
            Destroy(tempParentGO);
        }
    }
}