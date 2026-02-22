using _Project.Scripts.Factories;
using _Project.Scripts.NetworkSpawners;
using _Project.Scripts.Session;
using Zenject;
using Zenject.SpaceFighter;

namespace _Project.Scripts.Installers
{
     public class GameplaySceneInstaller : MonoInstaller
     {
          public override void InstallBindings()
          {
               Container.BindInterfacesTo<GameObjectFactory>().AsSingle().NonLazy();
               Container.BindInterfacesTo<RoomSessionData>().FromComponentsInHierarchy().AsSingle().NonLazy();
          }
     }
}
