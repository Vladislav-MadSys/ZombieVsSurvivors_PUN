using _Project.Scripts.Low;
using _Project.Scripts.Low.ScenesController;
using Zenject;

namespace _Project.Scripts.Installers
{
       public class ProjectContextInstaller : MonoInstaller
       {
              public override void InstallBindings()
              {
                     Container.BindInterfacesTo<ScenesController>().AsSingle().NonLazy();
              }
       }
}
