using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DIMEDUS.RimuruDev
{
    public sealed class SceneDataContainer : MonoBehaviour
    {
        [SerializeField] private GameObject leftGridParent;
        [SerializeField] private GameObject rightGridParent;

        [Space] public DataList dataList;
        [Space] public TextContainer textContainer;

        [Space] public HeaderPanel headerLeftPanel;
        [Space] public HeaderPanel headerRightPanel;

        [Space] public PanelData[] listElement = new PanelData[9]; // Magic num. Pleace use Start(){ new PanelData[leftGridParent.Length;]}
        [Space] public PanelListData panelListData = new PanelListData();

        public GameObject LeftGridParent { get => leftGridParent; set => leftGridParent = value; }
        public GameObject RightGridParent { get => rightGridParent; set => rightGridParent = value; }
    }
}