using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using HotelManager.Core.Domain;
using HotelManager.Core.Repository;
using HotelManager.Core.Infranstructure;
using AutoMapper;
using HotelManager.Core.Models;
using HotelManager.API.Infrastructure;

namespace HotelManager.API.Controllers
{
    [Authorize]
    public class CustomersController : BaseApiController
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomersController(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IUserRepository userRepository) : base(userRepository)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }
        // GET: api/Customers
        public IEnumerable<CustomerModel> GetCustomers()
        {
            return Mapper.Map<IEnumerable<CustomerModel>>(_customerRepository.GetAll());
        }

        // GET: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = _customerRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<CustomerModel>(customer));
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, CustomerModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            var dbCustomer = _customerRepository.GetById(id);
            dbCustomer.Update(customer);
            _customerRepository.Update(dbCustomer);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Customers
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(CustomerModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbCustomer = new Customer(customer);
            _customerRepository.Add(dbCustomer);
            _unitOfWork.Commit();
            customer.CustomerId = dbCustomer.CustomerId;


            return CreatedAtRoute("DefaultApi", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = _customerRepository.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            _customerRepository.Delete(customer);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<CustomerModel>(customer));
        }



        private bool CustomerExists(int id)
        {
            return _customerRepository.Any(e => e.CustomerId == id);
        }

        //Get: Count
        [Route("api/customers/count")]
        public int GetCustomersCount()
        {

            return _customerRepository.Count();
        }

    }
}