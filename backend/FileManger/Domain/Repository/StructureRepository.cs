using Domain.Models;
using Domain.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Repository
{
    public class StructureRepository : BaseRepository<Structure>
    {
        public StructureRepository(FileManagerContext context) : base(context)
        {

        }

        public override int Count()
        {
            try
            {
                return context.Structures.Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override int Count(Expression<Func<Structure, bool>> predicate)
        {
            try
            {
                return context.Structures.Count(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override long Create(Structure entity)
        {
            try
            {
                context.Structures.Add(entity);
                context.SaveChanges();

                return entity.StructureId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override Structure Get(long id)
        {
            try
            {
                return context.Structures.Where(j => j.StructureId == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override ICollection<Structure> Get()
        {
            try
            {
                return context.Structures.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override ICollection<Structure> Get(Expression<Func<Structure, bool>> predicate)
        {
            try
            {
                return context.Structures.Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override ICollection<Structure> Get(Expression<Func<Structure, bool>> predicate, int page, int size, Func<Structure, object> filterAttribute, bool descending)
        {
            return descending ? context.Structures.Where(predicate).Skip(page).Take(size).OrderByDescending(filterAttribute).ToList()
               : context.Structures.Where(predicate).Skip(page).Take(size).OrderBy(filterAttribute).ToList();
        }

        public override Structure GetFirst(Expression<Func<Structure, bool>> predicate)
        {
            try
            {
                return context.Structures.FirstOrDefault(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override void Update(Structure entity)
        {
            try
            {
                context.Structures.Update(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

    }
}
