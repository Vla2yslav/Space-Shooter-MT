using Newtonsoft.Json;
using UnityEngine;

namespace Service.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        public bool HasKey(string key) => PlayerPrefs.HasKey(key);
        public void Save<T>(string key, T data) => PlayerPrefs.SetString(key, JsonConvert.SerializeObject(data));
        public T Load<T>(string key) => JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(key));
    }
}