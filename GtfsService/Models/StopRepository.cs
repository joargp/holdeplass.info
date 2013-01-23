using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace GtfsService.Models
{ 
    public class StopRepository : IStopRepository
    {
        GtfsServiceContext context = new GtfsServiceContext();

        public IQueryable<Stop> All
        {
            get { return context.Stops; }
        }

        public IQueryable<Stop> AllIncluding(params Expression<Func<Stop, object>>[] includeProperties)
        {
            IQueryable<Stop> query = context.Stops;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Stop Find(int id)
        {
            return context.Stops.Find(id);
        }

        public void InsertOrUpdate(Stop stop)
        {
            if (stop.Id == default(int)) {
                // New entity
                context.Stops.Add(stop);
            } else {
                // Existing entity
                context.Entry(stop).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var stop = context.Stops.Find(id);
            context.Stops.Remove(stop);
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

    public interface IStopRepository : IDisposable
    {
        IQueryable<Stop> All { get; }
        IQueryable<Stop> AllIncluding(params Expression<Func<Stop, object>>[] includeProperties);
        Stop Find(int id);
        void InsertOrUpdate(Stop stop);
        void Delete(int id);
        void Save();
    }
}