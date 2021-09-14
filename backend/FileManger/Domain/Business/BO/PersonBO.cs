using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using CrossCutting.ApiModel;
using Domain.Business.Interface;
using Domain.Business.Profiles;
using Domain.Models;
using Domain.Repository;
using Domain.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Business.BO
{
    public class PersonBO : IPerson
    {
        private readonly FileManagerContext context;
        private readonly IMapper mapper;

        public PersonBO(FileManagerContext context)
        {
            this.context = context;

            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();
                cfg.AddProfile<AdminProfile>();
            });

            mapper = new Mapper(mapConfig);
        }

        /// <summary>
        /// Crear registro de person
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public long Create(PersonAM entity)
        {
            try
            {
                var person = mapper.Map<Person>(entity);

                IRepository<Person> repo = new PersonRepository(context);
                return repo.Create(person);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Obtener cantidad de registros de person
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public int Count()
        {
            try
            {
                IRepository<Person> repo = new PersonRepository(context);
                return repo.Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Obtener cantidad de registros de person según filtro
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public int Count(Expression<Func<PersonAM, bool>> predicate)
        {
            try
            {
                var where = mapper.MapExpression<Expression<Func<Person, bool>>>(predicate);

                IRepository<Person> repo = new PersonRepository(context);
                return repo.Count(where);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Obtener person por Id
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public PersonAM Get(long id)
        {
            try
            {
                IRepository<Person> repo = new PersonRepository(context);
                var person = repo.Get(id);

                return mapper.Map<PersonAM>(person);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Obtener lista de person
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public List<PersonAM> Get()
        {
            try
            {
                IRepository<Person> repo = new PersonRepository(context);
                var person = repo.Get();

                return mapper.Map<List<PersonAM>>(person);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Obtener lista de person
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public List<PersonAM> Get(Expression<Func<PersonAM, bool>> predicate)
        {
            try
            {
                var where = mapper.MapExpression<Expression<Func<Person, bool>>>(predicate);

                IRepository<Person> repo = new PersonRepository(context);
                var person = repo.Get(where);

                return mapper.Map<List<PersonAM>>(person);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Obtener primera persons según filtro
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public PersonAM GetFirst(Expression<Func<PersonAM, bool>> predicate)
        {
            try
            {
                var where = mapper.MapExpression<Expression<Func<Person, bool>>>(predicate);

                IRepository<Person> repo = new PersonRepository(context);
                var person = repo.GetFirst(where);

                return mapper.Map<PersonAM>(person);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Actualizar person
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public void Update(PersonAM entity)
        {
            try
            {
                var person = mapper.Map<Person>(entity);

                IRepository<Person> repo = new PersonRepository(context);
                repo.Update(person);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
