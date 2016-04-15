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
using HotelManager.Core.Repository;
using HotelManager.Core.Infranstructure;
using HotelManager.API.Infrastructure;
using AutoMapper;
using HotelManager.Core.Models;

namespace HotelManager.API.Controllers
{
    [Authorize]
    public class RoomsController : BaseApiController
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoomsController(IRoomRepository roomRepository, IUnitOfWork unitOfWork, IUserRepository userRepository) : base(userRepository)
        {
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
        }
        // GET: api/Rooms
        public IEnumerable<RoomModel> GetRooms()
        {
            return Mapper.Map<IEnumerable<RoomModel>>(_roomRepository.GetAll());
        }

        // GET: api/Rooms/5
        [ResponseType(typeof(Room))]
        public IHttpActionResult GetRoom(int id)
        {
            Room room = _roomRepository.GetById(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<RoomModel>(room));
        }

        // PUT: api/Rooms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoom(int id, RoomModel room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.RoomId)
            {
                return BadRequest();
            }

            var dbRoom = _roomRepository.GetById(id);
            dbRoom.Update(room);
            _roomRepository.Update(dbRoom);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!RoomExists(id))
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

        // POST: api/Rooms
        [ResponseType(typeof(Room))]
        public IHttpActionResult PostRoom(RoomModel room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbRoom = new Room(room);
            _roomRepository.Add(dbRoom);
            _unitOfWork.Commit();
            room.RoomId = dbRoom.RoomId;


            return CreatedAtRoute("DefaultApi", new { id = room.RoomId }, room);
        }

        // DELETE: api/Rooms/5
        [ResponseType(typeof(Room))]
        public IHttpActionResult DeleteRoom(int id)
        {
            Room room = _roomRepository.GetById(id);

            if (room == null)
            {
                return NotFound();
            }

            _roomRepository.Delete(room);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<RoomModel>(room));
        }



        private bool RoomExists(int id)
        {
            return _roomRepository.Any(e => e.RoomId == id);
        }

        //Get: Count
        [Route("api/rooms/count")]
        public int GetRoomsCount()
        {

            return _roomRepository.Count();
        }

    }
}