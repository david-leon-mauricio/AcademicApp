namespace AcademicApp.Storage
{
    public interface ICache
    {
        bool Contains(string key);
        bool Add(string key, object value);
        void Set(string key, object value);
        T Get<T>(string key);
        bool Remove(string key);
    }
}
