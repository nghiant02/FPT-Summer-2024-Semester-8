using BusinessObject;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace Dao
{
    public class AuthorDao
    {
        private readonly AppDbContext? _context;

        public AuthorDao()
        {
            _context = new AppDbContext();
        }
        public async Task<Author> AddAuthor(Author author)
        {
            if (author == null || _context == null) throw new ArgumentNullException(nameof(author));
            author.AuthorId = Generator.GenerateId();
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return author;
        }
        public async Task<Author?> GetAuthorById(string authorId)
        {
            if (string.IsNullOrEmpty(authorId) || _context == null) throw new ArgumentNullException(nameof(authorId));
            return await _context.Authors.FindAsync(authorId);
        }
        public async Task<List<Author>?> GetAuthors()
        {
            if (_context == null) throw new ArgumentNullException(nameof(_context));
            return await _context.Authors.ToListAsync();
        }
        public async Task<int> DeleteAuthor(string authorId)
        {
            if (string.IsNullOrEmpty(authorId) || _context == null) throw new ArgumentNullException(nameof(authorId));
            var author = await _context.Authors.FindAsync(authorId);
            if (author == null) return 0;
            _context.Authors.Remove(author);
            return await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateAuthor(string id, Author author)
        {
            if (string.IsNullOrEmpty(id) || author == null || _context == null) throw new ArgumentNullException(nameof(id));
            var authorInDb = await _context.Authors.FindAsync(id);
            if (authorInDb == null) return false;
            authorInDb.Phone = author.Phone;
            authorInDb.Zip = author.Zip;
            authorInDb.State = author.State;
            authorInDb.City = author.City;
            authorInDb.Address = author.Address;
            authorInDb.EmailAddress = author.EmailAddress;
            authorInDb.LastName = author.LastName;
            authorInDb.FirstName = author.FirstName;
            authorInDb.MiddelName = author.MiddelName;

            _context.Authors.Update(authorInDb);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
