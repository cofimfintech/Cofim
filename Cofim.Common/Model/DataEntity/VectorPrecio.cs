using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cofim.Common.Model.DataEntity
{
    public class VectorPrecio
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }        

        [Display(Name = "Operadora")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(50, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string Operadora { get; set; }

        [Display(Name = "Fondo")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(20, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string Fondo { get; set; }

        [Display(Name = "Serie")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(10, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string Serie { get; set; }

        [Display(Name = "FondoSerie")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(30, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string FondoSerie { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage     = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(7, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal Precio { get; set; }

        
        /***  RENDIMIENTOS DIRECTOS ***/
        [Display(Name = "4 Días Directos")]
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiDirCuatroDias { get; set; }
        
        [Display(Name = "7 Días Directos")]
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiDirSieteDias { get; set; }
        
        [Display(Name = "30 Días Directos")]
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiDirUnMes { get; set; }
        
        [Display(Name = "90 Días Directos")]
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiDirTresMeses { get; set; }
        
        [Display(Name = "180 Días Directos")]        
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiDirSeisMeses { get; set; }

        [Display(Name = "270 Días Directos")]
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiDirNueveMeses { get; set; }

        [Display(Name = "360 Días Directos")]        
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiDirDoceMeses { get; set; }
        
        [Display(Name = "540 Días Directos")]        
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiDirDiesiOchoMeses { get; set; }
        
        [Display(Name = "720 Días Directos")]        
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiDirVeintiCuatroMeses { get; set; }
        
        [Display(Name = "1080 Días Directos")]        
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiDirTreintaySeisMeses { get; set; }
        /***  RENDIMIENTOS DIRECTOS ***/

        /***  RENDIMIENTOS ANUALIZADOS ***/
        [Display(Name = "4 Días Anualizado")]
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiAnuCuatroDias { get; set; }

        [Display(Name = "7 Días Anualizado")]
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiAnuSieteDias { get; set; }

        [Display(Name = "30 Días Anualizado")]
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiAnuUnMes { get; set; }

        [Display(Name = "90 Días Anualizado")]
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiAnuTresMeses { get; set; }

        [Display(Name = "180 Días Anualizado")]
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiAnuSeisMeses { get; set; }

        [Display(Name = "270 Días Anualizado")]
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiAnuNueveMeses { get; set; }

        [Display(Name = "360 Días Anualizado")]
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiAnuDoceMeses { get; set; }

        [Display(Name = "540 Días Anualizado")]
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiAnuDiesiOchoMeses { get; set; }

        [Display(Name = "720 Días Anualizado")]
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiAnuVeintiCuatroMeses { get; set; }

        [Display(Name = "1080 Días Anualizado")]
        [DisplayFormat(DataFormatString = "{0:C4}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(18,6)")]
        public decimal? RendiAnuTreintaySeisMeses { get; set; }
        /***  RENDIMIENTOS ANUALIZADOS ***/

        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(20, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string IdLoadFile { get; set; }

        /**************RELATIONSHIP*****************/
        [MaxLength(34, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string FondoKey { get; set; }

        [ForeignKey("FondoKey")] 
        public FondosInversionMontosMinimos FondoInversion { get; set; }

    }//Class
}//Namespace
