namespace Service.SaveLoad
{
    public interface ISaveLoadService
    {
        bool HasKey(string key);
        void Save<T>(string key, T data);
        T Load<T>(string key);
    }
}