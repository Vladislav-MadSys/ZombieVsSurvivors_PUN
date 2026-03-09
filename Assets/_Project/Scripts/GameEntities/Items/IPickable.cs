

using Fusion;

namespace _Project.Scripts.GameEntities.Items
{
    public interface IPickable
    {
        public void RPC_PickUp(PlayerAvatar.PlayerAvatar picker);
    }
}
