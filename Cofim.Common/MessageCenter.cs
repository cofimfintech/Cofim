using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cofim.Common
{
    public static class MessageCenter
    {
        /*******URL CONSTANT********/        
        public const String URL         = "https://cofim.azurewebsites.net/";
        public const String URL_API     = "https://cofim.azurewebsites.net/api/";
        public const String URL_IMG_APP = "https://Cofim.azurewebsites.net/images/app/";
        /*******URL CONSTANT********/

        /*******WEB APP Messages********/
        public const String webAppTextFieldRequired  = "El campo {0} es obligatorio.";
        public const String webAppTextFieldMaxLength = "El campo {0} no puede tener más de {1} caractéres.";
        public const String webAppTextFieldBetween   = "El campo {0} debe tener entre {2} y {1} caractéres.";

        public const String webAppTextFieldRequiredCombo = "Favor de seleccionar un valor";

        public const String webAppTextEmailRequired = "El correo electrónico es obligatorio.";
        public const String webAppTextEmailInvalid  = "Correo electrónico invalido. Ej.: micorreo@midominio.com";
        public const String webAppTextPhoneInvalid  = "El celular es invalido";

        public const String webApplabelLoginFail    = "Usuario o Contraseña invalidos. Intente de nuevo.";

        public const String webApplabelEmailNotFound = "Usuario no encontrado.";

        public const String webAppMessagePageNotFound  = "Lo sentimos, el contenido buscado no está disponible.";
        public const String webAppMessageNotAuthorized = "Usted no está autorizado para ejecutar esta acción.";

        public const String webAppTitlePageRegisterUser = "Registro a Tramiauto";
        public const String webAppTitlePageEditUser     = "Actualizar Perfil";
        

        /*******WEB APP Messages********/

        /*******APP Messages********/
        public const String appTextEmailFieldRequired = "El correo electrónico es obligatorio, para ingresar a la app.";
        public const String appTextEmailUsed          = "El correo electrónico ingresado, ya se encuentra en uso por otra persona.";
        public const String appTextPassFieldRequired  = "La contraseña es obligatoria, para ingresar a la app.";

        public const String appLabelError      = "Error";
        public const String appLabelAceptar    = "Aceptar";
        public const String appLabelErrorNoCam = "Error No Cámara";
        public const String appLabelErrorTram  = "Requisito o Doc";
        public const String appLabelErrorTR    = "Favor de seleccionar el requisito o documento a anexar";
        public const String appLabelErrorTT    = "Favor de seleccionar el tipo de Tramite a realizar";
        public const String appLabelErrorFP    = "Favor de seleccionar una forma de pago";
        public const String appDirDocs         = "mx.tramiauto";

        public const String appLabelErrorConnect     = "Error de Conectividad";
        public const String appLabelErrorNoInter     = "Tramiuto para su correcto funcionamiento necesita de acceso a internet, verifique su conexión.";
        public const String appLabelErrorNoUserToken = "Hubo un problema con el usuario y su acceso.";
        public const String appLabelErrorNoCamara    = "No fue posible habilitar la cámara del dispositivo para Escanear el documento.";

        public const String appTitlePageLogin         = "Bienvenido a Tramiauto";
        public const String appTitlePageTramites      = "Tramites";
        public const String appTitlePageDetailTramite = "Detalle de Tramite";
        public const String appGroupName1DFTramite    = "Tipo Tramite";
        public const String appGroupName2DFTramite    = "Documentos";
        public const String appGroupName3DFTramite    = "Forma de Pago";
        public const String appTitleDocsTramite       = "Requisitos/Docs - ";

        /*******APP Messages********/


        /*******Common Messages********/
        public const String slogan                   = "Desiciones Inteligentes";
        public const String productDescription       = "El mejor comparador de Fondos de Inversión de México.";
        public const String commonMessageChoose      = "[Por favor seleccione]";
        public const String commonMessageChooseValue = "-1";


        public const String commonMessageEmailInst    = "Para completar el registro es necesario confirmar la cuenta de correo electrónico, ha sido envido un correo con las instrucciónes de confirmación";
        public const String commonMessageEmailConfirm = "Gracias por confirmar su cuenta de correo electrónica. Bienvenido a Tramiuto.";
        public const String Project_Name = "Tramiauto";


        public const String commonTitlePageRecoverPwd   = "Recuperar Contaraseña";
        public const String commonMessageRecoverNoEmail = "La cuenta de correo elecrónico no se encuentra registrada.";
        public const String commonMessageRecoverEmail   = "Han sido enviadas instrucciones para recuperar la contresaña al correo electrónico.";
        public const String commonMessagePassReset      = "La contraseña ha sido reestablecida, favor de ingresar al portal.";
        public const String commonMessageErrorPassReset = "Ha ocurrido un error al intentar reestablecer la contraseña. Favor de contactar al administrador.";

        public const String FormaDePagoCard  = "CARD";
        public const String FormaDePagoBank  = "BANK";
        public const String FormaDePagoStore = "STORE";

        /*******Common Messages********/
    }
}
