using System;
using System.Collections;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace DIMEDUS.RimuruDev
{
    public sealed class SaveAndLoadSystem : MonoBehaviour
    {
        private SceneDataContainer dataContainer = null;
        private UIController UIController = null;

        private Transform leftGrid = null;
        private Transform rightGrid = null;
        private string path = string.Empty;

        private void Awake()
        {
            dataContainer = FindObjectOfType<SceneDataContainer>();
            UIController = FindObjectOfType<UIController>();
        }

        private void Start()
        {
            leftGrid = dataContainer.LeftGridParent.transform;
            rightGrid = dataContainer.RightGridParent.transform;

            InitData();

            path = $"{Application.streamingAssetsPath}/DataJson.json";
        }

        private void InitData()// TODO: Move it to a separate method.
        {
            for (int i = 0; i < dataContainer.listElement.Length; i++)
            {
                dataContainer.listElement[i].panelSide = 0;
                dataContainer.listElement[i].text = dataContainer.dataList.listString[i];
                dataContainer.listElement[i].elementNum = dataContainer.dataList.listInt[i];
            }
        }

        public void LoadToJson()
        {
            // Parse json
            string json = string.Empty;
            {
                if (File.Exists(path))
                    json = File.ReadAllText(path);
                else
                    using (File.Create(path))
                        json = File.ReadAllText(path);
            }
            PanelListData data = JsonUtility.FromJson<PanelListData>(json);

            // Move all panel elements to the left // TODO: Move it to a separate method.
            int rightChildCount = rightGrid.childCount;
            for (int i = 0; i < rightChildCount; i++)
            {
                rightGrid.GetChild(0).SetParent(leftGrid);
            }

            // Load data
            for (int i = 0; i < dataContainer.listElement.Length; i++)
            {
                dataContainer.listElement[i].text = data.panelDatas[i].text;
                dataContainer.listElement[i].panelSide = data.panelDatas[i].panelSide;
                dataContainer.listElement[i].elementNum = data.panelDatas[i].elementNum;
            }

            // Data filling  // TODO: Move it to a separate method + cache coll GetComponentInChildren
            for (int i = 0; i < leftGrid.childCount; i++)
            {
                leftGrid.GetChild(i).GetChild(0).GetComponentInChildren<Text>().text = dataContainer.listElement[i].elementNum.ToString();
                leftGrid.GetChild(i).GetChild(1).GetComponentInChildren<Text>().text = dataContainer.listElement[i].text;
            }

            // Transferring Saved Left Side Panels. // TODO: Move it to a separate method.
            int hitPosition = 0;
            {
                for (int i = 0; i < dataContainer.listElement.Length; i++)
                {
                    if (dataContainer.listElement[i].panelSide == ((int)PanelSides.Right))
                    {
                        hitPosition = i;

                        break;
                    }
                }

                for (int i = hitPosition; i < dataContainer.listElement.Length; i++)
                {
                    leftGrid.GetChild(hitPosition).SetParent(rightGrid);
                }
            }

            UIController.HeadersElementCounterUpdate();
        }

        public void SaveToJson()
        {
            if (dataContainer.panelListData.panelDatas.Count > dataContainer.listElement.Length)
                dataContainer.panelListData.panelDatas.Clear();

            for (int i = 0; i < leftGrid.childCount; i++)
            {
                dataContainer.listElement[i].panelSide = ((int)PanelSides.Left);
                dataContainer.listElement[i].text = leftGrid.GetChild(i).GetChild(1).GetComponentInChildren<Text>().text;
                dataContainer.listElement[i].elementNum = int.Parse(leftGrid.GetChild(i).GetChild(0).GetComponentInChildren<Text>().text);
            }

            for (int i = 0; i < rightGrid.childCount; i++)
            {
                dataContainer.listElement[i + leftGrid.childCount].panelSide = ((int)PanelSides.Right);
                dataContainer.listElement[i + leftGrid.childCount].text = rightGrid.GetChild(i).GetChild(1).GetComponentInChildren<Text>().text;
                dataContainer.listElement[i + leftGrid.childCount].elementNum = int.Parse(rightGrid.GetChild(i).GetChild(0).GetComponentInChildren<Text>().text);
            }

            for (int i = 0; i < dataContainer.listElement.Length; i++)
            {
                dataContainer.panelListData.panelDatas.Add(dataContainer.listElement[i]);
            }

            if (File.Exists(path))
                JSONUtility.JSONWriter.Write("DataJson", dataContainer.panelListData);
            else
                using (File.Create(path))
                    JSONUtility.JSONWriter.Write("DataJson", dataContainer.panelListData);
        }
    }

    public enum PanelSides { Left = 0, Right = 1 }
}