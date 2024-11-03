using System;
using Models;
using DataAccess.DAOs;
using Repository.interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        private PublisherDAO publisherDAO;
        public PublisherRepository(PublisherDAO publisher)
        {
            publisherDAO = publisher;
        }
        public async Task<IEnumerable<Publisher>> getAllPublishers() => await publisherDAO.getAllPublishers();
        public async Task<Publisher> getPublisherById(int publisherId) => await publisherDAO.getPublisherById(publisherId);
        public async Task<(bool, string mess, Publisher)> createPublisher(Publisher publisher) => await publisherDAO.createPublisher(publisher);
        public async Task<(bool, string mess, Publisher)> updatePublisher(Publisher publisher) => await publisherDAO.updatePublisher(publisher);
        public async Task<(bool, string mess)> deletePublisher(int publisherId) => await publisherDAO.deletePublisher(publisherId);
    }
}
