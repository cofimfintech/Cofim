using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cofim.Common.Model.Request
{
    public class LoginTARequest : INotifyDataErrorInfo, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private string _email;
        [Display     (Order = 1, ShortName = "Correo Electrónico", Prompt = "Ingrese correo electrónico")] 
        [Required    (AllowEmptyStrings = false, ErrorMessage = MessageCenter.appTextEmailFieldRequired)]
        [StringLength(40, ErrorMessage = MessageCenter.webAppTextFieldMaxLength)]
        public string Email
        {   get { return _email; }
            set { _email = value;
                  RaisePropertyChanged("Email");
                }
        }

        private string _password;
        [Display     (Order = 2, ShortName = "Contraseña", Prompt = "Ingrese su contraseña")]
        [Required    (ErrorMessage = MessageCenter.webAppTextFieldRequired)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = MessageCenter.webAppTextFieldBetween)]
        [DataType    (DataType.Password)]
        public string Password
        {   get { return _password; }
            set { _password = value;
                  RaisePropertyChanged("Pwd");
                }
        }
        
        private bool _rememberMe;
        [Display(Order = 3, ShortName = "Recordar correo")]
        public bool RememberMe
        {   get { return _rememberMe; }
            set { _rememberMe = value;
                  RaisePropertyChanged("RememberMe");
                }
        }

        public LoginTARequest()
        {

        }


        [Display(AutoGenerateField = false)]
        public bool HasErrors
        { get { return false; } }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            var list = new List<string>();
           
            
            return list;
        }

        /*
[Required]
[EmailAddress]
public string Email { get; set; }

[Required]
[MinLength(6)]
public string Password { get; set; }

public bool RememberMe { get; set; }     

public bool IsRunning  { get; set; }

public bool IsEnabled  { get; set; }
*/

    }//class
}//NameSpace
