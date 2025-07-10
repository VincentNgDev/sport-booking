using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public abstract class SchemaBase
    {
        protected SchemaBase()
        {
            ObjectId = Guid.NewGuid();
        }

        /// <summary>
        /// Object Unique ID
        /// </summary>
        [Key]
        public Guid ObjectId { get; private set; }
        /// <summary>
        /// Object Name
        /// </summary>
        public string? ObjectName { get; set; }

        /// <summary>
        /// Object Number
        /// used for document number & etc
        /// </summary>
        public string? ObjectNumber { get; set; }
        /// <summary>
        /// Object created time
        /// Coordinated Universal Time (UTC)
        /// </summary>
        public DateTime? CreatedDateTime { get; set; }
        /// <summary>
        /// User who create the object
        /// </summary>
        public string? CreatedUser { get; set; }
        /// <summary>
        /// Modify date time
        /// </summary>
        public DateTime? UpdatedDateTime { get; set; }
        /// <summary>
        /// User who update the object
        /// </summary>
        public string? UpdatedUser { get; set; }
        /// <summary>
        /// In order to keep the data even user want to delete
        /// Implementation of soft delete for keep the data
        /// but will no able to retrieve data 
        /// </summary>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// Version tracking
        /// </summary>
        [Timestamp]
        public byte[]? Version { get; set; }
    }
}
