﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Reflection;
using System.IO;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using MatlabData;
using MatlabDataNative;
using System.ComponentModel;

using ImPort;
using System.Runtime.InteropServices;

using Agilent.AgXSAn.Interop;
using Ivi.Driver.Interop;
using System.Globalization;
using System.Data;



namespace CodeForPractice
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        [DllImport("E:\\JiweiSung\\VisualStudioProject2017\\CodeForPractice\\x64\\Release\\CodeForCppDll.dll", CharSet = CharSet.Ansi)]
        private extern static double TestMethod_Array(double[] _array, int _size);

        [DllImport("E:\\JiweiSung\\VisualStudioProject2017\\CodeForPractice\\x64\\Release\\CodeForCppDll.dll", CharSet = CharSet.Ansi)]
        private extern static int TryCatchTest(int _choice);

        private BackgroundWorker bgWorker = new BackgroundWorker();
        private BackgroundWorker FuncBgWorker = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();
            InitializeBgWorker();
            InitializeBarProcess();
            InitializeFuncBgWorker();
        }

        private void InitializeBarProcess()
        {
            BarProcess.Maximum = 1000;
        }

        private void InitializeBgWorker()
        {
            //初始化worker属性，订阅事件
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.ProgressChanged += BgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
        }

        private void InitializeFuncBgWorker()
        {
            FuncBgWorker.WorkerReportsProgress = true;
            FuncBgWorker.WorkerSupportsCancellation = true;
            FuncBgWorker.DoWork += FuncBgWorker_DoWork;
            FuncBgWorker.ProgressChanged += FuncBgWorker_ProgressChanged;
            FuncBgWorker.RunWorkerCompleted += FuncBgWorker_RunWorkerCompleted;
        }

        private void FuncBgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FuncBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FuncBgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            //点击事件里检查进程是否忙，如果不忙测异步调用进程
            if (bgWorker.IsBusy)
            {
                Log(1, "Busy !");
            }
            else
            {
                bgWorker.RunWorkerAsync();
            }
        }
        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            //异步取消请求，需在耗时程序内检测取消请求
            bgWorker.CancelAsync();
        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //耗时程序需要在DoWork事件里进行，并检测异步取消请求，禁止在DoWork事件里操作UI
            for (int i = 0; i < 1000; i++)
            {
                if (bgWorker.CancellationPending)
                {
                    e.Cancel = true;
                }
                else
                {
                    bgWorker.ReportProgress(i + 1, "Processing");
                    System.Threading.Thread.Sleep(10);
                }
            }
        }

        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //在ProgressChanged事件里处理，状态上报，进程百分比和用户状态
            BarProcess.Value = e.ProgressPercentage;
            LbProcess.Content = e.UserState.ToString() + " " + e.ProgressPercentage.ToString() + " %";
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //在completed事件里，处理异步取消、处理结束错误、处理正常结束逻辑
            if (e.Cancelled)
            {
                Log(1, "Process Cancelled !");
                BarProcess.Value = 0;
                LbProcess.Content = "Processing 0 %";
            }
            else
            {
                if (e.Error != null)
                {
                    MessageBox.Show(e.Error.ToString());
                }
                else
                {
                    Log(1, "Process Finished !");
                }
            }

        }

        private void Log(int _re_print, string _log)
        {
            switch (_re_print)
            {
                case 0:
                    TbLog.Clear();
                    TbLog.Text = DateTime.Now.ToString("yy-mm-dd hh-mm-ss") + "-> " + _log + "\n";
                    break;
                case 1:
                    TbLog.AppendText(DateTime.Now.ToString("yy-mm-dd hh-mm-ss") + "-> " + _log + "\n");
                    break;
                default:
                    TbLog.AppendText(DateTime.Now.ToString("yy-mm-dd hh-mm-ss") + "-> " + _log + "\n");
                    break;
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //在窗口关闭逻辑内，取消事件订阅
            bgWorker.DoWork -= BgWorker_DoWork;
            bgWorker.ProgressChanged -= BgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted -= BgWorker_RunWorkerCompleted;
        }

        private void BtnContinue_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SubWindow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MathNetTest_Click(object sender, RoutedEventArgs e)
        {
            MWCharArray fileDir = "E:\\JiweiSung\\VisualStudioProject2017\\CodeForPractice\\";
            MWCharArray fileName = "20180527";

            MWNumericArray profile = new int[] { 6, 32, 871, 1205, 1972, 2781, 8192 };
            MWNumericArray withMissTone = 0;
            MWNumericArray missToneList = new int[] { 1000, 2000 };
            MWNumericArray upLimitPar = 16;
            MWNumericArray lowLimitPar = 13;
            MWNumericArray maxVoltage = 1;
            MWNumericArray ifftSize = 65536;
            MWNumericArray bitWidth = 16;
            MWNumericArray instrPar = 16;
            MWNumericArray withFile = 1;

            MatlabData.N8241aWfm n8241aData = new MatlabData.N8241aWfm();

            MWArray[] realPar = n8241aData.DslWfmDataConfig(2, profile, withMissTone, missToneList, upLimitPar, lowLimitPar, maxVoltage, fileDir, fileName, ifftSize, bitWidth, instrPar, withFile);

            Log(1, realPar[0].ToString());
            Log(1, realPar[1].ToString());
        }

        private void ButtonN9030aTest_Click(object sender, RoutedEventArgs e)
        {
            //AgXSAn n9030a = new AgXSAn();
            //try
            //{
            //    n9030a.Initialize("TCPIP0::192.168.2.100::inst0::INSTR", true, true, "");
            //}
            //catch (Exception)
            //{
            //    Log(1, "Error : " + e.ToString());
            //}

            //int _code = 0;
            //string _message = null;
            //n9030a.Utility.ErrorQuery(ref _code, ref _message);
            try
            {
                TryCatchTestMethod(0);
            }
            catch (Exception)
            {
                Log(1, "Error : " + e.ToString());
            }
        }

        private void TryCatchTestMethod(int _choice)
        {
            if (TryCatchTest(_choice) != 0)
            {
                throw new Exception("Wrong !");
            }
            else
            {
                Log(1, "right !");
            }
        }

        private void ButtonTest0_Click(object sender, RoutedEventArgs e)
        {
            NotesDataTableProcess();
        }

        private void NotesStringProcess()
        {
            //功能：字符串转换为整型
            //依赖：using System.Globalization;
            string varX00 = "2";
            int varY00 = int.Parse(varX00, NumberStyles.Any);

            //功能：字符串转换为数组
            string varX11 = "2 3 4";
            string[] varY11 = varX11.Split(' ');
            foreach (var item in varY11)
            {

            }
        }

        private void NotesDataTableProcess()
        {
            DataTable mtprDataTable = new DataTable();
            List<int> missToneList = new List<int>() { 1, 2, 3, 4, 5 };

            foreach (var item in missToneList)
            {
                mtprDataTable.Columns.Add(new DataColumn("column" + item.ToString(), typeof(double)));
            }

            for (int i = 0; i < 2; i++)
            {
                List<double> mtprList = new List<double>() { 1, 2, 3, 4, 5 };

                object[] values = new object[mtprList.Count];


                for (int j = 0; j < mtprList.Count; j++)
                {
                    values[j] = mtprList[j];
                }

                mtprDataTable.Rows.Add(values);
            }
            object result = mtprDataTable.Compute("Var(column1)", string.Empty);
            Log(1, result.ToString());
        }
    }
}
