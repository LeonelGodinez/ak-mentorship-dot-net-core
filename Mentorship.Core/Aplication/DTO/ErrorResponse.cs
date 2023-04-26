using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentorship.Core.Aplication.DTO
{
    public class ErrorResponse : BaseResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return "{Code: " + Code + ", Message: " + Message + "}"; 
        }
    }
}
