using FruitShop.MasterData;
using OdenGame.Domain;
using OdenGame.Repository;
using Zenject;

namespace FruitShop.App
{
    public class ShopSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ItemUsageInformation>().AsSingle();
            Container.Bind<Player>().AsSingle();
            Container.Bind<ShopLevelMaster>().AsSingle();
            Container.Bind<ShopUseCase>().AsSingle();
            Container.Bind<PlayerData>().AsSingle();
            Container.Bind<ShopModel>().AsSingle();
        }
    }
}