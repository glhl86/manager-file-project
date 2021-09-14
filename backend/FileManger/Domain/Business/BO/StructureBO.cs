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
    public class StructureBO : IStructure
    {
        private readonly FileManagerContext context;
        private readonly IMapper mapper;

        public StructureBO(FileManagerContext context)
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
        /// Crear registro de Structure
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public long Create(StructureAM entity)
        {
            try
            {
                var Structure = mapper.Map<Structure>(entity);

                IRepository<Structure> repo = new StructureRepository(context);
                return repo.Create(Structure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Obtener cantidad de registros de Structure
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public int Count()
        {
            try
            {
                IRepository<Structure> repo = new StructureRepository(context);
                return repo.Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Obtener cantidad de registros de Structure según filtro
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public int Count(Expression<Func<StructureAM, bool>> predicate)
        {
            try
            {
                var where = mapper.MapExpression<Expression<Func<Structure, bool>>>(predicate);

                IRepository<Structure> repo = new StructureRepository(context);
                return repo.Count(where);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Obtener Structure por Id
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public StructureAM Get(long id)
        {
            try
            {
                IRepository<Structure> repo = new StructureRepository(context);
                var Structure = repo.Get(id);

                return mapper.Map<StructureAM>(Structure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Obtener lista de Structure
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public List<StructureAM> Get()
        {
            try
            {
                IRepository<Structure> repo = new StructureRepository(context);
                var Structure = repo.Get();

                return mapper.Map<List<StructureAM>>(Structure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Obtener lista de Structure
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public List<StructureAM> Get(Expression<Func<StructureAM, bool>> predicate)
        {
            try
            {
                var where = mapper.MapExpression<Expression<Func<Structure, bool>>>(predicate);

                IRepository<Structure> repo = new StructureRepository(context);
                var Structure = repo.Get(where);

                return mapper.Map<List<StructureAM>>(Structure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Obtener primera Structures según filtro
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public StructureAM GetFirst(Expression<Func<StructureAM, bool>> predicate)
        {
            try
            {
                var where = mapper.MapExpression<Expression<Func<Structure, bool>>>(predicate);

                IRepository<Structure> repo = new StructureRepository(context);
                var Structure = repo.GetFirst(where);

                return mapper.Map<StructureAM>(Structure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Actualizar Structure
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public void Update(StructureAM entity)
        {
            try
            {
                var Structure = mapper.Map<Structure>(entity);

                IRepository<Structure> repo = new StructureRepository(context);
                repo.Update(Structure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
