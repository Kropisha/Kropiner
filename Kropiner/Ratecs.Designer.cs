﻿namespace Kropiner
{
    partial class Ratecs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ratecs));
            this.KROPINERPROBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSet1 = new Kropiner.DataSet1();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.KROPINERPROTableAdapter = new Kropiner.DataSet1TableAdapters.KROPINERPROTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.KROPINERPROBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // KROPINERPROBindingSource
            // 
            this.KROPINERPROBindingSource.DataMember = "KROPINERPRO";
            this.KROPINERPROBindingSource.DataSource = this.DataSet1;
            // 
            // DataSet1
            // 
            this.DataSet1.DataSetName = "DataSet1";
            this.DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.KROPINERPROBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Kropiner.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(3, 86);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(559, 414);
            this.reportViewer1.TabIndex = 0;
            // 
            // KROPINERPROTableAdapter
            // 
            this.KROPINERPROTableAdapter.ClearBeforeFill = true;
            // 
            // Ratecs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(581, 591);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Ratecs";
            this.Text = "Rate";
            this.Load += new System.EventHandler(this.Ratecs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.KROPINERPROBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource KROPINERPROBindingSource;
        private DataSet1 DataSet1;
        private DataSet1TableAdapters.KROPINERPROTableAdapter KROPINERPROTableAdapter;
    }
}