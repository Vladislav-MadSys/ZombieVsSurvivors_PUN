using Fusion;
using UnityEngine;

public class MainMenuHUDPresenter
{
    private MainMenuHUDModel _model;
    private MainMenuHUDView _view;
    private FusionBootstrap _fusionBootstrap;
    
    public MainMenuHUDPresenter(MainMenuHUDModel model, MainMenuHUDView view, FusionBootstrap fusionBootstrap)
    {
        _model = model;
        _view = view;
        _fusionBootstrap = fusionBootstrap;
    }

    public void ClickConnectionToRoom()
    {
        _fusionBootstrap.DefaultRoomName = _model.RoomName;
        _fusionBootstrap.StartSharedClient();
    }
    
    public void ChangingRoomName(string roomName)
    {
        _model.RoomName = roomName;
    }
}
