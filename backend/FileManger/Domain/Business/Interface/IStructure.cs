using CrossCutting.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Business.Interface
{
    public interface IStructure
    {
        public void Update(StructureAM entity);
        public StructureAM GetFirst(Expression<Func<StructureAM, bool>> predicate);
        public List<StructureAM> Get(Expression<Func<StructureAM, bool>> predicate);
        public List<StructureAM> Get();
        public StructureAM Get(long id);
        public int Count(Expression<Func<StructureAM, bool>> predicate);
        public int Count();
        public long Create(StructureAM entity);
    }
}


