using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace GtfsService.Models
{ 
    public class TripRepository : ITripRepository
    {
        GtfsServiceContext context = new GtfsServiceContext();

        public IQueryable<Trip> All
        {
            get { return context.Trips; }
        }

        public IQueryable<Trip> AllIncluding(params Expression<Func<Trip, object>>[] includeProperties)
        {
            IQueryable<Trip> query = context.Trips;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Trip Find(int id)
        {
            return context.Trips.Find(id);
        }

        public void InsertOrUpdate(Trip trip)
        {
            if (trip.Id == default(int)) {
                // New entity
                context.Trips.Add(trip);
            } else {
                // Existing entity
                context.Entry(trip).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var trip = context.Trips.Find(id);
            context.Trips.Remove(trip);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface ITripRepository : IDisposable
    {
        IQueryable<Trip> All { get; }
        IQueryable<Trip> AllIncluding(params Expression<Func<Trip, object>>[] includeProperties);
        Trip Find(int id);
        void InsertOrUpdate(Trip trip);
        void Delete(int id);
        void Save();
    }
}