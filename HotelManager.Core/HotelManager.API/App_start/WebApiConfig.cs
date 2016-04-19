using AutoMapper;
using HotelManager.Core.Domain;
using HotelManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HotelManager.API.App_start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            CreateMaps();
        }
        public static void CreateMaps()
        {
            Mapper.CreateMap<Room, RoomModel>();
            Mapper.CreateMap<Customer, CustomerModel>();
            Mapper.CreateMap<Reservation, ReservationModel>();
            Mapper.CreateMap<Workorder, WorkorderModel>();
            Mapper.CreateMap<User, UserModel>();
        }
    }
}