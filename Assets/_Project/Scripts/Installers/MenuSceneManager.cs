using _Project.Scripts.NetworkSpawners;
using Fusion;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class MenuSceneManager : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<NetworkRunner>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<NetworkSceneManagerDefault>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}
