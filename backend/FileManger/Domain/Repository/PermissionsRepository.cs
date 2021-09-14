using Domain.Models;
using Domain.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Repository
{
    public class PermissionsRepository : BaseRepository<Permissions>
    {
        public PermissionsRepository(FileManagerContext context) : base(context)
        {

        }

        public override int Count()
        {
            try
            {
                return context.Permissions.Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override int Count(Expression<Func<Permissions, bool>> predicate)
        {
            try
            {
                return context.Permissions.Count(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override long Create(Permissions entity)
        {
            try
            {
                context.Permissions.Add(entity);
                context.SaveChanges();

                return entity.PermissionsId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override Permissions Get(long id)
        {
            try
            {
                return context.Permissions.Where(j => j.PermissionsId == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override ICollection<Permissions> Get()
        {
            try
            {
                return context.Permissions.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override ICollection<Permissions> Get(Expression<Func<Permissions, bool>> predicate)
        {
            try
            {
                return context.Permissions.Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override ICollection<Permissions> Get(Expression<Func<Permissions, bool>> predicate, int page, int size, Func<Permissions, object> filterAttribute, bool descending)
        {
            return descending ? context.Permissions.Where(predicate).Skip(page).Take(size).OrderByDescending(filterAttribute).ToList()
               : context.Permissions.Where(predicate).Skip(page).Take(size).OrderBy(filterAttribute).ToList();
        }

        public override Permissions GetFirst(Expression<Func<Permissions, bool>> predicate)
        {
            try
            {
                return context.Permissions.FirstOrDefault(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override void Update(Permissions entity)
        {
            try
            {
                context.Permissions.Update(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
