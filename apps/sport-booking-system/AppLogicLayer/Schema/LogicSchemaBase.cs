using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public abstract class LogicSchemaBase : SchemaBase
    {
        public Guid? CreatedUserId { get; set; }

        [ForeignKey(nameof(CreatedUserId))]
        public User? Creator { get; set; }
    }
}
