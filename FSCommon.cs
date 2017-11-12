using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunnySnake
{
    public class FSCommon
    {
        #region 定数
        public const string APP_TITLE = "Funny Snake";
        #endregion

        #region 共通関数
        public static bool IsNumber(string src)
        {
            foreach (char c in src)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
        #endregion

        #region メッセージ表示
        public static DialogResult ShowMessage(IWin32Window owner, string msg, string title, MessageBoxButtons btn, MessageBoxIcon icon)
        {
            return MessageBox.Show(owner, msg, title, btn, icon);
        }
        public static void ShowMessageInfo(IWin32Window owner, string msg)
        {
            ShowMessage(owner, msg, APP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowMessageWarn(IWin32Window owner, string msg)
        {
            ShowMessage(owner, msg, APP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ShowMessageError(IWin32Window owner, string msg)
        {
            ShowMessage(owner, msg, APP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool ShowMessageQuestionYesNo(IWin32Window owner, string msg)
        {
            return (ShowMessage(owner, msg, APP_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
        }

        public static bool ShowMessageQuestionOKCancel(IWin32Window owner, string msg)
        {
            return (ShowMessage(owner, msg, APP_TITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK);
        }
        #endregion
    }
}
