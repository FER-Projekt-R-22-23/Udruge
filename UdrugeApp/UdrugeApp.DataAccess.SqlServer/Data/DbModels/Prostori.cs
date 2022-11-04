﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UdrugeApp.DataAccess.SqlServer.Data.DbModels
{
    public partial class Prostori
    {
        public Prostori()
        {
            Resursi = new HashSet<Resursi>();
        }

        [Key]
        public int IdProstor { get; set; }
        public int IdUdruge { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Adresa { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string Namjena { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Dodijelio { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DodjeljenoDo { get; set; }

        [ForeignKey("IdUdruge")]
        [InverseProperty("Prostori")]
        public virtual Udruge IdUdrugeNavigation { get; set; }
        [InverseProperty("IdProstorNavigation")]
        public virtual ICollection<Resursi> Resursi { get; set; }
    }
}