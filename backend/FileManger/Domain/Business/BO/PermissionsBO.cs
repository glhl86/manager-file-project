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
    public class PermissionsBO : IPermissions
    {
        private readonly FileManagerContext context;
        private readonly IMapper mapper;

        public PermissionsBO(FileManagerContext context)
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
        /// Crear registro de Permissions
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public long Create(PermissionsAM entity)
        {
            try
            {
                var Permissions = mapper.Map<Permissions>(entity);

                IRepository<Permissions> repo = new PermissionsRepository(context);
                return repo.Create(Permissions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Obtener cantidad de registros de Permissions
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public int Count()
        {
            try
            {
                IRepository<Permissions> repo = new PermissionsRepository(context);
                return repo.Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Obtener cantidad de registros de Permissions según filtro
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public int Count(Expression<Func<PermissionsAM, bool>> predicate)
        {
            try
            {
                var where = mapper.MapExpression<Expression<Func<Permissions, bool>>>(predicate);

                IRepository<Permissions> repo = new PermissionsRepository(context);
                return repo.Count(where);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Obtener Permissions por Id
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public PermissionsAM Get(long id)
        {
            try
            {
                IRepository<Permissions> repo = new PermissionsRepository(context);
                var Permissions = repo.Get(id);

                return mapper.Map<PermissionsAM>(Permissions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Obtener lista de Permissions
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public List<PermissionsAM> Get()
        {
            try
            {
                IRepository<Permissions> repo = new PermissionsRepository(context);
                var Permissions = repo.Get();

                return mapper.Map<List<PermissionsAM>>(Permissions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Obtener lista de Permissions
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public List<PermissionsAM> Get(Expression<Func<PermissionsAM, bool>> predicate)
        {
            try
            {
                var where = mapper.MapExpression<Expression<Func<Permissions, bool>>>(predicate);

                IRepository<Permissions> repo = new PermissionsRepository(context);
                var Permissions = repo.Get(where);

                return mapper.Map<List<PermissionsAM>>(Permissions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Obtener primera Permissionss según filtro
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public PermissionsAM GetFirst(Expression<Func<PermissionsAM, bool>> predicate)
        {
            try
            {
                var where = mapper.MapExpression<Expression<Func<Permissions, bool>>>(predicate);

                IRepository<Permissions> repo = new PermissionsRepository(context);
                var Permissions = repo.GetFirst(where);

                return mapper.Map<PermissionsAM>(Permissions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Actualizar Permissions
        /// Autor: Jair Guerrero
        /// Fecha: 2021
        /// </summary>
        public void Update(PermissionsAM entity)
        {
            try
            {
                var Permissions = mapper.Map<Permissions>(entity);

                IRepository<Permissions> repo = new PermissionsRepository(context);
                repo.Update(Permissions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
