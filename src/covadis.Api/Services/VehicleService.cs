using covadis.Api.Context;
using covadis.Api.Models;
using covadis.Shared.Requests;
using covadis.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace covadis.Api.Services
{
    public class VehicleService
    {
        private readonly DbContextCovadis dbContext;

        public VehicleService(DbContextCovadis dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<VehicleResponse> GetVehicles()
        {
            return dbContext.Vehicles
                .Select(v => new VehicleResponse
                {
                    Id = v.Id,
                    LicensePlate = v.LicensePlate,
                    Odometer = v.Odometer,
                    Brand = v.Brand,
                    Model = v.Model,
                    ManufacturedDate = v.ManufacturedDate
                })
                .ToList();
        }

        public VehicleResponse GetVehicleById(int id)
        {
            var vehicle = dbContext.Vehicles.Find(id);

            if (vehicle == null)
                return null;

            return new VehicleResponse
            {
                Id = vehicle.Id,
                LicensePlate = vehicle.LicensePlate,
                Odometer = vehicle.Odometer,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                ManufacturedDate = vehicle.ManufacturedDate
            };
        }

        public VehicleResponse CreateVehicle(CreateVehicleRequest request)
        {
            var newVehicle = new Vehicle
            {
                LicensePlate = request.LicensePlate,
                Odometer = request.Odometer,
                Brand = request.Brand,
                Model = request.Model,
                ManufacturedDate = request.ManufacturedDate
            };

            dbContext.Vehicles.Add(newVehicle);
            dbContext.SaveChanges();

            return new VehicleResponse
            {
                Id = newVehicle.Id,
                LicensePlate = newVehicle.LicensePlate,
                Odometer = newVehicle.Odometer,
                Brand = newVehicle.Brand,
                Model = newVehicle.Model,
                ManufacturedDate = newVehicle.ManufacturedDate
            };
        }

        public bool UpdateVehicle(int id, UpdateVehicleRequest request)
        {
            var existingVehicle = dbContext.Vehicles.Find(id);

            if (existingVehicle == null)
                return false;

            existingVehicle.LicensePlate = request.LicensePlate;
            existingVehicle.Odometer = request.Odometer;
            existingVehicle.Brand = request.Brand;
            existingVehicle.Model = request.Model;
            existingVehicle.ManufacturedDate = (DateOnly)request.ManufacturedDate;

            dbContext.SaveChanges();
            return true;
        }

        public bool DeleteVehicle(int id)
        {
            var vehicle = dbContext.Vehicles.Find(id);

            if (vehicle == null)
                return false;

            dbContext.Vehicles.Remove(vehicle);
            dbContext.SaveChanges();
            return true;
        }
    }
}
