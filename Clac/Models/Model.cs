using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace Clac.Models
{
    enum Operators
    {
        Add,
        Sub,
        Mul,
        Div,
        Equal,
    }  

    public class Model : NotificationObject
    {
        Action PrevOperate;
        bool isLastInputOperatorFlag = false;

        private decimal? leftOperand;
        public decimal? LeftOperand
        {
            get { return this.leftOperand; }
            set
            {
                if (this.leftOperand == value) return;
                this.leftOperand = value;
                RaisePropertyChanged("LeftOperand");
            }
        }


        private decimal? rightOperand;
        public decimal? RightOperand
        {
            get { return this.rightOperand; }
            set
            {
                if (this.rightOperand == value) return;
                this.rightOperand = value;
                RaisePropertyChanged("RightOperand");
            }
        }

        public void Operate(string type)
        {
            if (PrevOperate == null) PrevOperate = this.Equal;
            if(!this.isLastInputOperatorFlag)PrevOperate();

            Operators po = (Operators)Enum.Parse(typeof(Operators), type);
            switch(po)
            {
                case Operators.Add:
                    this.PrevOperate = this.Addition;
                    break;
                case Operators.Sub:
                    this.PrevOperate = this.Subtraction;
                    break;
                case Operators.Mul:
                    this.PrevOperate = this.Multiplication;
                    break;
                case Operators.Div:
                    this.PrevOperate = this.Division;
                    break;
                default:
                    this.PrevOperate = this.Equal;
                    break;
            }
            this.isLastInputOperatorFlag = true;
        }

        public void Addition()
        {
            this.CheckOperand();
            this.LeftOperand += this.RightOperand;
            this.RightOperand = null;
        }
        public void Subtraction()
        {
            this.CheckOperand();
            this.LeftOperand -= this.RightOperand;
            this.RightOperand = null;
        }
        public void Multiplication()
        {
            this.CheckOperand();
            this.LeftOperand *= this.RightOperand; 
            this.RightOperand = null;
        }
        public void Division()
        {
            this.CheckOperand();
            this.LeftOperand /= this.RightOperand; // divZeroはここで例外出るに任せる。いいのか？
            this.RightOperand = null;
        }
        public void Equal()
        {
            this.LeftOperand = this.RightOperand;
            this.RightOperand = null;
        }

        public void CheckOperand()
        {
            if(this.LeftOperand == null)
            {
                this.LeftOperand = (decimal)0;
            }
            if (this.RightOperand == null)
            {
                this.RightOperand = this.LeftOperand;
            }                                       
        }
         public void AddDigit(int num)
        {
            decimal val;
            decimal.TryParse(this.RightOperand.ToString() + num.ToString(), out val);
            this.RightOperand = val;     
            this.isLastInputOperatorFlag = false;
        }

        public void Clear()
        {
            this.RightOperand = null;
            this.isLastInputOperatorFlag = false;
        }
        public void AllClear()
        {
            this.LeftOperand = null;
            this.RightOperand = null;
            PrevOperate = null;
            this.isLastInputOperatorFlag = false;
        }
    }
}
