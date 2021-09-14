using Domain.Models;
using Domain.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Repository
{
    public class PersonRepository : BaseRepository<Person>
    {
        public PersonRepository(FileManagerContext context) : base(context)
        {
        }

        public override int Count()
        {
            try
            {
                return context.Person.Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override int Count(Expression<Func<Person, bool>> predicate)
        {
            try
            {
                return context.Person.Count(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override long Create(Person entity)
        {
            try
            {
                context.Person.Add(entity);
                context.SaveChanges();

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override Person Get(long id)
        {
            try
            {
                return context.Person.Where(j => j.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override ICollection<Person> Get()
        {
            try
            {
                return context.Person.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override ICollection<Person> Get(Expression<Func<Person, bool>> predicate)
        {
            try
            {
                return context.Person.Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override ICollection<Person> Get(Expression<Func<Person, bool>> predicate, int page, int size, Func<Person, object> filterAttribute, bool descending)
        {
            return descending ? context.Person.Where(predicate).Skip(page).Take(size).OrderByDescending(filterAttribute).ToList()
               : context.Person.Where(predicate).Skip(page).Take(size).OrderBy(filterAttribute).ToList();
        }

        public override Person GetFirst(Expression<Func<Person, bool>> predicate)
        {
            try
            {
                return context.Person.FirstOrDefault(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public override void Update(Person entity)
        {
            try
            {
                context.Person.Update(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
