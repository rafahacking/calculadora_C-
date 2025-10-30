using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalculadoraApp
{
    public class Calculadora : Form
    {
        // UI Components
        private TextBox displayTextBox;
        private Button[] numberButtons;
        private Button btnAdd, btnSubtract, btnMultiply, btnDivide;
        private Button btnEquals, btnClear, btnClearEntry, btnDecimal;
        private Button btnBackspace, btnPlusMinus, btnSquareRoot, btnPercent;
        
        // Calculator logic variables
        private double currentValue = 0;
        private double storedValue = 0;
        private string currentOperation = "";
        private bool operationPressed = false;
        private bool newCalculation = true;

        public Calculadora()
        {
            InitializeComponents();
            SetupLayout();
        }

        private void InitializeComponents()
        {
            // Form properties
            this.Text = "Calculadora";
            this.Size = new Size(350, 500);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Display TextBox
            displayTextBox = new TextBox
            {
                Location = new Point(20, 20),
                Size = new Size(300, 50),
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Right,
                ReadOnly = true,
                Text = "0",
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(displayTextBox);

            // Initialize number buttons (0-9)
            numberButtons = new Button[10];
            for (int i = 0; i < 10; i++)
            {
                numberButtons[i] = CreateButton(i.ToString(), Color.FromArgb(250, 250, 250));
                numberButtons[i].Click += NumberButton_Click;
            }

            // Operation buttons
            btnAdd = CreateButton("+", Color.FromArgb(255, 159, 10));
            btnAdd.Click += OperationButton_Click;
            
            btnSubtract = CreateButton("-", Color.FromArgb(255, 159, 10));
            btnSubtract.Click += OperationButton_Click;
            
            btnMultiply = CreateButton("×", Color.FromArgb(255, 159, 10));
            btnMultiply.Click += OperationButton_Click;
            
            btnDivide = CreateButton("÷", Color.FromArgb(255, 159, 10));
            btnDivide.Click += OperationButton_Click;

            // Special function buttons
            btnEquals = CreateButton("=", Color.FromArgb(255, 159, 10));
            btnEquals.Click += BtnEquals_Click;
            
            btnClear = CreateButton("C", Color.FromArgb(165, 165, 165));
            btnClear.Click += BtnClear_Click;
            
            btnClearEntry = CreateButton("CE", Color.FromArgb(165, 165, 165));
            btnClearEntry.Click += BtnClearEntry_Click;
            
            btnBackspace = CreateButton("⌫", Color.FromArgb(165, 165, 165));
            btnBackspace.Click += BtnBackspace_Click;
            
            btnDecimal = CreateButton(".", Color.FromArgb(250, 250, 250));
            btnDecimal.Click += BtnDecimal_Click;
            
            btnPlusMinus = CreateButton("±", Color.FromArgb(165, 165, 165));
            btnPlusMinus.Click += BtnPlusMinus_Click;
            
            btnSquareRoot = CreateButton("√", Color.FromArgb(165, 165, 165));
            btnSquareRoot.Click += BtnSquareRoot_Click;
            
            btnPercent = CreateButton("%", Color.FromArgb(165, 165, 165));
            btnPercent.Click += BtnPercent_Click;
        }

        private Button CreateButton(string text, Color backColor)
        {
            Button btn = new Button
            {
                Text = text,
                Size = new Size(70, 60),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                BackColor = backColor,
                ForeColor = text == "+" || text == "-" || text == "×" || text == "÷" || text == "=" 
                    ? Color.White : Color.Black,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void SetupLayout()
        {
            int startX = 20;
            int startY = 90;
            int buttonSpacing = 75;

            // Row 1: C, CE, ⌫, ÷
            btnClear.Location = new Point(startX, startY);
            this.Controls.Add(btnClear);
            
            btnClearEntry.Location = new Point(startX + buttonSpacing, startY);
            this.Controls.Add(btnClearEntry);
            
            btnBackspace.Location = new Point(startX + buttonSpacing * 2, startY);
            this.Controls.Add(btnBackspace);
            
            btnDivide.Location = new Point(startX + buttonSpacing * 3, startY);
            this.Controls.Add(btnDivide);

            // Row 2: 7, 8, 9, ×
            numberButtons[7].Location = new Point(startX, startY + buttonSpacing);
            this.Controls.Add(numberButtons[7]);
            
            numberButtons[8].Location = new Point(startX + buttonSpacing, startY + buttonSpacing);
            this.Controls.Add(numberButtons[8]);
            
            numberButtons[9].Location = new Point(startX + buttonSpacing * 2, startY + buttonSpacing);
            this.Controls.Add(numberButtons[9]);
            
            btnMultiply.Location = new Point(startX + buttonSpacing * 3, startY + buttonSpacing);
            this.Controls.Add(btnMultiply);

            // Row 3: 4, 5, 6, -
            numberButtons[4].Location = new Point(startX, startY + buttonSpacing * 2);
            this.Controls.Add(numberButtons[4]);
            
            numberButtons[5].Location = new Point(startX + buttonSpacing, startY + buttonSpacing * 2);
            this.Controls.Add(numberButtons[5]);
            
            numberButtons[6].Location = new Point(startX + buttonSpacing * 2, startY + buttonSpacing * 2);
            this.Controls.Add(numberButtons[6]);
            
            btnSubtract.Location = new Point(startX + buttonSpacing * 3, startY + buttonSpacing * 2);
            this.Controls.Add(btnSubtract);

            // Row 4: 1, 2, 3, +
            numberButtons[1].Location = new Point(startX, startY + buttonSpacing * 3);
            this.Controls.Add(numberButtons[1]);
            
            numberButtons[2].Location = new Point(startX + buttonSpacing, startY + buttonSpacing * 3);
            this.Controls.Add(numberButtons[2]);
            
            numberButtons[3].Location = new Point(startX + buttonSpacing * 2, startY + buttonSpacing * 3);
            this.Controls.Add(numberButtons[3]);
            
            btnAdd.Location = new Point(startX + buttonSpacing * 3, startY + buttonSpacing * 3);
            this.Controls.Add(btnAdd);

            // Row 5: ±, 0, ., =
            btnPlusMinus.Location = new Point(startX, startY + buttonSpacing * 4);
            this.Controls.Add(btnPlusMinus);
            
            numberButtons[0].Location = new Point(startX + buttonSpacing, startY + buttonSpacing * 4);
            this.Controls.Add(numberButtons[0]);
            
            btnDecimal.Location = new Point(startX + buttonSpacing * 2, startY + buttonSpacing * 4);
            this.Controls.Add(btnDecimal);
            
            btnEquals.Location = new Point(startX + buttonSpacing * 3, startY + buttonSpacing * 4);
            this.Controls.Add(btnEquals);
        }

        // Event Handlers
        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            
            if (displayTextBox.Text == "0" || operationPressed || newCalculation)
            {
                displayTextBox.Text = btn.Text;
                operationPressed = false;
                newCalculation = false;
            }
            else
            {
                displayTextBox.Text += btn.Text;
            }
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            
            if (!string.IsNullOrEmpty(currentOperation) && !operationPressed)
            {
                PerformCalculation();
            }
            
            storedValue = double.Parse(displayTextBox.Text);
            currentOperation = btn.Text;
            operationPressed = true;
            newCalculation = false;
        }

        private void BtnEquals_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentOperation))
            {
                PerformCalculation();
                currentOperation = "";
                newCalculation = true;
            }
        }

        private void PerformCalculation()
        {
            try
            {
                currentValue = double.Parse(displayTextBox.Text);
                double result = 0;

                switch (currentOperation)
                {
                    case "+":
                        result = storedValue + currentValue;
                        break;
                    case "-":
                        result = storedValue - currentValue;
                        break;
                    case "×":
                        result = storedValue * currentValue;
                        break;
                    case "÷":
                        if (currentValue != 0)
                            result = storedValue / currentValue;
                        else
                        {
                            MessageBox.Show("Não é possível dividir por zero!", "Erro", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            BtnClear_Click(null, null);
                            return;
                        }
                        break;
                }

                displayTextBox.Text = result.ToString();
                storedValue = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no cálculo: " + ex.Message, "Erro", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                BtnClear_Click(null, null);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            displayTextBox.Text = "0";
            currentValue = 0;
            storedValue = 0;
            currentOperation = "";
            operationPressed = false;
            newCalculation = true;
        }

        private void BtnClearEntry_Click(object sender, EventArgs e)
        {
            displayTextBox.Text = "0";
        }

        private void BtnBackspace_Click(object sender, EventArgs e)
        {
            if (displayTextBox.Text.Length > 1)
            {
                displayTextBox.Text = displayTextBox.Text.Substring(0, displayTextBox.Text.Length - 1);
            }
            else
            {
                displayTextBox.Text = "0";
            }
        }

        private void BtnDecimal_Click(object sender, EventArgs e)
        {
            if (!displayTextBox.Text.Contains("."))
            {
                displayTextBox.Text += ".";
            }
        }

        private void BtnPlusMinus_Click(object sender, EventArgs e)
        {
            if (displayTextBox.Text != "0")
            {
                double value = double.Parse(displayTextBox.Text);
                displayTextBox.Text = (-value).ToString();
            }
        }

        private void BtnSquareRoot_Click(object sender, EventArgs e)
        {
            try
            {
                double value = double.Parse(displayTextBox.Text);
                if (value >= 0)
                {
                    displayTextBox.Text = Math.Sqrt(value).ToString();
                    newCalculation = true;
                }
                else
                {
                    MessageBox.Show("Não é possível calcular raiz quadrada de número negativo!", 
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPercent_Click(object sender, EventArgs e)
        {
            try
            {
                double value = double.Parse(displayTextBox.Text);
                displayTextBox.Text = (value / 100).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Calculadora());
        }
    }
}