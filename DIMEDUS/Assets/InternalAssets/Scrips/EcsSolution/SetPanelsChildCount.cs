using DIMEDUS.RimuruDev;
using Leopotam.Ecs;

namespace DIMEDUS.ECS
{
    internal sealed class SetPanelsChildCount : IEcsInitSystem
    {
        private readonly EcsWorld world = null;
        private readonly SceneDataContainer dataContainer = null;

        public void Init()
        {
            ref var headerPanel = ref world.NewEntity().Get<HeadersPanelChildCountComponent>();
            {
                headerPanel.headerPanelChildCount = new int[((int)PanelSides.BothSides)];
                headerPanel.childCountTexts = new UnityEngine.UI.Text[((int)PanelSides.BothSides)];

                headerPanel.headerPanelChildCount[(int)PanelSides.Left] = dataContainer.leftGridSide.transform.childCount;
                headerPanel.headerPanelChildCount[(int)PanelSides.Right] = dataContainer.rightGridSide.transform.childCount;

                headerPanel.childCountTexts[(int)PanelSides.Left] = dataContainer.leftHeaderPanel.elementCounterText;
                headerPanel.childCountTexts[(int)PanelSides.Right] = dataContainer.rightHeaderPanel.elementCounterText;
            }

            headerPanel.childCountTexts[(int)PanelSides.Left].text = $"Count: {headerPanel.headerPanelChildCount[(int)PanelSides.Left]}";
            headerPanel.childCountTexts[(int)PanelSides.Right].text = $"Count: {headerPanel.headerPanelChildCount[(int)PanelSides.Right]}";
        }
    }
}