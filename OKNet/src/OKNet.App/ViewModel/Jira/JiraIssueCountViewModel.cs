using System;

namespace OKNet.App.ViewModel.Jira
{
    public class JiraIssueCountViewModel : ViewModelBase
    {
        private int _parentHeight;
        private int _allProjectIssueCountTotal;
        private int _issueCount;
        private int _heightAdjustment;

        public string Status { get; set; }
        public string BackgroundColor { get; set; }
        public int Height => (int) GetHeight() + HeightAdjustment;


        public int IssueCount
        {
            get => _issueCount;
            set => SetValue(ref _issueCount, value);
        }

        public int AllProjectIssueCountTotal
        {
            get => _allProjectIssueCountTotal;
            set => SetValue(ref _allProjectIssueCountTotal, value);
        }

        public int ParentHeight
        {
            get => _parentHeight;
            set => SetValue(ref _parentHeight, value);
        }

        public int HeightAdjustment
        {
            get => _heightAdjustment;
            set
            {
                SetValue(ref _heightAdjustment, value);
                OnPropertyChanged(nameof(Height));
            }
        }

        private decimal GetHeight()
        {
            return AllProjectIssueCountTotal > 0 ? (ParentHeight * ((decimal)IssueCount / AllProjectIssueCountTotal)) : 0;
        }

        public decimal Leftover => GetHeight()-Height;
    }
}