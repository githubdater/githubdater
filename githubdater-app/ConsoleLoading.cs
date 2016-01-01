using NGitHubdater;
using NGitHubdaterConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdaterConsole
{
    class ConsoleLoading
    {
        private const int DefaultBarSize = 30;

        private const char DefaultCompleteChar = '=';
        private const ConsoleColor DefaultCompleteColor = ConsoleColor.DarkGreen;

        private const char DefaultIncompleteChar = '-';
        private const ConsoleColor DefaultIncompleteColor = ConsoleColor.Gray;

        private readonly int barSize;

        private readonly char completeChar;
        private readonly ConsoleColor completeColor = ConsoleColor.DarkGreen;

        private readonly char incompleteChar;
        private readonly ConsoleColor incompleteColor = ConsoleColor.Gray;

        public ConsoleLoading() 
            : this(DefaultBarSize, DefaultCompleteChar, ConsoleColor.DarkGreen, DefaultIncompleteChar, ConsoleColor.Gray) { }

        public ConsoleLoading(char completeChar, char incompleteChar) 
            : this(DefaultBarSize, completeChar, DefaultCompleteColor, incompleteChar, DefaultIncompleteColor) { }

        public ConsoleLoading(ConsoleColor completeColor, ConsoleColor incompleteColor)
            : this(DefaultBarSize, DefaultCompleteChar, completeColor, DefaultIncompleteChar, incompleteColor) { }

        public ConsoleLoading(int barSize, char completeChar, char incompleteChar) 
            : this(barSize, completeChar, DefaultCompleteColor, incompleteChar, DefaultIncompleteColor) { }

        public ConsoleLoading(int barSize, ConsoleColor completeColor, ConsoleColor incompleteColor) 
            : this(barSize, DefaultCompleteChar, completeColor, DefaultIncompleteChar, incompleteColor) { }

        public ConsoleLoading(int barSize, char completeChar, ConsoleColor completeColor, char incompleteChar, ConsoleColor incompleteColor)
        {
            this.barSize = barSize;
            this.completeChar = completeChar;
            this.completeColor = completeColor;
            this.incompleteChar = incompleteChar;
            this.incompleteColor = incompleteColor;
        }

        public void execute(FileProcessingProgress progress)
        {
            Console.CursorVisible = false;

            Console.CursorLeft = Console.WindowLeft;
            Console.CursorTop = Console.WindowTop;

            string title = progress.Title;

            if (title != null && title != string.Empty)
            {
                if (progress.Error != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(progress.Error);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(title);
                }

                for(int j = 0; j < Console.WindowWidth; j++)
                {
                    Console.Write(" "); // filling the console
                }

                Console.Write('\r');
            }

            Console.Write("[");

            StringBuilder completePart = new StringBuilder();
            StringBuilder incompletePart = new StringBuilder();

            decimal barCompleteNumber = ((decimal)progress.Percentage / (decimal)100) * barSize;

            int i, k = 0;
            for (i = 0; i < barCompleteNumber; i++) completePart.Append(completeChar);
            for (k = 0; k < barSize - i; k++) incompletePart.Append(incompleteChar);

            Console.ForegroundColor = completeColor;
            Console.Write(completePart);

            Console.ForegroundColor = incompleteColor;
            Console.Write(incompletePart);

            Console.ResetColor();

            Console.Write("]");

            KeyValuePair<long, BytesUnit> total = BytesUnit.GetHumanReadable(progress.TotalBytesToProcess);
            KeyValuePair<long, BytesUnit> received = new KeyValuePair<long, BytesUnit>(total.Value.Convert(progress.BytesProcessed), total.Value);

            Console.Write(" {0}% - {1}/{2}", progress.Percentage, (received.Key + received.Value.ShortHandle), (total.Key + total.Value.ShortHandle));

            for (int j = 0; j < Console.WindowWidth; j++)
            {
                Console.Write(" "); // filling the console
            }
        }
    }
}
