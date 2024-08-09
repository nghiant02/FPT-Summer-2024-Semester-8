using BusinessObject.Models;
using Dao;
using Repository.Interface;

namespace Repository.Implementation
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AuthorDao _authorDao;
        public AuthorRepository()
        {
            _authorDao = new AuthorDao();
        }
        public async Task<Author> Create(Author entity)
        {
            return await _authorDao.AddAuthor(entity);
        }

        public async Task<int> Delete(string id)
        {
            return await _authorDao.DeleteAuthor(id);
        }

        public async Task<Author?> GetById(string id)
        {
            return await _authorDao.GetAuthorById(id);
        }

        public async Task<IEnumerable<Author>?> Gets()
        {
            return await _authorDao.GetAuthors();
        }

        public async Task<bool> Update(string id, Author entity)
        {
            return await _authorDao.UpdateAuthor(id, entity);
        }
    }
}
