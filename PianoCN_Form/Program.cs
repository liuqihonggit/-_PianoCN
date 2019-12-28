#if lnk
using Shell32; // Microsoft Shell Controls And Automation 如果运行的时候出错,找不到,请去vs修复,添加net的环境  
#endif
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;


namespace PlotFactory
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // "C:\Users\54076\AppData\Roaming\Autodesk\AutoCAD 2019\R23.0\chs\Plotters\DWG To PDF.pc3"
            // "D:\桌面\DWG To PDF.pc3 - 快捷方式.lnk"
            // "D:\桌面\DWG To PDF.pc3"

            string path = "";

            //如果有参数,就执行不同的操作 
            if (args != null && args.Length > 0)
            {
                //得到快捷方式属性 
                path = args[0];
                var ext = Path.GetExtension(path);
                if (!string.IsNullOrEmpty(ext))
                {
                    ext = ext.ToLower();
                    if (ext == ".lnk")
                    {
#if lnk
                        path = GetShortcutTargetFile(path);
#endif
                    }
                }
            }
            if (!string.IsNullOrEmpty(path))//目标路径不是空的 
            {
                var ext = Path.GetExtension(path).ToLower();
                if (!(ext == ".pc3" || ext == ".pmp" || ext == ".ctb"))
                {
                    path = "";
                }
            }
            Application.Run(new PiaForm(path));
        }

#if lnk
        //https://codeday.me/bug/20181107/359206.html
        /// <summary>
        /// 获取快捷方式的目标
        /// </summary>
        /// <param name="shortcutFilename"></param>
        /// <returns></returns>
        public static string GetShortcutTargetFile(string shortcutFilename)
        {
            string pathOnly = Path.GetDirectoryName(shortcutFilename);
            string filenameOnly = Path.GetFileName(shortcutFilename);

            Shell shell = new Shell();
            Folder folder = shell.NameSpace(pathOnly);
            FolderItem folderItem = folder.ParseName(filenameOnly);
            if (folderItem != null)
            {
                ShellLinkObject link = (ShellLinkObject)folderItem.GetLink;
                return link.Path;
            }

            return string.Empty;
        }

        class 创建快捷方式
        {
            // https://xkonglong.com/creatshortcut/
            [ComImport]
            [Guid("00021401-0000-0000-C000-000000000046")]
            internal class ShellLink
            {
            }
            [ComImport]
            [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            [Guid("000214F9-0000-0000-C000-000000000046")]
            internal interface IShellLink
            {

                void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);
                void GetIDList(out IntPtr ppidl);
                void SetIDList(IntPtr pidl);
                void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
                void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
                void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
                void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
                void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
                void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
                void GetHotkey(out short pwHotkey);
                void SetHotkey(short wHotkey);
                void GetShowCmd(out int piShowCmd);
                void SetShowCmd(int iShowCmd);
                void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
                void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
                void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
                void Resolve(IntPtr hwnd, int fFlags);
                void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
            }
            private void creatShortcut()
            {
                IShellLink link = (IShellLink)new ShellLink();
                link.SetDescription("将文件拖放到本快捷方式即可实现文件分拣.");
                link.SetPath(Application.ExecutablePath); //指定文件路径

                IPersistFile file = (IPersistFile)link;
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                file.Save(Path.Combine(desktopPath, "文件自动分拣.lnk"), false);  //快捷方式保存到桌面
            }

        }

#endif
    }
}
