using UnityEngine;
using Leopotam.Ecs;
using Voody.UniLeo;
using System;
using UnityEngine.PlayerLoop;

namespace DIMEDUS.ECS
{
    public sealed class EcsStartup : MonoBehaviour
    {
        public SceneDataContainer dataContainer;

        private EcsWorld world;
        private EcsSystems systems;

        private void Awake()
        {
            if (dataContainer == null)
                dataContainer = Find<SceneDataContainer>.FastFind(Tag.DataContainer);
        }

        private void Start()
        {
            world = new EcsWorld();
            systems = new EcsSystems(world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(systems);
#endif
            systems.ConvertScene();
            {
                AddInjections();
                AddOneFrames();
                AddSystems();
            }
            systems.Init();
        }

        private void Update() => systems?.Run();

        private void OnDestroy()
        {
            if (systems != null)
            {
                systems.Destroy();
                systems = null;

                world.Destroy();
                world = null;
            }
        }

        private void AddSystems()
        {
            systems

            .Add(new DataArrayInitialization())
            //loading data into UI
               .Add(new FillingTextDataPanels())
               .Add(new SetPanelName())
               .Add(new SetPanelsChildCount()
            );
        }

        private void AddInjections()
        {
            systems.Inject(dataContainer);
        }

        private void AddOneFrames()
        {

        }
    }
}