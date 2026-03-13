using _Project.Scripts.NetworkSpawners;
using Fusion;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class MenuSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<NetworkRunner>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<NetworkSceneManagerDefault>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<FusionBootstrap>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MainMenuHUDFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MainMenuHUDView>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}
