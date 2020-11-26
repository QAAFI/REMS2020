﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using Unit = MediatR.Unit;

namespace Rems.Application.CQRS
{
    public class InsertTraitTableCommand : IRequest
    {
        public DataTable Table { get; set; }

        public Type Type { get; set; }

        public Type Dependency { get; set; }
    }

    public class InsertTraitTableCommandHandler : IRequestHandler<InsertTraitTableCommand>
    {
        private readonly IRemsDbContext _context;

        public InsertTraitTableCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(InsertTraitTableCommand request, CancellationToken cancellationToken) => Task.Run(() => Handler(request));

        private Unit Handler(InsertTraitTableCommand request)
        {
            var columns = request.Table.Columns.Cast<DataColumn>().Where(c => !c.ColumnName.Contains("Column"));
            var infos = new List<PropertyInfo>();
            var traits = new List<Trait>();

            foreach (DataColumn c in request.Table.Columns)
            {
                if (c.ColumnName.Contains("Column")) continue;

                if (c.FindProperty() is PropertyInfo info)
                {
                    infos.Add(info);
                }
                else
                {
                    var trait = _context.Traits.FirstOrDefault(e => e.Name == c.ColumnName);
                    if (trait is null) trait = _context.AddTrait(c.ColumnName, request.Type.Name);

                    traits.Add(trait);
                }
            }

            var foreignInfo = request.Dependency.GetProperties().First(p => p.PropertyType == request.Type);
            var traitInfo = request.Dependency.GetProperty("TraitId");
            var valueInfo = request.Dependency.GetProperty("Value");

            var entities = new List<IEntity>();
            foreach (DataRow r in request.Table.Rows)
            {
                var entity = r.ToEntity(_context, request.Type, infos.ToArray());

                foreach (var trait in traits)
                {
                    var foreign = Activator.CreateInstance(foreignInfo.DeclaringType) as IEntity;
                    var value = r[trait.Name];

                    foreignInfo.SetValue(foreign, entity);
                    traitInfo.SetValue(foreign, trait.TraitId);
                    foreign.SetValue(valueInfo, value);
                    entities.Add(foreign);
                    //_context.Attach(foreign);
                }

                _context.Attach(entity);

                EventManager.InvokeProgressIncremented();
            }
            _context.SaveChanges();
            _context.AttachRange(entities.ToArray());
            _context.SaveChanges();
            return Unit.Value;
        }
    }
}
