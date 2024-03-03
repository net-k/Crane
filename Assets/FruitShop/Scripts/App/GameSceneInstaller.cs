using FruitShop.Domain;
using OdenGame.Domain;
using Zenject;

namespace FruitShop.App
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Player>().AsSingle();
            Container.Bind<BonusCalculator>().AsSingle();
            Container.Bind<ItemUsageInformation>().AsSingle();
        }
    }
}