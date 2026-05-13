namespace IndustrialControls.Template.Pages
{
    partial class DataVisualizationPage
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox groupBoxCharts;
        private IndustrialControls.Controls.DataVisualization.TrendChart trendChart1;
        private IndustrialControls.Controls.DataVisualization.GaugeControl gaugeTemperature;
        private IndustrialControls.Controls.DataVisualization.GaugeControl gaugePressure;
        private IndustrialControls.Controls.DataVisualization.IndustrialProgressBar industrialProgressBar1;
        private System.Windows.Forms.Label descLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.descLabel = new System.Windows.Forms.Label();
            this.groupBoxCharts = new System.Windows.Forms.GroupBox();
            this.industrialProgressBar1 = new IndustrialControls.Controls.DataVisualization.IndustrialProgressBar();
            this.gaugePressure = new IndustrialControls.Controls.DataVisualization.GaugeControl();
            this.gaugeTemperature = new IndustrialControls.Controls.DataVisualization.GaugeControl();
            this.trendChart1 = new IndustrialControls.Controls.DataVisualization.TrendChart();
            this.groupBoxCharts.SuspendLayout();
            this.SuspendLayout();
            // 
            // descLabel
            // 
            this.descLabel.AutoSize = true;
            this.descLabel.Location = new System.Drawing.Point(16, 12);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(377, 12);
            this.descLabel.TabIndex = 0;
            this.descLabel.Text = "数据可视化：趋势图、仪表盘、进度条（数据来自 MockDataService）";
            // 
            // groupBoxCharts
            // 
            this.groupBoxCharts.Controls.Add(this.industrialProgressBar1);
            this.groupBoxCharts.Controls.Add(this.gaugePressure);
            this.groupBoxCharts.Controls.Add(this.gaugeTemperature);
            this.groupBoxCharts.Controls.Add(this.trendChart1);
            this.groupBoxCharts.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxCharts.Location = new System.Drawing.Point(0, 27);
            this.groupBoxCharts.Name = "groupBoxCharts";
            this.groupBoxCharts.Size = new System.Drawing.Size(900, 493);
            this.groupBoxCharts.TabIndex = 1;
            this.groupBoxCharts.TabStop = false;
            this.groupBoxCharts.Text = "实时数据";
            // 
            // industrialProgressBar1
            // 
            this.industrialProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.industrialProgressBar1.Location = new System.Drawing.Point(450, 280);
            this.industrialProgressBar1.Name = "industrialProgressBar1";
            this.industrialProgressBar1.Size = new System.Drawing.Size(430, 36);
            this.industrialProgressBar1.TabIndex = 3;
            // 
            // gaugePressure
            // 
            this.gaugePressure.Location = new System.Drawing.Point(230, 252);
            this.gaugePressure.Name = "gaugePressure";
            this.gaugePressure.Size = new System.Drawing.Size(200, 200);
            this.gaugePressure.TabIndex = 2;
            // 
            // gaugeTemperature
            // 
            this.gaugeTemperature.Location = new System.Drawing.Point(14, 252);
            this.gaugeTemperature.Name = "gaugeTemperature";
            this.gaugeTemperature.Size = new System.Drawing.Size(200, 200);
            this.gaugeTemperature.TabIndex = 1;
            // 
            // trendChart1
            // 
            this.trendChart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trendChart1.CornerRadius = 6;
            this.trendChart1.Location = new System.Drawing.Point(14, 22);
            this.trendChart1.Name = "trendChart1";
            this.trendChart1.Size = new System.Drawing.Size(866, 220);
            this.trendChart1.TabIndex = 0;
            // 
            // DataVisualizationPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxCharts);
            this.Controls.Add(this.descLabel);
            this.Name = "DataVisualizationPage";
            this.Size = new System.Drawing.Size(900, 520);
            this.Load += new System.EventHandler(this.DataVisualizationPage_Load);
            this.groupBoxCharts.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
