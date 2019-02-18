using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OKNet.App.ViewModel.Jira
{
    public class JiraInProgressGraphViewModel : ViewModelBase
    {
        private ObservableCollection<JiraProjectIssueCountViewModel> _projectIssuesCount = new ObservableCollection<JiraProjectIssueCountViewModel>();
        private int _width;
        private string _graphHeight;
        private string _margin;
        protected int NumTicks = 10;
        protected int _graphWidth;

        public string GetGraphHeight => (Convert.ToInt32(GraphHeight) - (Convert.ToInt32(Margin)*2)).ToString();
        public int GetStackedChartWidth => Width / ProjectIssuesCount.Count;
        public int IssuesTotal => ProjectIssuesCount.SelectMany(model => model.ProjectIssues).Select(model => model.IssueCount).Sum();
        public List<TickValue> Ticks => Enumerable.Range(1, NumTicks).Select(i => new TickValue {DisplayText = ((IssuesTotal / NumTicks) * i).ToString(), Height = (int) HeightPerTick}).Reverse().ToList();
        public decimal HeightPerTick => (Convert.ToDecimal(GraphHeight) / NumTicks);

        public string GraphHeight
        {
            get => _graphHeight;
            set
            {
                SetValue(ref _graphHeight, value);
                OnPropertyChanged(nameof(GetGraphHeight));
                OnPropertyChanged(nameof(Ticks));
            }
        }

        public ObservableCollection<JiraProjectIssueCountViewModel> ProjectIssuesCount
        {
            get => _projectIssuesCount;
            set
            {
                SetValue(ref _projectIssuesCount, value);
                OnPropertyChanged(nameof(IssuesTotal));
                OnPropertyChanged(nameof(HeightPerTick));
                OnPropertyChanged(nameof(Ticks));
                Refresh();
            }
        }

        public int Width
        {
            get => _width;
            set
            {
                SetValue(ref _width, value);
                OnPropertyChanged(nameof(GetStackedChartWidth));
            }
        }

        public string Margin
        {
            get => _margin;
            set
            {
                SetValue(ref _margin, value);
                OnPropertyChanged(nameof(GetGraphHeight));
            }
        }

        public override void Refresh()
        {
            foreach (var jiraProjectIssueCountViewModel in ProjectIssuesCount)
            {
                jiraProjectIssueCountViewModel.Width = GetStackedChartWidth;
                jiraProjectIssueCountViewModel.AllProjectIssuesCount = IssuesTotal;
                jiraProjectIssueCountViewModel.Height = Convert.ToInt32(GraphHeight);
                jiraProjectIssueCountViewModel.Refresh();
            }
            base.Refresh();
        }
    }
}