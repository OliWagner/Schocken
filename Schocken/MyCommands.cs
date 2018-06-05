using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Schocken
{
    public static class MyCommands
    {
        public static readonly RoutedUICommand BtnReadyCommand = new RoutedUICommand
                (
                        "BtnReadyCommand",
                        "BtnReadyCommand",
                        typeof(MyCommands)
                );
        public static readonly RoutedUICommand Btn1Command = new RoutedUICommand
                (
                        "Btn1Command",
                        "Btn1Command",
                        typeof(MyCommands)
                );
        public static readonly RoutedUICommand Btn2Command = new RoutedUICommand
                (
                        "Btn2Command",
                        "Btn2Command",
                        typeof(MyCommands)
                );
        public static readonly RoutedUICommand Btn3Command = new RoutedUICommand
                (
                        "Btn3Command",
                        "Btn3Command",
                        typeof(MyCommands)
                );
        public static readonly RoutedUICommand BtnRollCommand = new RoutedUICommand
                (
                        "BtnRollCommand",
                        "BtnRollCommand",
                        typeof(MyCommands)
                );
    }
}
