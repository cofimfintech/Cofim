using Cofim.Common.Model.DataEntity;
using Cofim.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Cofim.Web.Controllers
{
    public class EtlController : Controller
    {
        private readonly IMailHelper _mailHelper;
        private readonly IEtlHelper  _etlHelper;

        public EtlController(IMailHelper mailHelper, IEtlHelper etlHelper)
        {            
            _mailHelper = mailHelper;
            _etlHelper  = etlHelper;
        }

        public void Montosminimos()
        {
            EtlProcessedFile ProcessedFile = _etlHelper.LoadExcelFileMontosMinimos();
            //_mailHelper.SendEmailEtlLoad("ems@convivere.mx", ProcessedFile);


        }//Montominimo

        

        public void Vectorprecios()
        {

            EtlProcessedFile ProcessedFile = _etlHelper.LoadExcelFileVectorPrecio();
           //_mailHelper.SendEmailEtlLoad("ems@convivere.mx", ProcessedFile);

        }//Vectorprecio


        

    }//CLASS
}//NAMESPACE