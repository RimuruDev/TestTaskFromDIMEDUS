using DIMEDUS.RimuruDev;
using Leopotam.Ecs;

namespace DIMEDUS.ECS
{
    internal sealed class SetPanelName : IEcsInitSystem
    {
        private readonly EcsWorld world = null;
        private readonly SceneDataContainer dataContainer = null;

        public void Init()
        {
            ref var headerPanels = ref world.NewEntity().Get<HeadersPanelComponent>();

            headerPanels.headerPanelName = new UnityEngine.UI.Text[((int)PanelSides.BothSides)];
            headerPanels.panelSite = new UnityEngine.Transform[((int)PanelSides.BothSides)];

            headerPanels.headerPanelName[(int)PanelSides.Left] = dataContainer.leftHeaderPanel.panelNameText;
            headerPanels.headerPanelName[(int)PanelSides.Right] = dataContainer.rightHeaderPanel.panelNameText;

            headerPanels.panelSite[(int)PanelSides.Left] = dataContainer.leftPanel;
            headerPanels.panelSite[(int)PanelSides.Right] = dataContainer.rightPanel;

            headerPanels.headerPanelName[(int)PanelSides.Left].text = headerPanels.panelSite[(int)PanelSides.Left].name;
            headerPanels.headerPanelName[(int)PanelSides.Right].text = headerPanels.panelSite[(int)PanelSides.Right].name;
        }
    }
}