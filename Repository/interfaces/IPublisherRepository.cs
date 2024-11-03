using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.interfaces
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<Publisher>> getAllPublishers();
        Task<Publisher> getPublisherById(int publisherId);
        Task<(bool, string mess, Publisher)> createPublisher(Publisher publisher);
        Task<(bool, string mess, Publisher)> updatePublisher(Publisher publisher);
        Task<(bool, string mess)> deletePublisher(int publisherId);
    }
}
