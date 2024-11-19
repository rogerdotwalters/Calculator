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

namespace Calculator {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        double lastNumber, result;
        SelectedOperator selectedOperator;
        
        public MainWindow() {
            InitializeComponent();

            buttonAC.Click += ButtonAC_Click;
            buttonPositiveNegative.Click += ButtonPositiveNegative_Click;
            buttonPercent.Click += ButtonPercent_Click;
            buttonEquals.Click += ButtonEquals_Click;
            buttonDecimal.Click += ButtonDecimal_Click;
        }

        private void ButtonEquals_Click(object sender, RoutedEventArgs e) {

            double currentNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out currentNumber)) {

                switch (selectedOperator) {

                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multiply(lastNumber, currentNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Divide(lastNumber, currentNumber);
                        break;
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(lastNumber, currentNumber);
                        break;
                    case SelectedOperator.Subtraction:
                        result = SimpleMath.Subtract(lastNumber, currentNumber);
                        break;
                }

                lastNumberLabel.Content = "";
                resultLabel.Content = result.ToString();
            }
        }

        private void ButtonPercent_Click(object sender, RoutedEventArgs e) {

            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber)) {

                if (lastNumber != 0) {

                    lastNumber /= 100;
                    resultLabel.Content = lastNumber.ToString();
                }
            }
        }

        private void ButtonPositiveNegative_Click(object sender, RoutedEventArgs e) {

            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber)) {

                if (lastNumber != 0) {
                    lastNumber = lastNumber * -1;
                    resultLabel.Content = lastNumber.ToString();
                }
            }
        }

        private void ButtonAC_Click(object sender, RoutedEventArgs e) {

            resultLabel.Content = "0";
            lastNumberLabel.Content = "";
            operatorLabel.Content = "";
            selectedOperator = new SelectedOperator();
        }

        private void ButtonDecimal_Click(object sender, RoutedEventArgs e) {

            if (resultLabel.Content.ToString().Contains(".")) {

                //Do Nothing
            } else {
                resultLabel.Content = $"{resultLabel.Content}.";
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e) {

            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber)) {
                
                lastNumberLabel.Content = lastNumber.ToString();
                resultLabel.Content = "0";
            }

            if (sender == buttonMultiply) {

                selectedOperator = SelectedOperator.Multiplication;
                operatorLabel.Content = "*";
            }

            if (sender == buttonDivide) {

                selectedOperator = SelectedOperator.Division;
                operatorLabel.Content = "/";
            }

            if (sender == buttonAdd) {

                selectedOperator = SelectedOperator.Addition;
                operatorLabel.Content = "+";
            }

            if (sender == buttonSubtract) {

                selectedOperator = SelectedOperator.Subtraction;
                operatorLabel.Content = "-";
            }
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e) {

            //int selectedValue = 0;

            //Button selectedButton = (Button)sender;

            //if (int.TryParse(selectedButton.Content.ToString(), out int value)) {

            //    selectedValue = value;
            //}


            int selectedValue = int.Parse((sender as Button).Content.ToString());


            if (resultLabel.Content.ToString() == "0") {

                resultLabel.Content = selectedValue;
            } else {

                resultLabel.Content = $"{resultLabel.Content}{selectedValue}";
            }
        }
    }

    public enum SelectedOperator {

        Addition,
        Subtraction,
        Multiplication,
        Division,
    }

    public class SimpleMath {

        public static double Add(double n1, double n2) {

            return n1 + n2;
        }
        public static double Subtract(double n1, double n2) {

            return n1 - n2;
        }

        public static double Multiply(double n1, double n2) {

            return n1 * n2;
        }

        public static double Divide(double n1, double n2) {

            if (n2 == 0) {

                MessageBox.Show("User Error: Can not divide by 0.", "Invalid Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            return n1 / n2;
        }

    }
}