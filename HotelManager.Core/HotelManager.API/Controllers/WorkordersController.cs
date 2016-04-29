using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HotelManager.Core.Domain;
using HotelManager.Data.Infrastructure;
using HotelManager.API.Infrastructure;
using HotelManager.Core.Repository;
using HotelManager.Core.Infranstructure;
using HotelManager.Core.Models;
using AutoMapper;

namespace HotelManager.API.Controllers
{
    [Authorize]
    public class WorkordersController : BaseApiController
    {
        private readonly IWorkorderRepository _workorderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public WorkordersController(IWorkorderRepository workorderRepository, IUnitOfWork unitOfWork, IUserRepository userRepository) : base(userRepository)
        {
            _workorderRepository = workorderRepository;
            _unitOfWork = unitOfWork;
        }
        // GET: api/Workorders
        public IEnumerable<WorkorderModel> GetWorkorders()
        {
            return Mapper.Map<IEnumerable<WorkorderModel>>(_workorderRepository.GetAll());
        }

        // GET: api/Workorders/5
        [ResponseType(typeof(Workorder))]
        public IHttpActionResult GetWorkorder(int id)
        {
            Workorder workorder = _workorderRepository.GetById(id);
            if (workorder == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<WorkorderModel>(workorder));
        }

        // PUT: api/Workorders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorkorder(int id, WorkorderModel workorder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workorder.WorkorderId)
            {
                return BadRequest();
            }

            var dbWorkorder = _workorderRepository.GetById(id);
            dbWorkorder.Update(workorder);
            _workorderRepository.Update(dbWorkorder);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!WorkorderExists(id))
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

        // POST: api/Workorders
        [ResponseType(typeof(Workorder))]
        public IHttpActionResult PostWorkorder(WorkorderModel workorder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbWorkorder = new Workorder(workorder);
            _workorderRepository.Add(dbWorkorder);
            _unitOfWork.Commit();
            workorder.WorkorderId = dbWorkorder.WorkorderId;


            return CreatedAtRoute("DefaultApi", new { id = workorder.WorkorderId }, workorder);
        }

        // DELETE: api/Workorders/5
        [ResponseType(typeof(Workorder))]
        public IHttpActionResult DeleteWorkorder(int id)
        {
            Workorder workorder = _workorderRepository.GetById(id);

            if (workorder == null)
            {
                return NotFound();
            }

            _workorderRepository.Delete(workorder);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<WorkorderModel>(workorder));
        }



        private bool WorkorderExists(int id)
        {
            return _workorderRepository.Any(e => e.WorkorderId == id);
        }

        //Get: Count
        [Route("api/workorders/count")]
        public int GetWorkordersCount()
        {

            return _workorderRepository.Count();
        }

    }
}