using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace timdothatlac.Common
{
    public class LoginUserSession
    {
        public int MaUser { set; get; }
        public string TenUser { set; get; }
        public string MailUser { set; get; }
        public string MatKhauUser { set; get; }
        public int? QuyenUser { set; get; }
    }
}