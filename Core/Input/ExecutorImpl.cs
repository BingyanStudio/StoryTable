using System.Threading.Tasks;

namespace StoryTable
{
    public class ExecutorImpl : ExecutorBase
    {
        /// <summary>
        /// <see cref="Process"/>中的刷新时间，单位是毫秒
        /// </summary>
        public int RefreshTime { get; set; } = 10;


        protected int count;
        public override bool Processing => count > 0;
        public override void Complete() => count--;

        protected void NextLine() => Locate(Position.FileName, Position.LineIndex + 1);

        public override async void Execute()
        {
            if (Processing)
            {
                Skip = true;
                return;
            }
            if (Position.LineIndex == CurrentFile.Length) return;
            if (Position.LineIndex == 0) FileProcessing();
            Executing();
            Pause = false;
            while (!Pause) await Process();
            Executed();
            if (Position.LineIndex == CurrentFile.Length) FileProcessed();
        }
        private async Task Process()
        {
            LineProcessing(Position);
            Skip = false;
            ExecuteMode mode;
            while ((mode = CurrentLine.Execute(this)) == ExecuteMode.Next)
            {
                count++;
                NextLine();
                if (Position.LineIndex == CurrentFile.Length) return;
            }
            count++;
            NextLine();
            Pause = mode == ExecuteMode.Pause;
            while (Processing) await Task.Delay(RefreshTime);
            LineProcessed(Position);
        }
    }
}
