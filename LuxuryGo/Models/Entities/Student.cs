namespace LuxuryGo.Models.Entities
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Student
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public string NgaySinh { get; set; }
        public string QueQuan { get; set; }
        public string MaDanToc { get; set; }
        public string MaLop { get; set; }
    }
}
