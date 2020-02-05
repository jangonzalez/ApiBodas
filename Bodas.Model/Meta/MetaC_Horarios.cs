using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bodas.Model.BD
{
    [MetadataType(typeof(MetaC_Horarios))]
    public partial class C_Horarios { }
    public class MetaC_Horarios
    {
        public int id { get; set; }
        public string hora { get; set; }
        public int idHotel { get; set; }

        [IgnoreDataMember]
        public virtual C_Hoteles C_Hoteles { get; set; }
        
        
    }
}
