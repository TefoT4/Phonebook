using Phonebook.Domains;
using Phonebook.Services;
using Microsoft.AspNetCore.Mvc;

namespace Phonebook.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhonebookController : Controller
    {
        private readonly IPhonebookService _phonebookService;

        public PhonebookController(IPhonebookService phonebookService)
        {
            _phonebookService = phonebookService;
        }

        /// <summary>
        /// Get all Phone Books contacts
        /// </summary>
        /// <returns>List of phone book contacts</returns>
        // GET api/phonebook
        [HttpGet]
        public ActionResult Contacts()
        {
            var serviceResponse = _phonebookService.GetContacts();
            return Ok(serviceResponse.Entity);
        }

        // <summary>
        /// Get Phone book contact by id
        /// </summary>
        /// <param name="id"> Specified id for contact </param>
        /// <returns> contact </returns>
        /// GET api/phonebook/1
        [HttpGet("{id}")]
        public ActionResult Contact(int id)
        {
            var serviceResponse = _phonebookService.GetContactById(id);
            return serviceResponse.IsSuccessful ? Ok(serviceResponse.Entity) : NotFound(id);
        }

        /// <summary>
        /// Update Phone book contact
        /// </summary>
        /// <param name="id"> Phone Book contact id </param>
        /// <param name="contact"> Phone Book Contact Entity </param>
        /// <returns> The update phone book contact entity </returns>
        /// PUT api/phonebook/2
        [HttpPost]
        public ActionResult Post(Contact contact)
        {
            var serviceResponse = _phonebookService.CreateContact(contact);
            return serviceResponse.IsSuccessful ? Ok(serviceResponse.Entity) : BadRequest(serviceResponse.Errors);
        }

        /// <summary>
        /// Update Phone book contact
        /// </summary>
        /// <param name="id"> Phone Book contact id </param>
        /// <param name="contact"> Phone Book Contact Entity </param>
        /// <returns> The update phone book contact entity </returns>
        /// PUT api/phonebook/2
        [HttpPut("{id}")]
        public ActionResult Put(int id, Contact contact)
        {
            var serviceResponse = _phonebookService.EditContact(id, contact);
            return serviceResponse.IsSuccessful ? Ok(serviceResponse.Entity) : BadRequest(serviceResponse.Errors);
        }

        // <summary>
        /// Delete Phone Book contact
        /// </summary>
        /// <param name="id"> Phone Book Contact Id </param>
        /// <returns> True if deleted </returns>
        // DELETE api/phonebook/3
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var serviceResponse = _phonebookService.DeleteContactById(id);
            return serviceResponse.IsSuccessful;
        }
    }
}
