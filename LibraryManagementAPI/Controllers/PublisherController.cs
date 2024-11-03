using Microsoft.AspNetCore.Mvc;
using Models;
using Repository.interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : Controller
    {
        private readonly IPublisherRepository _publisherRepository;
        public PublisherController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> getAllPublishers()
        {
            var publisher = await _publisherRepository.getAllPublishers();
            return Ok(publisher);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> getPublisherById(int id)
        {
            var publisher = await _publisherRepository.getPublisherById(id);
            return Ok(publisher);
        }

        [HttpPost]
        public async Task<ActionResult> createPublisher(Publisher publisher)
        {
            await _publisherRepository.createPublisher(publisher);
            return CreatedAtAction(nameof (getPublisherById), new { id = publisher.PublisherId }, publisher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updatePublisher(int id, Publisher publisher)
        {
            if (id != publisher.PublisherId)
            {
                return BadRequest();
            }
            var (success, message, p) = await _publisherRepository.updatePublisher(publisher);
            return Ok(new { message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deletePublisher(int id)
        {
            var (success, message) = await _publisherRepository.deletePublisher(id);
            return Ok(new { message });
        }
    }
}
