using _Project.Scripts.Low.Input;
using _Project.Scripts.Low.ScenesController;
using Zenject;

namespace _Project.Scripts.Installers
{
       public class ProjectContextInstaller : MonoInstaller
       {
              public static DiContainer DiContainer { get; private set; }
              
              public override void InstallBindings()
              {
                     DiContainer = Container;
                     Container.BindInterfacesTo<ScenesController>().AsSingle().NonLazy();
                     Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle().NonLazy();
                     Container.Bind<PlayerInput>().AsSingle().NonLazy();
              }
       }
}
