
namespace MobileTVLibrary.Repositories
{
    public interface IRepository<T>
    {
        bool Delete(int id);
        T Find(int id);
        T Insert(T show);
        void Update(T show);
    }
}
