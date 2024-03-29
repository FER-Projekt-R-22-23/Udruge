﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UdrugeApp.DataAccess.SqlServer.Data.DbModels
{
    [Index("IdUdruge", Name = "OIB_Udruge", IsUnique = true)]
    public partial class Udruge
    {
        public Udruge()
        {
            Prostori = new HashSet<Prostori>();
            Resursi = new HashSet<Resursi>();
            VoditeljiUdruge = new HashSet<VoditeljiUdruge>();
        }

        [Key]
        public int IdUdruge { get; set; }
        [Required]
        [Column("OIB")]
        [StringLength(11)]
        [Unicode(false)]
        public string Oib { get; set; }
        [Required]
        [StringLength(30)]
        [Unicode(false)]
        public string Naziv { get; set; }
        [Required]
        [StringLength(60)]
        [Unicode(false)]
        public string Sjediste { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string BrMob { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Mail { get; set; }

        [InverseProperty("IdUdrugeNavigation")]
        public virtual ICollection<Prostori> Prostori { get; set; }
        [InverseProperty("IdUdrugeNavigation")]
        public virtual ICollection<Resursi> Resursi { get; set; }
        [InverseProperty("IdUdrugeNavigation")]
        public virtual ICollection<VoditeljiUdruge> VoditeljiUdruge { get; set; }
    }
}