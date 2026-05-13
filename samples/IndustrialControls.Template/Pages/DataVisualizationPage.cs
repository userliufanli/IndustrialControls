using System;
using System.Drawing;
using System.Windows.Forms;
using IndustrialControls.Core;
using IndustrialControls.Template.Core;

namespace IndustrialControls.Template.Pages
{
    public partial class DataVisualizationPage : UserControl
    {
        private Timer _timer;

        public DataVisualizationPage()
        {
            InitializeComponent();
        }

        private void DataVisualizationPage_Load(object sender, EventArgs e)
        {
            if (ControlDesignerHelper.IsDesignMode(this))
                return;
            TemplateUiChrome.ApplyPage(this);
            TemplateUiChrome.StyleGroupBox(groupBoxCharts);

            trendChart1.Title = "温度趋势";
            trendChart1.AddChannel("温度", Color.OrangeRed);
            trendChart1.ShowGrid = true;
            trendChart1.AutoScaleY = true;

            gaugeTemperature.Minimum = 0;
            gaugeTemperature.Maximum = 120;
            gaugeTemperature.Unit = "°C";

            gaugePressure.Minimum = 0;
            gaugePressure.Maximum = 1.5;
            gaugePressure.Unit = "MPa";

            industrialProgressBar1.Minimum = 0;
            industrialProgressBar1.Maximum = 100;

            _timer = new Timer { Interval = 500 };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var d = TemplateRuntime.Data.GetSnapshot();
            gaugeTemperature.Value = d.Temperature;
            gaugePressure.Value = d.Pressure;
            industrialProgressBar1.Value = Math.Min(100, d.Temperature);
            trendChart1.AddDataPoint("温度", d.Temperature);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
            base.OnHandleDestroyed(e);
        }
    }
}
