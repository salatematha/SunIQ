using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SunIQ.Models
{
    public class Calculation_Setup
    {
        public Calculation_Setup()
        {
            Rated_Power = 1262000;
            Efficiency = 695;
            Efficiency_Plus_Minus = 15;
        }

        [Key]
        public int CalculationId { get; set; }

        [MaxLength(130),Required, DisplayName("Setup Name")]
        public string Setup_Name { get; set; }

        [DefaultValue(1262000), Required, DisplayName("Rated Power")]
        public int Rated_Power { get; set; }

        [DefaultValue(695), Required]
        public int Efficiency { get; set; }

        [DefaultValue(15), Required, DisplayName("Efficiency Plus Minus")]
        public int Efficiency_Plus_Minus { get; set; }

        public virtual ICollection<Setup_Percentage_Range> Setup_percentage_Ranges { get; set; }
    }

    public class Setup_Percentage_Range
    {
        [Key]

        public int Setup_PercentageId { get; set; }

        [Required,ForeignKey("Calculation_Setup")]
        public int CalculationId { get; set; }

        [Required]
        public int Percentage { get; set; }

        [Required, DisplayName("Min Percentage Range")]
        public int Min_Percentage_Range { get; set; }

        [Required, DisplayName("Max Percentage Range")]
        public int Max_Percentage_Range { get; set; }

        [Required]
        public float Weightage { get; set; }

        public virtual Calculation_Setup Calculation_Setup { get; set; }
    }
}