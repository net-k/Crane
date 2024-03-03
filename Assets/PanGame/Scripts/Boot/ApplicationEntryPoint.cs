using App;

namespace Quiz.Application
{
    public class ApplicationEntryPoint : SingletonMonoBehaviour<ApplicationEntryPoint>
    {
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            SaveDataManager.Instance.Initialize();
        }
    }
}
