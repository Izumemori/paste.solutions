using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasteSolutions.Database.Generators
{
    public class IdGenerator : ValueGenerator<string>
    {
        public override bool GeneratesTemporaryValues => false;

        public override string Next(EntityEntry entry)
        {
            if (!(entry.Context is DatabaseContext db)) return null;

            return db.GeneratorBucket.NextAsync().GetAwaiter().GetResult();
        }
    }
}
