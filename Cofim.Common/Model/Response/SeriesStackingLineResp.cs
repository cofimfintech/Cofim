using System;
using System.Collections.Generic;
using System.Text;

namespace Cofim.Common.Model.Response
{
    public class SeriesStackingLineResp
    {        
        public string Type { get; set; }
        public List<ChartDataStackingLineResp> DataSource { get; set; }
        public MarkerClass Marker { get; set; }
        public string DashArray { get; set; }
        public string XName { get; set; }
        public int Width { get; set; }
        public string YName { get; set; }
        public string Name { get; set; }


}//class


    public class MarkerClass
    {
        public bool Visible { get; set; }
    }
 
}//namespace