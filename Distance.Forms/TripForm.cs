using System.Globalization;
using Distance.Services;

namespace Distance.Forms {
    public partial class TripForm : Form {
        private readonly DistanceService _distanceService;

        public TripForm() {
            _distanceService = new DistanceService();
            InitializeComponent();
        }

        private void computeButton_Click(object? sender, EventArgs e) {
            double distance = 0.0;
            int passengers = 0;

            if ((distanceTextBox is not null) && (!double.TryParse(distanceTextBox.Text, out distance) || (distance < 5.0))) {
                MessageBox.Show("Please enter a valid distance (≥ 5.0 km).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((passengersTextBox is not null) && (!int.TryParse(passengersTextBox.Text, out passengers) || (passengers < 1) || (passengers > 25))) {
                MessageBox.Show("Passengers must be between 1 and 25.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool includeRests = (restCheckBox is not null) ? restCheckBox.Checked : false;
            double cost = 0.0;
            try {
                cost = _distanceService.TotalTripCost(distance, passengers, includeRests);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (resultLabel is null) return;
            resultLabel.Text = $"Total Trip Cost: {cost.ToString("C2", CultureInfo.CurrentCulture)}";
        }

        private TextBox? distanceTextBox;
        private TextBox? passengersTextBox;
        private CheckBox? restCheckBox;
        private Button? computeButton;
        private Label? resultLabel;

        private void InitializeComponent() {
            this.distanceTextBox = new TextBox { Left = 150, Top = 20, Width = 100 };
            this.passengersTextBox = new TextBox { Left = 150, Top = 60, Width = 100 };
            this.restCheckBox = new CheckBox { Left = 150, Top = 100, Text = "Include Rests" };
            this.computeButton = new Button {
                Left = 150,
                Top = 140,
                Width = 150,           
                Height = 35,           
                Text = "Compute Trip",
            };

            this.resultLabel = new Label { Left = 150, Top = 180, Width = 250, Height = 30, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            var distanceLabel = new Label { Left = 20, Top = 20, Text = "Distance (km):", AutoSize = true };
            var passengerLabel = new Label { Left = 20, Top = 60, Text = "Passengers (1-25):", AutoSize = true };

            this.computeButton.Click += computeButton_Click;

            this.Controls.Add(distanceLabel);
            this.Controls.Add(passengerLabel);
            this.Controls.Add(distanceTextBox);
            this.Controls.Add(passengersTextBox);
            this.Controls.Add(restCheckBox);
            this.Controls.Add(computeButton);
            this.Controls.Add(resultLabel);

            this.Text = "Trip Cost Calculator";
            this.MinimumSize = new Size(400, 250);
            this.MaximumSize = new Size(450, 275);
            this.ClientSize = new Size(400, 250);
        }
    }
}
