using Cofim.Common.Model.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cofim.Web.Helpers
{
    public interface IEtlHelper
    {
        EtlProcessedFile LoadExcelFileMontosMinimos();
        EtlProcessedFile LoadExcelFileVectorPrecio();

    }//Class
}//Namespace