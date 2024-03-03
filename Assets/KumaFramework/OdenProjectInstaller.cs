using OdenGame.Repository;
using Zenject;

public class OdenProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameSettings>().AsSingle();
    }
}