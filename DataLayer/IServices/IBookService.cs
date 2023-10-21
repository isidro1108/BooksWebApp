using DataLayer.Models;

namespace DataLayer.IServices
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> Create(Book book);
        Task<Book?> GetById(int id);
        Task<Book> Update(Book book);
        Task Delete(int id);
    }
}
