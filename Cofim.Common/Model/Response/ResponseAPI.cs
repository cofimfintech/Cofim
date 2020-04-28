using System;
using System.Collections.Generic;
using System.Text;


namespace Cofim.Common.Model.Response
{
 
    // TODO: (Checar) Implementacion de clases genericas, en vez de devolver un objetc, mediante la clase generica se puede especificar que la respuesta de la API va a una clase o un modelo
    public class ResponseAPI<Resp> where Resp: class
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public Resp Result { get; set; }
    }
}
