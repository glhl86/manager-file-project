using CrossCutting.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Business.Interface
{
    public interface IPermissionsBO
    {
        public void Update(PermissionsAM entity);
        public PermissionsAM GetFirst(Expression<Func<PermissionsAM, bool>> predicate);
        public List<PermissionsAM> Get(Expression<Func<PermissionsAM, bool>> predicate);
        public List<PermissionsAM> Get();
        public PermissionsAM Get(long id);
        public int Count(Expression<Func<PermissionsAM, bool>> predicate);
        public int Count();
        public long Create(PermissionsAM entity);
    }
}
