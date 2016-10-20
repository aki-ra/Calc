using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;
using System.Windows.Input;

using Clac.Models;

namespace Clac.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {                                  
        public void Initialize()
        {
        }

        private Model model;
        public Model Model
        {
            get
            {
                if (this.model == null)
                {
                    this.model = new Model();
                }
                return this.model;
            }
        }

        Livet.Commands.ListenerCommand<string> addDigitCommand;
        public ICommand AddDigitCommand
        {
            get
            {
                if (this.addDigitCommand == null)
                {
                    this.addDigitCommand = new Livet.Commands.ListenerCommand<string>(this.AddDigit);
                }
                return this.addDigitCommand;
            }
        }

        private void AddDigit(string num)
        {
            this.Model.AddDigit(int.Parse(num));
        }

        Livet.Commands.ListenerCommand<string> operateCommand;
        public ICommand OperateCommand
        {
            get
            {
                if (this.operateCommand == null)
                {
                    this.operateCommand = new Livet.Commands.ListenerCommand<string>(this.Operate);
                }
                return this.operateCommand;
            }
        }

        private void Operate(string type)
        {
            try
            {
                this.Model.Operate(type);
            }
            catch (DivideByZeroException e)
            {
                Messenger.Raise(new Livet.Messaging.TransitionMessage("DivideByZero"));
            }
        }

        Livet.Commands.ViewModelCommand clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                if (this.clearCommand == null)
                {
                    this.clearCommand = new Livet.Commands.ViewModelCommand(() =>
                    {
                        this.Model.Clear();
                    });
                }
                return this.clearCommand;
            }
        }

        Livet.Commands.ViewModelCommand allClearCommand;
        public ICommand AllClearCommand
        {
            get
            {
                if (this.allClearCommand == null)
                {
                    this.allClearCommand = new Livet.Commands.ViewModelCommand(() =>
                    {
                        this.Model.AllClear();
                    });
                }
                return this.allClearCommand;
            }
        }

        Livet.Commands.ViewModelCommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (this.addCommand == null)
                {
                    this.addCommand = new Livet.Commands.ViewModelCommand(() =>
                    {
                        this.Model.Addition();
                    });
                }
                return this.addCommand;
            }
        }
    }
}
