using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SunIQ.Models
{
    public class Inverter
    {
        [Key]
        public int InverterId { get; set; }
        [Required]
        [MaxLength(130), DisplayName("Serial Number")]
        public String Serial_Number { get; set; }

        [MaxLength(130), DisplayName("File Name")]
        public String File_Name { get; set; }

        [DisplayName("Last Modified")]
        public DateTime Last_Modified { get; set; }

        public virtual ICollection<Inverter_Files_Data> inverter_files_datas { get; set; }
    }

    public class Inverter_Files_Data
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Inverter")]
        public int InverterId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int Serial_No { get; set; }
        public double ETUK { get; set; }
        public double ETPK { get; set; }
        public double STUK { get; set; }
        public double STPK { get; set; }
        public double SFPK { get; set; }

        public virtual Inverter Inverter { get; set; }
    }
}
