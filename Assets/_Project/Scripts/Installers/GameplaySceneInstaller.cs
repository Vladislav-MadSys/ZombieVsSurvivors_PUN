using _Project.Scripts.Factories;
using _Project.Scripts.NetworkSpawners;
using Zenject;

namespace _Project.Scripts.Installers
{
     public class GameplaySceneInstaller : MonoInstaller
     {
          public override void InstallBindings()
          {
               Container.BindInterfacesTo<GameObjectFactory>().AsSingle().NonLazy();
          }
     }
}
