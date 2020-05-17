using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cofim.Common.Model.DataEntity
{
    public class EtlProcessedFile
    {
        [Key]
        public int Id { get; set; }

        [Display      (Name = "DateIni")]
        [Required     (ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)] 
        public DateTime DateIni { get; set; }

        [Display      (Name = "DateEnd")]
        [Required     (ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)] 
        public DateTime DateEnd { get; set; }

        [Display      (Name = "ElapsedTime")]
        [Required     (ErrorMessage = MessageCenter.webAppTextFieldRequired)]        
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public long ElapsedTime { get; set; }

        [Display (Name = "DateEnd")]
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        public int LoadedRecords { get; set; }

        [Display  (Name = "FileName")]
        [Required (ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(80, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)] 
        public string FileName { get; set; }

        [Display  (Name = "Tipo de Informacion")]
        [Required (ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(40, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string TypeLoad { get; set; }
        
        [Required(ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [MaxLength(20, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string IdLoadFile { get; set; }


    }//EtlprocessedFile

}
