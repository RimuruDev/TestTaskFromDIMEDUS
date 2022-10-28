using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DIMEDUS.RimuruDev
{
    public sealed class UIController : MonoBehaviour
    {
        private SceneDataContainer dataContainer = null;
        private Canvas mainCanvas = null;

        private void Awake()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<SceneDataContainer>();

            mainCanvas = FindObjectOfType<Canvas>();
        }

        private void Start()
        {
            HeadersElementCounterUpdate();

            FillingPaneTexts();

            SetHeadersNamePanel();
        }

        public void HeadersElementCounterUpdate()
        {
            dataContainer.headerLeftPanel.elementCounterText.text = $"Count: {LeftPanelElementCounter()}";
            dataContainer.headerRightPanel.elementCounterText.text = $"Count: {RightPanelElementCounter()}";
        }

        private void FillingPaneTexts()
        {
            for (int i = 0; i < dataContainer.LeftGridParent.transform.childCount; i++)
            {
                dataContainer.LeftGridParent.transform.GetChild(i).GetComponentInChildren<Text>().text = $"{dataContainer.dataList.listInt[i]}";

                dataContainer.textContainer.stringPanelsText[i].text = $"{dataContainer.dataList.listString[i]}";
            }
        }

        public void SetHeadersNamePanel()
        {
            dataContainer.headerLeftPanel.panelNameText.text = $"{mainCanvas.transform.GetChild(0).name}";
            dataContainer.headerRightPanel.panelNameText.text = $"{mainCanvas.transform.GetChild(1).name}";
        }

        private int LeftPanelElementCounter() => dataContainer.LeftGridParent.transform.childCount;

        private int RightPanelElementCounter() => dataContainer.RightGridParent.transform.childCount;
    }
}