using System;
using System.Collections.Generic;
using System.IO;
using Cofim.Common;
using Cofim.Common.Model.DataEntity;
using Cofim.Web.Models;
using EFCore.BulkExtensions;
using Syncfusion.XlsIO;
using System.Diagnostics;
using System.Linq;

namespace Cofim.Web.Helpers
{

    public class EtlHelper : IEtlHelper
    {
        private readonly DataContext _dataContext;

        public EtlHelper( DataContext context)
        {           
            _dataContext = context;
        }



        public EtlProcessedFile LoadExcelFileMontosMinimos()
        {
            var dateIni    = DateTime.Now;
            var idLoadFile = dateIni.TimeOfDay.ToString().PadRight(20).Replace(".", "").Replace(":", "");

            EtlProcessedFile processedFile = new EtlProcessedFile { DateIni = dateIni, FileName = MessageCenter.URL_FILE_MM };
            try
            {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            List<FondosInversionMontosMinimos> fondos = new List<FondosInversionMontosMinimos>();
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                FileStream inputStream = new FileStream(processedFile.FileName, FileMode.Open);
                IWorkbook     workbook = excelEngine.Excel.Workbooks.Open(inputStream);
                IWorksheet       sheet = workbook.Worksheets["montos minimos"];
                IMigrantRange   mRange = sheet.MigrantRange;
                int           rowCount = sheet.UsedRange.LastRow;
                int           colCount = sheet.UsedRange.LastColumn;

                for (int r = 2; r <= rowCount; r++)
                {
                    var fondoInversion = new FondosInversionMontosMinimos();
                    for (int c = 1; c <= colCount; c++)
                    {
                        mRange.ResetRowColumn(r, c);
                        var value = mRange.Value?.ToString().TrimEnd().TrimStart();
                        DateTime parsedDate = new DateTime();                        
                        switch (c)
                        {
                            case 1: fondoInversion.Fecha = DateTime.TryParse(value, out parsedDate) == true ? parsedDate : new DateTime(); break;
                            case 2: fondoInversion.Operadora = value; break;
                            case 3: fondoInversion.Fondo = value; break;
                            case 4: fondoInversion.Serie = value; break;
                            case 6: fondoInversion.MontoMinimo = String.IsNullOrEmpty(value) == true ? -1 : Convert.ToDecimal(value.Replace(",", "").Replace("'", "").Replace("´", ""), null); break;
                            case 7: fondoInversion.MontoMinimoTipo = value.ToUpper() == "SERIE A" ? (char)'S' : StringToChar(value.ToCharArray()); break;
                            case 8: fondoInversion.Divisa = value; break;
                            case 9: fondoInversion.Activo = StringToChar(value.ToUpper().ToCharArray()); break;
                            case 11: fondoInversion.PersonaFisica = TransformStringToBoolean(value.ToUpper()); break;
                            case 12: fondoInversion.PersonaMoral = TransformStringToBoolean(value.ToUpper()); break;
                            case 13: fondoInversion.PersonaMoralNoContribuyente = TransformStringToBoolean(value.ToUpper()); break;
                            case 14: fondoInversion.SociedadesDeInversion = TransformStringToBoolean(value.ToUpper()); break;
                        }//SWITCH
                    }

                    fondoInversion.FondoSerie = fondoInversion.Fondo + fondoInversion.Serie;
                    fondoInversion.FondoKey   = fondoInversion.Fecha.ToString("MMyy") + fondoInversion.FondoSerie;
                    fondoInversion.IdLoadFile = idLoadFile;
                    fondos.Add(fondoInversion);
                }
                
                workbook.Close();      //Close the instance of IWorkbook                
                excelEngine.Dispose(); //Dispose the instance of ExcelEngine

            }//USING

            _dataContext.BulkInsert(fondos);
            stopwatch.Stop();

            processedFile.DateEnd       = DateTime.Now;
            processedFile.ElapsedTime   = (stopwatch.ElapsedMilliseconds) / 1000;
            processedFile.LoadedRecords = fondos.Count;
            processedFile.TypeLoad      = "Fondos de Inversión MM";
            processedFile.IdLoadFile    = idLoadFile;

            _dataContext.EtlProcessedFiles.Add(processedFile);
            _dataContext.SaveChanges();

            } catch ( Exception e)
                    { Console.WriteLine($"Finish with errors {e.ToString()} "); }

            return processedFile;

        }//Montominimo

        private bool TransformStringToBoolean(String value)
        {
            return value == "TRUE" ? true : false;
        }

        private char StringToChar(char[] charArr)
        {          
            return charArr[0];
        }




        public EtlProcessedFile LoadExcelFileVectorPrecio()
        {
            var dateIni    = DateTime.Now;
            var idLoadFile = dateIni.TimeOfDay.ToString().PadRight(20).Replace(".", "").Replace(":", "");

            //EtlProcessedFile processedFile = new EtlProcessedFile { DateIni = dateIni, FileName = MessageCenter.URL_FILE_VP };
            // "vectorPrecios_20200301Data.xlsx" vectorPrecios_20200324Data.xlsx vectorPrecios_20200327Data.xlsx vectorPrecios_20200331Data.xlsx
            EtlProcessedFile processedFile = new EtlProcessedFile { DateIni = dateIni, FileName = "vectorPrecios_20200331Data.xlsx" };            
            try
            {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            List<VectorPrecio> vectores = new List<VectorPrecio>();
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                FileStream inputStream = new FileStream(processedFile.FileName, FileMode.Open);
                IWorkbook     workbook = excelEngine.Excel.Workbooks.Open(inputStream);
                IWorksheet       sheet = workbook.Worksheets[0];
                IMigrantRange mRange   = sheet.MigrantRange;
                int           rowCount = sheet.UsedRange.LastRow;
                int           colCount = sheet.UsedRange.LastColumn;

                for (int r = 2; r <= rowCount; r++)
                {   var vectorPrecio = new VectorPrecio();
                    for (int c = 1; c <= colCount; c++)
                    {   mRange.ResetRowColumn(r, c);
                        var value = mRange.Value?.ToString().TrimEnd().TrimStart();
                        DateTime parsedDate = new DateTime();
                        switch (c)
                        {
                            case 1: vectorPrecio.Fecha     = DateTime.TryParse(value, out parsedDate) == true ? parsedDate : new DateTime(); break;
                            case 2: vectorPrecio.Operadora = value; break;
                            case 3: vectorPrecio.Fondo     = value; break;
                            case 4: vectorPrecio.Serie     = value; break;
                            case 6: vectorPrecio.Precio    = String.IsNullOrEmpty(value) == true ? -1 : Convert.ToDecimal(value.Replace(",", "").Replace("'", "").Replace("´", ""), null); break;                           
                        }//SWITCH
                    }
                    var fondoserie = vectorPrecio.Fondo + vectorPrecio.Serie;
                    var fondo      = vectorPrecio.Fondo;
                    var serie      = vectorPrecio.Serie;
                    var operadora  = vectorPrecio.Operadora;
                    var fondokey   = vectorPrecio.Fecha.ToString("MMyy") + fondoserie;
                    //var fondoInverison = _dataContext.FondosInversionMontosMinimos.FirstOrDefault(fi => fi.FondoKey == fondokey);

                    vectorPrecio.FondoSerie     = fondoserie;
                    vectorPrecio.FondoKey       = fondokey;
                    vectorPrecio.FondoInversion = new FondosInversionMontosMinimos { FondoKey = fondokey, Fondo = fondo, Serie = serie, FondoSerie = fondoserie, Operadora = operadora } ;
                    vectorPrecio.IdLoadFile     = idLoadFile;

                     /*** CUATRO_DIAS ***/
                     var r4 = CalculateRendimiento(vectorPrecio.Fecha, vectorPrecio.Precio, fondoserie, MessageCenter.CUATRO_DIAS);
                     vectorPrecio.RendiDirCuatroDias = r4.Directo;
                     vectorPrecio.RendiAnuCuatroDias = r4.Anualizado;
                     
                     /*** SIETE_DIAS ***/
                     var r7 = CalculateRendimiento(vectorPrecio.Fecha, vectorPrecio.Precio, fondoserie, MessageCenter.SIETE_DIAS);
                     vectorPrecio.RendiDirSieteDias = r7.Directo;
                     vectorPrecio.RendiAnuSieteDias = r7.Anualizado;
                     
                     /*** UN_MES ***/
                     var r30 = CalculateRendimiento(vectorPrecio.Fecha, vectorPrecio.Precio, fondoserie, MessageCenter.UN_MES);
                     vectorPrecio.RendiDirUnMes = r30.Directo;
                     vectorPrecio.RendiAnuUnMes = r30.Anualizado;

                     /*** TRES_MESES ***/
                     var r90 = CalculateRendimiento(vectorPrecio.Fecha, vectorPrecio.Precio, fondoserie, MessageCenter.TRES_MESES);
                     vectorPrecio.RendiDirTresMeses = r90.Directo;
                     vectorPrecio.RendiAnuTresMeses = r90.Anualizado;

                     /*** SEIS_MESES ***/
                     var r180 = CalculateRendimiento(vectorPrecio.Fecha, vectorPrecio.Precio, fondoserie, MessageCenter.SEIS_MESES);
                     vectorPrecio.RendiDirSeisMeses = r180.Directo;
                     vectorPrecio.RendiAnuSeisMeses = r180.Anualizado;

                     /*** NUEVE_MESES ***/
                     var r270 = CalculateRendimiento(vectorPrecio.Fecha, vectorPrecio.Precio, fondoserie, MessageCenter.NUEVE_MESES);
                     vectorPrecio.RendiDirNueveMeses = r270.Directo;
                     vectorPrecio.RendiAnuNueveMeses = r270.Anualizado;

                     /*** DOCE_MESES ***/
                     var r360 = CalculateRendimiento(vectorPrecio.Fecha, vectorPrecio.Precio, fondoserie, MessageCenter.DOCE_MESES);
                     vectorPrecio.RendiDirDoceMeses = r360.Directo;
                     vectorPrecio.RendiAnuDoceMeses = r360.Anualizado;

                     /*** DIECIOCHO_MESES ***/
                     var r540 = CalculateRendimiento(vectorPrecio.Fecha, vectorPrecio.Precio, fondoserie, MessageCenter.DIECIOCHO_MESES);
                     vectorPrecio.RendiDirDiesiOchoMeses = r540.Directo;
                     vectorPrecio.RendiAnuDiesiOchoMeses = r540.Anualizado;

                     /*** VEINTICUATRO_MESES ***/
                     var r720 = CalculateRendimiento(vectorPrecio.Fecha, vectorPrecio.Precio, fondoserie, MessageCenter.VEINTICUATRO_MESES);
                     vectorPrecio.RendiDirVeintiCuatroMeses = r720.Directo;
                     vectorPrecio.RendiAnuVeintiCuatroMeses = r720.Anualizado;

                     /*** TREINTAYSEIS_MESES ***/
                     var r1080 = CalculateRendimiento(vectorPrecio.Fecha, vectorPrecio.Precio, fondoserie, MessageCenter.TREINTAYSEIS_MESES);
                     vectorPrecio.RendiDirTreintaySeisMeses = r1080.Directo;
                     vectorPrecio.RendiAnuTreintaySeisMeses = r1080.Anualizado;

                     vectores.Add(vectorPrecio);
                }

                workbook.Close();      //Close the instance of IWorkbook                
                excelEngine.Dispose(); //Dispose the instance of ExcelEngine

            }//USING

            _dataContext.BulkInsert(vectores);
            stopwatch.Stop();
            processedFile.DateEnd       = DateTime.Now;
            processedFile.ElapsedTime   = (stopwatch.ElapsedMilliseconds) / 1000;
            processedFile.LoadedRecords = vectores.Count;
            processedFile.TypeLoad      = "Vector de Precios";
            processedFile.IdLoadFile    = idLoadFile;

            _dataContext.EtlProcessedFiles.Add(processedFile);
            _dataContext.SaveChanges();

            } catch ( Exception e)
                    { Console.WriteLine($"Errors LoadExcelFileVectorPrecio: {e.ToString()} "); }

            return processedFile;

        }//Vectorprecio

        private Rendimiento CalculateRendimiento(DateTime dateEnd, decimal priceEnd, string fondoSerie, int dias)
        {
            var r = new Rendimiento();            
            try
            {
                DateTime    dateIni = dateEnd.AddDays(dias);
                var vectorPrecioIni = _dataContext.VectorPrecios.FirstOrDefault(vp => vp.FondoSerie == fondoSerie && vp.Fecha == dateIni);
                
                if (vectorPrecioIni == null)
                   { return new Rendimiento { Directo = null, Anualizado = null }; }
                
                decimal priceIni = vectorPrecioIni.Precio;                

                try   { r.Directo = Decimal.Divide(priceEnd, priceIni) - 1; }
                catch ( DivideByZeroException )
                      { return new Rendimiento { Directo = null, Anualizado = null }; }

                r.Anualizado = (r.Directo / Math.Abs(dias)) * 360;

            }
            catch (Exception e)
                  { Console.WriteLine($"Errors CalculateRendimiento {e.ToString()} "); }

            return r;
        }

    }//EtlExcelFile


    public class Rendimiento
    {
        public decimal? Directo { get; set; }
        public decimal? Anualizado { get; set; }
    }   

}//NAMESPACE
