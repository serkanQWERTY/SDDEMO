﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Domain.Entity
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            isActive = true;
            isDeleted = false;
            isDefault = false;
        }

        [Key]
        public Guid id { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime updatedDate { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public bool isDefault { get; set; }

        [ForeignKey("user")]
        public Guid createdBy { get; set; }

        public virtual User? user { get; set; }
    }
}
