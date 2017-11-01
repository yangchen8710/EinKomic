using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EinKomic.Model
{
    class UtilHelper
    {
        public static Boolean GetFolder(out string path)
        {
            path = "";
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "Please Select Folder";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    return false;
                }
                path = dialog.SelectedPath;
                return true;
            }
            return false;
        }

    }
}
