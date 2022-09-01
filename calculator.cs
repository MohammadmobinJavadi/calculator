using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator
{
    public class Number
    {
        public bool HasPoint { get { return _haspoint; } }
        public double GetNumber { get { return _number; } }
        public string GetNumerString { get { return (_haspoint && !(_countAshari>0)) ? _number.ToString() + "." : _number.ToString(); } }
        bool _haspoint = false;
        int _countAshari = 0;
        double _number = 0;
        public double InsertNumber(string text)
        {
            if (text == ".")
            {
                if (_haspoint || _countAshari>0)
                {
                    return _number;
                }
                _haspoint = true;
            }
            else
            {
                if (_haspoint|| _countAshari>0)
                {
                    _countAshari++;
                    _haspoint = false;
                    _number += double.Parse(text) / Math.Pow(10.0,_countAshari);
                }
                else
                {
                    _number = _number * 10 + int.Parse(text);
                }
            }
            return _number;
        }
        public void Clear()
        {
            _number = 0;
            _countAshari = 0;
            _haspoint = false;
        }
    }
    public enum enOperand
    {
        notset=0,
        Plus=1,
        Minus=2,
        MultiPly=3,
        Divide=4,
    }
    public partial class calculator : Form
    {
        Number leftnum = new Number();
        Number rightnum = new Number();
        enOperand Operand = enOperand.notset;
        bool isleftfinished = false;
        public calculator()
        {
            InitializeComponent();
        }
        double num1, num2, t, m, j, z;
        bool eat1, eat2, eat3, eat4;

        private void butt_click(object sender, EventArgs e)
        {
            Operand = enOperand.Divide;
            isleftfinished = true;

            //if (textBox.Text != "")
            //{

            //    num1 = Convert.ToDouble(textBox.Text);
            //    textBox.Text = "";
            //    eat4 = true;
            //}
        }

        private void button19_Click(object sender, EventArgs e)
        {


            if (!isleftfinished)
            {
                leftnum.InsertNumber(((Button)sender).Text);
                textBox.Text = leftnum.GetNumerString;
                //  textBox.Text = leftnum + ".";
            }
            else
            {
                rightnum.InsertNumber(((Button)sender).Text);
                textBox.Text = rightnum.GetNumerString;
                //textBox.Text = rightnum + ".";
            }

        }

        private void del_click(object sender, EventArgs e)
        {
            history.Items.Clear();
        }

        private void butz_click(object sender, EventArgs e)
        {
            Operand = enOperand.MultiPly;
            isleftfinished = true;
            //if (textBox.Text != "")
            //{

            //    num1 = Convert.ToDouble(textBox.Text);
            //    textBox.Text = "";
            //    eat3 = true;
            //}
        }

        private void btbNumber_Click(object sender, EventArgs e)
        {
            if (!isleftfinished)
            {
                leftnum.InsertNumber(((Button)sender).Text);
                textBox.Text =leftnum.GetNumerString;
            }
            else
            {
                rightnum.InsertNumber(((Button)sender).Text);
                textBox.Text = rightnum.GetNumerString;
            }
            //leftnum.InsertNumber(((Button)sender).Text);
            //UpdateTextBox(((Button)sender).Text);
        }

        private void UpdateTextBox(string text)
        {
            textBox.Text += text;
        }

        private void but_Click(object sender, EventArgs e)
        {
            switch (Operand)
            {
                case enOperand.notset:
                    break;
                case enOperand.Plus:
                    textBox.Text = (leftnum.GetNumber+rightnum.GetNumber).ToString();
                    j = leftnum.GetNumber + rightnum.GetNumber;
                    textBox.Text = j.ToString();
                    history.Items.Add(leftnum.GetNumber + "+" + rightnum.GetNumber + "" + "=" + j);
                    break;
                case enOperand.Minus:
                    textBox.Text = (leftnum.GetNumber-rightnum.GetNumber).ToString();
                    m = leftnum.GetNumber - rightnum.GetNumber;
                    textBox.Text = m.ToString();
                    history.Items.Add(leftnum.GetNumber + "-" + rightnum.GetNumber + "" + "=" + m);
                    break;
                case enOperand.MultiPly:
                    textBox.Text = (leftnum.GetNumber*rightnum.GetNumber).ToString();
                    z = leftnum.GetNumber * rightnum.GetNumber;
                    textBox.Text = z.ToString();
                    history.Items.Add(leftnum.GetNumber + "*" + rightnum.GetNumber + "" + "=" + z);
                    break;
                case enOperand.Divide:
                    t = leftnum.GetNumber / num2;
                    textBox.Text = t.ToString();
                    history.Items.Add(leftnum.GetNumber + "/" + rightnum.GetNumber + "" + "=" + t);
                    if (rightnum.GetNumber==0)
                    {
                        textBox.Text = "Infinity";
                    }
                    else
                    {
                        textBox.Text = (leftnum.GetNumber / rightnum.GetNumber).ToString();
                    }
                    break;
                default:
                    break;
            }
            isleftfinished = false;
            leftnum.Clear();
            rightnum.Clear();
            //if (textBox.Text != "")
            //{

            //    num2 = Convert.ToDouble(textBox.Text);
            //    if (eat1 == true)
            //    {
            //        j = num1 + num2;
            //        textBox.Text = j.ToString();
            //        history.Items.Add(num1 + "+" + num2 + "" + "=" + j);
            //        eat1 = false;

            //    }
            //    else if (eat2 == true)
            //    {
            //        m = num1 - num2;
            //        textBox.Text = m.ToString();
            //        history.Items.Add(num1 + "-" + num2 + "" + "=" + m);
            //        eat2 = false;
            //    }
            //    else if (eat3 == true)
            //    {
            //        z = num1 * num2;
            //        textBox.Text = z.ToString();
            //        history.Items.Add(num1 + "*" + num2 + "" + "=" + z);
            //        eat3 = false;
            //    }
            //    else if (eat4 == true)
            //    {
            //        t = num1 / num2;
            //        textBox.Text = t.ToString();
            //        history.Items.Add(num1 + "/" + num2 + "" + "=" + t);
            //        eat4 = false;
            //    }
            //}
        }

        private void butclear_click(object sender, EventArgs e)
        {
            if (!isleftfinished)
            {
                leftnum.Clear();
            }
            else
            {
                rightnum.Clear();
            }
            textBox.Clear();
        }

        private void butj_click(object sender, EventArgs e)
        {
            isleftfinished = true;
            Operand = enOperand.Plus;
            //if (textBox.Text != "")
            //{
            //                    num1 = Convert.ToDouble(textBox.Text);
            //    textBox.Text = "";
            //    eat1 = true;
            //}
        }

        private void butm_Click(object sender, EventArgs e)
        {
            isleftfinished = true;
            Operand = enOperand.Minus;
            //if (textBox.Text != "")
            //{

            //    num1 = Convert.ToDouble(textBox.Text);
            //    textBox.Text = "";
            //    eat2 = true;
            //}
        }

    }
}







