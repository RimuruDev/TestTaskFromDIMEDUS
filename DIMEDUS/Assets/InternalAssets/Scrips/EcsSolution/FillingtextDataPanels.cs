using DIMEDUS.RimuruDev;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace DIMEDUS.ECS
{
    internal sealed class FillingTextDataPanels : IEcsPreInitSystem
    {
        private readonly EcsFilter<PanelDataContainerComponent> panelDataContainerFilter = null;
        private readonly SceneDataContainer dataContainer = null;

        public void PreInit()
        {
            // TODO: Use foreach
            ref var panelDataContainer = ref panelDataContainerFilter.Get1(0).panelDataContainer;

            int intTextLength = dataContainer.panelDataList.listInt.Count;
            int stringTextLength = dataContainer.panelDataList.listString.Count;

            for (int i = 0; i < intTextLength; i++)
                dataContainer.textContainer.intPanelsText[i].text = panelDataContainer[i].elementNum.ToString();

            for (int i = 0; i < stringTextLength; i++)
                dataContainer.textContainer.stringPanelsText[i].text = panelDataContainer[i].text;

            UnityEngine.Debug.Log("[1/1] InitialDataPadding.PreInit()");
        }

        public void HeadersElementCounterUpdate()
        {
            dataContainer.leftHeaderPanel.elementCounterText.text = $"Count: {LeftPanelElementCounter()}";
            dataContainer.rightHeaderPanel.elementCounterText.text = $"Count: {RightPanelElementCounter()}";
        }

        public void SetHeadersNamePanel()
        {
            dataContainer.leftHeaderPanel.panelNameText.text = $"{dataContainer.leftPanel.name}";
            dataContainer.rightHeaderPanel.panelNameText.text = $"{dataContainer.rightPanel.name}";
        }

        public void FillPanelString()
        {
            int stringTextLength = dataContainer.textContainer.stringPanelsText.Length;

            //    for (int i = 0; i < stringTextLength; i++)
            //                dataContainer.textContainer.stringPanelsText[i].text = dataContainer.panelDatas[i].text;
        }

        public void FillPanelInt()
        {
            int intTextLength = dataContainer.textContainer.intPanelsText.Length;

            // for (int i = 0; i < intTextLength; i++)
            //     dataContainer.textContainer.intPanelsText[i].text = dataContainer.panelDatas[i].elementNum.ToString();
        }

        private int LeftPanelElementCounter() => dataContainer.leftGridSide.transform.childCount;

        private int RightPanelElementCounter() => dataContainer.rightGridSide.transform.childCount;
    }
}