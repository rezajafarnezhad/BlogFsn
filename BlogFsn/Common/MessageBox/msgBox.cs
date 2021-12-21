using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Infrastructure;
using PrancaBeauty.WebApp.Common.Types;


namespace PrancaBeauty.WebApp.Common.Utility.MessageBox
{
    public class msgBox : ImsgBox
    {
        
        private string Show(string Title, string Message, MsgBoxType Type, string OkBtnText = "OK", string CallBackFunction = null)
        {

            CallBackFunction = CallBackFunction ?? "return '';";
            string Js = $@"swal.fire({{
                                        title: '{Title.Replace("'", "`")}',
                                        html: '{Message.Replace("'", "`")}',
                                        icon: '{Type.ToString()}',
                                        confirmButtonText: '{OkBtnText}',
                                    }}).then((result) => {{
                                        if (result.isConfirmed) {{
                                          {CallBackFunction};
                                        }}
                                    }});";

            return Js;
        }

        public JsResult ModelStateMsg(string ModelErrors, string CallBackFuncs = null)
        {
            return new JsResult(Show("", ModelErrors, MsgBoxType.error, "باشه", CallBackFuncs));
        }
        public JsResult FaildMsg(string message, string CallBackFuncs = null)
        {
            return new JsResult(Show("", message, MsgBoxType.error, "باشه", CallBackFuncs));
        }

        public JsResult SuccessMsg(string message, string CallBackFuncs = null)
        {
            return new JsResult(Show("", message, MsgBoxType.success, "باشه", CallBackFuncs));

        }

        public JsResult InfoMsg(string message, string CallBackFuncs = null)
        {
            return new JsResult(Show("", message, MsgBoxType.info, "باشه", CallBackFuncs));
        }
    }

    public enum MsgBoxType
    {
        success,
        error,
        warning,
        info,
        //question
    }
}
