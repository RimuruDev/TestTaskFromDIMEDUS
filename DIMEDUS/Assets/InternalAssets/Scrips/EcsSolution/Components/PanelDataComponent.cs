using UnityEngine.UI;
using UnityEngine;

namespace DIMEDUS.ECS
{
    [System.Serializable]
    public struct PanelDataListComponent
    {
        public PanelDataList panelDataList;
    }

    [System.Serializable]
    public struct HeadersPanelComponent
    {
        public Text[] headerPanelName;
        public Transform[] panelSite;
    }

    [System.Serializable]
    public struct HeadersPanelChildCountComponent
    {
        public Text[] childCountTexts;
        public int[] headerPanelChildCount;
    }
}