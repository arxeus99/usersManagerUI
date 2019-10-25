using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagerUI
{
    public class User
    {
        private int ID_USR { get; set; }
        private string DNI_USR { get; set; } 
        private string NOM_USR { get; set; }
        private string LLINATGE1 { get; set; }
        private string POB_USR { get; set; }
        private string EMAIL_USR { get; set; }
        private string TIPUS_USR { get; set; }
        public string FullInfo
        {
            get
            {
                return ID_USR + ":\t" + DNI_USR + "\t" + NOM_USR + "\t" + LLINATGE1 + "\t"
                    + POB_USR + "\t" + EMAIL_USR + "\t" + TIPUS_USR;
            }
        }
    }
}
