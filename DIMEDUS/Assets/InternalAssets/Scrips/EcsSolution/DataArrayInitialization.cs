using DIMEDUS.RimuruDev;
using Leopotam.Ecs;

namespace DIMEDUS.ECS
{
    internal sealed class DataArrayInitialization : IEcsPreInitSystem
    {
        private readonly EcsWorld world = null;
        private readonly EcsFilter<PanelDataListComponent> panelDataLisfFilter = null;
        private readonly EcsFilter<PanelDataContainerComponent> panelDataContainerFilter = null;
        private readonly SceneDataContainer dataContainer = null;

        public void PreInit()
        {
            world.NewEntity().Get<PanelDataListComponent>().panelDataList = new PanelDataList();
            world.NewEntity().Get<PanelDataContainerComponent>().panelDataContainer = new PanelDataContainer[9]; // TODO: Magic number

            // TODO: Use foreach
            ref var panelDataList = ref panelDataLisfFilter.Get1(0).panelDataList;
            ref var panelDataContainer = ref panelDataContainerFilter.Get1(0).panelDataContainer;

            int intTextLength = dataContainer.panelDataList.listInt.Count;
            int stringTextLength = dataContainer.panelDataList.listString.Count;

            panelDataList.listInt = new System.Collections.Generic.List<int>(9); // TODO: Magic number
            panelDataList.listString = new System.Collections.Generic.List<string>(9); // TODO: Magic number

            for (int i = 0; i < intTextLength; i++)
                panelDataList.listInt.Add(dataContainer.panelDataList.listInt[i]);

            for (int i = 0; i < stringTextLength; i++)
                panelDataList.listString.Add(dataContainer.panelDataList.listString[i]);

            for (int i = 0; i < panelDataContainer.Length; i++)
            {
                panelDataContainer[i].panelSide = ((int)PanelSides.Left);
                panelDataContainer[i].text = panelDataList.listString[i];
                panelDataContainer[i].elementNum = panelDataList.listInt[i];
            }

            UnityEngine.Debug.Log("[1/1] DataArrayInitialization()");
        }
    }
}