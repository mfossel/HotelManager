using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using HotelManager.Core.Domain;
using HotelManager.API.Infrastructure;
using HotelManager.Core.Repository;
using HotelManager.Core.Infranstructure;
using HotelManager.Core.Models;
using AutoMapper;

namespace HotelManager.API.Controllers
{
    [Authorize]
    public class ReservationsController : BaseApiController
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReservationsController(IReservationRepository reservationRepository, IUnitOfWork unitOfWork, IUserRepository userRepository) : base(userRepository)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
        }
        // GET: api/Reservations
        public IEnumerable<ReservationModel> GetReservations()
        {
            return Mapper.Map<IEnumerable<ReservationModel>>(_reservationRepository.GetAll());
        }

        // GET: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult GetReservation(int id)
        {
            Reservation reservation = _reservationRepository.GetById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ReservationModel>(reservation));
        }

        // PUT: api/Reservations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReservation(int id, ReservationModel reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservation.ReservationId)
            {
                return BadRequest();
            }

            var dbReservation = _reservationRepository.GetById(id);
            dbReservation.Update(reservation);
            _reservationRepository.Update(dbReservation);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!ReservationExists(id))
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

        // POST: api/Reservations
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult PostReservation(ReservationModel reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbReservation = new Reservation(reservation);
            _reservationRepository.Add(dbReservation);
            _unitOfWork.Commit();
            reservation.ReservationId = dbReservation.ReservationId;


            return CreatedAtRoute("DefaultApi", new { id = reservation.ReservationId }, reservation);
        }

        // DELETE: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult DeleteReservation(int id)
        {
            Reservation reservation = _reservationRepository.GetById(id);

            if (reservation == null)
            {
                return NotFound();
            }

            _reservationRepository.Delete(reservation);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<ReservationModel>(reservation));
        }



        private bool ReservationExists(int id)
        {
            return _reservationRepository.Any(e => e.ReservationId == id);
        }

        //Get: Count
        [Route("api/reservations/count")]
        public int GetReservationsCount()
        {

            return _reservationRepository.Count();
        }

        //LINQ Queries

        // GET: api/Reservations/recent
        [Route("api/reviews/recent")]
        public IHttpActionResult GetLatestReview()
        {
            IEnumerable<Reservation> recents = _reservationRepository.GetAll().OrderBy(r => r.ReservationId).Take(5);

            return Ok(Mapper.Map<IEnumerable<ReservationModel>>(recents));
        }





    }
}