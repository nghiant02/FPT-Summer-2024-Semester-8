using BusinessObject;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Tools;

namespace Dao;

public class PublisherDao
{
    private readonly AppDbContext _context;
    public PublisherDao()
    {
        _context = new AppDbContext();
    }
    public async Task<Publisher> CreatePublisher(Publisher publisher)
    {
        if (publisher == null || _context == null) throw new ArgumentNullException(nameof(publisher));
        publisher.PubId = Generator.GenerateId();
        await _context.Publishers.AddAsync(publisher);
        await _context.SaveChangesAsync();
        return publisher;
    }
    public async Task<IEnumerable<Publisher>> GetPublishers()
    {
        if (_context == null) throw new ArgumentNullException(nameof(_context));
        return await _context.Publishers.ToListAsync();
    }
    public async Task<Publisher> GetPublisherById(string id)
    {
        if (_context == null || id == null) throw new ArgumentNullException(nameof(id));
        var publisher = await _context.Publishers.FirstOrDefaultAsync(p=>p.PubId == id);
        if (publisher == null) throw new ArgumentNullException(nameof(publisher));
        return publisher;
    }
    public async Task<bool> DeletePublisher(string id)
    {
        if (string.IsNullOrEmpty(id) || _context == null) throw new ArgumentNullException(nameof(id));
        var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.PubId == id);
        if (publisher == null) return false;
        _context.Publishers.Remove(publisher);
        var result = await _context.SaveChangesAsync();
        if(result == 0) return false;
        return true;
    }
    public async Task<bool> UpdatePubliser(string id, Publisher publisher)
    {
        if (string.IsNullOrEmpty(id) || publisher == null || _context == null) throw new ArgumentNullException(nameof(id));
        var publisherInDb = await _context.Publishers.FirstOrDefaultAsync(p=>p.PubId == id);
        if(publisherInDb == null) return false;
        publisherInDb.PublisherName = publisher.PublisherName;
        publisherInDb.State = publisher.State; 
        publisherInDb.City = publisher.City;
        publisherInDb.Country = publisher.Country;
        _context.Publishers.Update(publisherInDb);
        var result = await _context.SaveChangesAsync();
        if(result == 0) return false;
        return true;
    }
}
