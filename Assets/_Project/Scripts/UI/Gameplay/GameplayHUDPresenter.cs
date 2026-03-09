using UnityEngine;

namespace _Project.Scripts.UI.Gameplay
{
    public class GameplayHUDPresenter
    {
        private GameplayHUDModel _model;
        private GameplayHUDView _view;
        
        public GameplayHUDPresenter(GameplayHUDModel model, GameplayHUDView view)
        {
            _model = model;
            _view = view;
        }

        public void Run()
        {
            _model.OnPlayerHpChanged += OnPlayerHPChanged;
        }

        public void Dispose()
        {
            _model.OnPlayerHpChanged -= OnPlayerHPChanged;
        }

        private void OnPlayerHPChanged(float currentHP, float maxHP)
        {
            float percentage = currentHP / maxHP;
            _view.ChangeHpBar(percentage);
        }
    }
}
