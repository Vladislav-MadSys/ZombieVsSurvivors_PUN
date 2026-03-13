using System;
using Fusion;
using UnityEngine;
using Zenject;

public class MainMenuHUDFactory : IInitializable, IDisposable
{
    private FusionBootstrap _fusionBootstrap;
    private MainMenuHUDView _view;
    
    public MainMenuHUDFactory(FusionBootstrap fusionBootstrap, MainMenuHUDView view)
    {
        _fusionBootstrap = fusionBootstrap;
        _view = view;
    }

    public void Initialize()
    {
        MainMenuHUDModel model = new MainMenuHUDModel();
        MainMenuHUDPresenter presenter = new MainMenuHUDPresenter(model, _view, _fusionBootstrap);
        _view.Initialize(presenter);
        _view.Run();
    }

    public void Dispose()
    {
        
    }
}
