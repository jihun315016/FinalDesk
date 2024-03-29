﻿using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESK_MES
{
    public partial class ReportPreviewForm : Form
    {
        public ReportPreviewForm(XtraReportBarcord rpt)
        {
            InitializeComponent();

            using (ReportPrintTool tool = new ReportPrintTool(rpt))
            {
                tool.ShowPreviewDialog();
            }
        }
    }
}
