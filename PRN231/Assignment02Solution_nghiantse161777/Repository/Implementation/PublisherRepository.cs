using BusinessObject.Models;
using Dao;
using Repository.Interface;

namespace Repository.Implementation;

public class PublisherRepository : IPublisherRepository
{
    private readonly PublisherDao _publisherDao;
    public PublisherRepository()
    {
        _publisherDao = new PublisherDao();
    }
    public async Task<Publisher> Create(Publisher entity)
    {
        return await _publisherDao.CreatePublisher(entity);
    }

    public async Task<int> Delete(string id)
    {
        return await _publisherDao.DeletePublisher(id) ? 1 : 0;
    }

    public async Task<Publisher?> GetById(string id)
    {
        return await _publisherDao.GetPublisherById(id);
    }

    public async Task<IEnumerable<Publisher>?> Gets()
    {
        return await _publisherDao.GetPublishers();
    }

    public Task<bool> Update(string id, Publisher entity)
    {
        return _publisherDao.UpdatePubliser(id, entity);
    }
}
