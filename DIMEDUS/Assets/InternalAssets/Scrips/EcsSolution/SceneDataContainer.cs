using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DIMEDUS.ECS
{
    public sealed class SceneDataContainer : MonoBehaviour
    {
        [Header("Canvas/Grid")]
        public Transform leftGridSide;
        public Transform rightGridSide;
        [Space]
        public Transform leftPanel;
        public Transform rightPanel;

        [Header("Header panels")]
        public HeaderPanel leftHeaderPanel;
        public HeaderPanel rightHeaderPanel;

        [Header("Data")]
        public PanelDataList panelDataList;
        public PanelDataContainer[] panelDatas = new PanelDataContainer[9]; // TODO: magic number

        [Header("Text")]
        public TextContainer textContainer;
    }

    [System.Serializable]
    public sealed class TextContainer
    {
        public Text[] intPanelsText;
        public Text[] stringPanelsText;
    }
}