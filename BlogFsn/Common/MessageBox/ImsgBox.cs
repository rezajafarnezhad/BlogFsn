using BlogFsn.Common.Types;

namespace BlogFsn.Common.MessageBox
{
    public interface ImsgBox
    {
        JsResult ModelStateMsg(string ModelErrors, string CallBackFuncs = null);

        JsResult InfoMsg(string message, string CallBackFuncs = null);
        JsResult FaildMsg(string message, string CallBackFuncs = null);

        JsResult SuccessMsg(string message, string CallBackFuncs = null);
    }
}