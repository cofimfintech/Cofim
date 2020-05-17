using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Cofim.Common.Model.Response
{
    public class RendimientoResp
    {
        public bool   Portafolio { get; set; }
        public string Operadora  { get; set; }     
        public string Fondo      { get; set; }
        public string Serie      { get; set; }        
        public string FondoSerie { get; set; }

        /***  RENDIMIENTOS DIRECTOS ***/        
        public decimal? RendiDirCuatroDias { get; set; }        
        public decimal? RendiDirSieteDias { get; set; }        
        public decimal? RendiDirUnMes { get; set; }
        public decimal? RendiDirTresMeses { get; set; }       
        public decimal? RendiDirSeisMeses { get; set; }
        public decimal? RendiDirNueveMeses { get; set; }
        public decimal? RendiDirDoceMeses { get; set; }        
        public decimal? RendiDirDiesiOchoMeses { get; set; }
        public decimal? RendiDirVeintiCuatroMeses { get; set; }        
        public decimal? RendiDirTreintaySeisMeses { get; set; }
        /***  RENDIMIENTOS DIRECTOS ***/

        /***  RENDIMIENTOS ANUALIZADOS ***/        
        public decimal? RendiAnuCuatroDias { get; set; }        
        public decimal? RendiAnuSieteDias  { get; set; }
        public decimal? RendiAnuUnMes      { get; set; }        
        public decimal? RendiAnuTresMeses  { get; set; }
        public decimal? RendiAnuSeisMeses  { get; set; }
        public decimal? RendiAnuNueveMeses { get; set; }
        public decimal? RendiAnuDoceMeses  { get; set; }
        public decimal? RendiAnuDiesiOchoMeses    { get; set; }
        public decimal? RendiAnuVeintiCuatroMeses { get; set; }        
        public decimal? RendiAnuTreintaySeisMeses { get; set; }
               
        public string Directo4Dias   => RendiDirCuatroDias?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });
        public string Directo7Dias   => RendiDirSieteDias?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });
        public string Directo1Mes    => RendiDirUnMes?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });
        public string Directo3Meses  => RendiDirTresMeses?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });
        public string Directo6Meses  => RendiDirSeisMeses?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });
        public string Directo9Meses  => RendiDirNueveMeses?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });
        public string Directo12Meses => RendiDirDoceMeses?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });
        public string Directo18Meses => RendiDirDiesiOchoMeses?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });
        public string Directo24Meses => RendiDirVeintiCuatroMeses?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });
        public string Directo36Meses => RendiDirTreintaySeisMeses?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });

        /***  RENDIMIENTOS ANUALIZADOS ***/
        public string Anual4Dias   => RendiAnuCuatroDias?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });
        public string Anual7Dias   => RendiAnuSieteDias?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });
        public string Anual1Mes    => RendiAnuUnMes?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });
        public string Anual3Meses  => RendiAnuTresMeses?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });
        public string Anual6Meses  => RendiAnuSeisMeses?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });
        public string Anual9Meses  => RendiAnuNueveMeses?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });
        public string Anual12Meses => RendiAnuDoceMeses?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });
        public string Anual18Meses => RendiAnuDiesiOchoMeses?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });
        public string Anual24Meses => RendiAnuVeintiCuatroMeses?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });
        public string Anual36Meses => RendiAnuTreintaySeisMeses?.ToString("P6", new NumberFormatInfo { PercentPositivePattern = 1, PercentNegativePattern = 1 });


    }//class
}//namespace
